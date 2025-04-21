using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.ProductProperty.Requests.Commands;
using Shop.Application.Generator;
using Shop.Domain.Models;

namespace Shop.Application.Features.ProductGallery.Handlers.Commands
{
    public class ProductGalleryCreateCommandHandler : IRequestHandler<ProductGalleryCreateCommandReq, ResponseDto>
    {
        private readonly IProductGallleryRepository _productGalleryRep;
        private readonly IMapper _mapper;

        public ProductGalleryCreateCommandHandler(IProductGallleryRepository productGalleryRep, IMapper mapper)
        {
            _productGalleryRep = productGalleryRep;
            _mapper = mapper;
        }
        public async Task<ResponseDto> Handle(ProductGalleryCreateCommandReq request, CancellationToken cancellationToken)
        {
            ResponseDto Response = new ResponseDto();
            try
            {
                // 1. بررسی وجود فایل
                if (request.ProductGalleryDto.File == null || request.ProductGalleryDto.File.Length == 0)
                {
                    Response.IsSuccessed = false;
                    Response.Message = "لطفاً یک فایل انتخاب کنید";
                    return Response;
                }

                // 2. بررسی فرمت فایل
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif",".webp" };
                var fileExtension = Path.GetExtension(request.ProductGalleryDto.File.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    Response.IsSuccessed = false;
                    Response.Message = "فرمت فایل مجاز نیست. فقط فایل‌های jpg, jpeg, png, gif مجاز هستند.";
                    return Response;
                    
                }

                // 3. بررسی اندازه فایل (مثلاً حداکثر 5 مگابایت)
                int maxFileSize = 5 * 1024 * 1024; // 5 MB
                if (request.ProductGalleryDto.File.Length > maxFileSize)
                {
                    Response.IsSuccessed = false;
                    Response.Message = "حجم فایل باید کمتر از 5 مگابایت باشد.";
                    return Response;
                    
                }

                // اگر همه چیز درست بود، فایل را ذخیره کنید
                request.ProductGalleryDto.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(request.ProductGalleryDto.File.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images/Products", request.ProductGalleryDto.ImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    request.ProductGalleryDto.File.CopyTo(stream);
                }

                var Pgallery = _mapper.Map<Domain.Models.ProductGallery>(request.ProductGalleryDto);
                await _productGalleryRep.AddAsync(Pgallery);

                Response.IsSuccessed = true;
                Response.Message = "فایل با موفقیت آپلود شد.";
                return Response;
               
            }
            catch (Exception ex)
            {
                Response.IsSuccessed = false;
                Response.Message = "خطا در آپلود فایل: \" + ex.Message";
                return Response;
                
            }
        }
    }
}
