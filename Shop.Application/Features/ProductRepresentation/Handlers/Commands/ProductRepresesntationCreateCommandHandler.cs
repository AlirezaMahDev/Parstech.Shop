using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Commands;
using Shop.Application.Dapper.ProductStockPrice.Commands;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.ProductRepresentation.Requests.Commands;
using Shop.Application.Features.ProductStockPrice.Requests.Queries;
using Shop.Application.Generator;
using Shop.Domain.Models;

namespace Shop.Application.Features.ProductRepresentation.Handlers.Commands
{
    public class ProductRepresesntationCreateCommandHandler : IRequestHandler<ProductRepresesntationCreateCommandReq, ProductRepresentationDto>
    {
        private readonly IProductRepresesntationRepository _productReoresentationRep;
        private readonly IProductStockPriceRepository _productStockPriceRep;
        private readonly IProductRepository _productRep;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public ProductRepresesntationCreateCommandHandler(IProductRepresesntationRepository productReoresentationRep,
            IMapper mapper,
            IProductStockPriceRepository productStockPriceRep,
            IProductRepository productRep,
            IMediator mediator)
        {
            _productReoresentationRep = productReoresentationRep;
            _productStockPriceRep = productStockPriceRep;
            _productRep = productRep;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<ProductRepresentationDto> Handle(ProductRepresesntationCreateCommandReq request, CancellationToken cancellationToken)
        {
            Domain.Models.ProductRepresentation item = new Domain.Models.ProductRepresentation();
            var productStock = await _productStockPriceRep.GetAsync(request.ProductRepresentationDto.ProductStockPriceId);
            var product = await _productRep.GetAsync(productStock.ProductId);

            if (request.ProductRepresentationDto.TypeId == 2)
            {
                if (productStock.Quantity < request.ProductRepresentationDto.Quantity)
                {
                    return new ProductRepresentationDto();
                }
            }
            item.Quantity = request.ProductRepresentationDto.Quantity;
            if (request.ProductRepresentationDto.UniqeCode == null)
            {
                Random random = new Random();
                request.ProductRepresentationDto.UniqeCode = random.Next(10000, 99999).ToString();
            }

            //var item = _mapper.Map<Domain.Models.ProductRepresentation>(request.ProductRepresentationDto);
            item.TypeId = request.ProductRepresentationDto.TypeId;
            item.UniqeCode = request.ProductRepresentationDto.UniqeCode;
            item.ProductStockPriceId = request.ProductRepresentationDto.ProductStockPriceId;
            item.UserId = request.ProductRepresentationDto.UserId;
            item.RepresntationId = request.ProductRepresentationDto.RepresntationId;
            item.CreateDate = DateTime.Now;
            if (request.ProductRepresentationDto.File != null)
            {
                try
                {
                    request.ProductRepresentationDto.FileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(request.ProductRepresentationDto.File.FileName);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", request.ProductRepresentationDto.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        request.ProductRepresentationDto.File.CopyTo(stream);
                    }

                }
                catch (Exception e)
                {
                }
            }
            var result = await _productReoresentationRep.AddAsync(item);
            await _productReoresentationRep.UpdateProductQuantityByProductRepresentationId(result.Id);


            switch(product.TypeId){
                case 3:
                    await _mediator.Send(new RefreshParentQuantityQueryReq(productStock.ProductId, productStock.StoreId));
                    break;
                case 5:
                    await _mediator.Send(new RefreshParentQuantityQueryReq(productStock.ProductId, productStock.StoreId));
                    break;
            }
            

            return _mapper.Map<ProductRepresentationDto>(result);
        }

    }

    public class ProductRepresesntationQuickCreateCommandHandler : IRequestHandler<ProductRepresesntationQuickCreateCommandReq, ProductRepresentationDto>
    {
        private readonly IProductRepresesntationRepository _productReoresentationRep;
        private readonly IProductStockPriceRepository _productStockPriceRep;
        private readonly IProductRepository _productRep;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IProductCommand _productCommand;
        private readonly IProductStockPriceCommand _productStockPriceCommand;
        private readonly string _connectionString;


        public ProductRepresesntationQuickCreateCommandHandler(IProductRepresesntationRepository productReoresentationRep,
            IMapper mapper,
            IProductStockPriceRepository productStockPriceRep,
            IProductRepository productRep,
            IMediator mediator,
            IProductCommand productCommand,
            IProductStockPriceCommand productStockPriceCommand,
            IConfiguration configuration)
        {
            _productReoresentationRep = productReoresentationRep;
            _productStockPriceRep = productStockPriceRep;
            _productRep = productRep;
            _mapper = mapper;
            _mediator = mediator;
            _productCommand=productCommand;
            _productStockPriceCommand=productStockPriceCommand;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<ProductRepresentationDto> Handle(ProductRepresesntationQuickCreateCommandReq request, CancellationToken cancellationToken)
        {
            var item = new Domain.Models.ProductRepresentation();
            //var productStock = await _productStockPriceRep.GetAsync(request.ProductRepresentationDto.ProductStockPriceId);
            //var product = await _productRep.GetAsync(productStock.ProductId);
            var productStock =  DapperHelper.ExecuteCommand<ProductStockPriceDto>(_connectionString, conn => conn.Query<ProductStockPriceDto>(_productStockPriceCommand.GetProductStockPriceById, new { @Id = request.ProductRepresentationDto.ProductStockPriceId }).FirstOrDefault());
            var product = DapperHelper.ExecuteCommand<ProductDto>(_connectionString, conn => conn.Query<ProductDto>(_productCommand.GetProductById, new { @Id = productStock.ProductId }).FirstOrDefault());

            if (request.ProductRepresentationDto.Quantity <0)
            {
                return new ProductRepresentationDto();
            }

            if (productStock.Quantity == 0&& request.ProductRepresentationDto.Quantity==0)
            {
                return new ProductRepresentationDto();
            }
            else if(productStock.Quantity == request.ProductRepresentationDto.Quantity) 
            { 
                item.Quantity=productStock.Quantity;
                item.TypeId = 2;
            }
            else if(productStock.Quantity > request.ProductRepresentationDto.Quantity) 
            {
                item.Quantity = productStock.Quantity - request.ProductRepresentationDto.Quantity;
                item.TypeId = 2;
            }
            else if(productStock.Quantity < request.ProductRepresentationDto.Quantity) {
                item.Quantity = request.ProductRepresentationDto.Quantity-productStock.Quantity ;
                item.TypeId = 1;
            }


            
            if (request.ProductRepresentationDto.UniqeCode == null)
            {
                Random random = new Random();
                request.ProductRepresentationDto.UniqeCode = random.Next(10000, 99999).ToString();
            }

            //var item = _mapper.Map<Domain.Models.ProductRepresentation>(request.ProductRepresentationDto);
            
            item.UniqeCode = request.ProductRepresentationDto.UniqeCode;
            item.ProductStockPriceId = request.ProductRepresentationDto.ProductStockPriceId;
            item.UserId = request.ProductRepresentationDto.UserId;
            item.RepresntationId = productStock.RepId;
            item.CreateDate = DateTime.Now;
            if (request.ProductRepresentationDto.File != null)
            {
                try
                {
                    request.ProductRepresentationDto.FileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(request.ProductRepresentationDto.File.FileName);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", request.ProductRepresentationDto.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        request.ProductRepresentationDto.File.CopyTo(stream);
                    }

                }
                catch (Exception e)
                {
                }
            }

            var result = await _productReoresentationRep.AddAsync(item);
            await _productReoresentationRep.UpdateProductQuantityByProductRepresentationId(result.Id);


            switch (product.TypeId)
            {
                case 3:
                    await _mediator.Send(new RefreshParentQuantityQueryReq(productStock.ProductId, productStock.StoreId));
                    break;
                case 5:
                    await _mediator.Send(new RefreshParentQuantityQueryReq(productStock.ProductId, productStock.StoreId));
                    break;
            }


            return _mapper.Map<ProductRepresentationDto>(result);
        }
    }
}
