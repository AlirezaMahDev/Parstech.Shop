using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.ProductStockPriceSection.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPriceSection.Handlers.Commands
{
    public class DeleteProdcutStockPriceSectionCommandHandler : IRequestHandler<DeleteProdcutStockPriceSectionCommandReq, ResponseDto>
    {
        #region Constractor 
        private readonly IProductStockPriceSectionRepository _productStockPriceSectionRep;

        public DeleteProdcutStockPriceSectionCommandHandler(IProductStockPriceSectionRepository productStockPriceSectionRep)
        {
            _productStockPriceSectionRep = productStockPriceSectionRep;
        }
        #endregion
        public async Task<ResponseDto> Handle(DeleteProdcutStockPriceSectionCommandReq request, CancellationToken cancellationToken)
        {
            ResponseDto response=new ResponseDto();
            var item =await _productStockPriceSectionRep.GetAsync(request.id);
            await _productStockPriceSectionRep.DeleteAsync(item);
            response.IsSuccessed = true;
            response.Object = request.id;
            return response;
        }
    }
}
