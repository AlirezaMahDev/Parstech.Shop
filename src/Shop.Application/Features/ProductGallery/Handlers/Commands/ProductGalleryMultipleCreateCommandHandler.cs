using AutoMapper;
using MediatR;
using Microsoft.Extensions.Hosting;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductGallery.Handlers.Commands
{
    public class ProductGalleryMultipleCreateCommandHandler : IRequestHandler<ProductGalleryMultipleCreateCommandReq, ResponseDto>
    {
        #region Contstractor
        private readonly IProductGallleryRepository _productGalleryRep;
        private readonly IMapper _mapper;

        public ProductGalleryMultipleCreateCommandHandler(IProductGallleryRepository productGalleryRep, IMapper mapper)
        {
            _productGalleryRep = productGalleryRep;
            _mapper = mapper;
        }
        #endregion
        public async Task<ResponseDto> Handle(ProductGalleryMultipleCreateCommandReq request, CancellationToken cancellationToken)
        {
            ResponseDto Response = new ResponseDto();
            foreach (var file in request.items.Files)
            {
                
                try
                {
                    // 1. بررسی وجود فایل
                    if (file == null || file.Length == 0)
                    {
                        Response.IsSuccessed = false;
                        Response.Message = "لطفاً یک فایل انتخاب کنید";
                        return Response;
                    }

                    // 2. بررسی فرمت فایل
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        Response.IsSuccessed = false;
                        Response.Message = "فرمت فایل مجاز نیست. فقط فایل‌های jpg, jpeg, png, gif مجاز هستند.";
                        return Response;

                    }

                    // 3. بررسی اندازه فایل (مثلاً حداکثر 5 مگابایت)
                    int maxFileSize = 5 * 1024 * 1024; // 5 MB
                    if (file.Length > maxFileSize)
                    {
                        Response.IsSuccessed = false;
                        Response.Message = "حجم فایل باید کمتر از 5 مگابایت باشد.";
                        return Response;

                    }

                    // اگر همه چیز درست بود، فایل را ذخیره کنید
                    ProductGalleryDto gallery = new ProductGalleryDto();
                    gallery.ImageName = NameGenerator.GenerateUniqCode() + Path.GetExtension(file.FileName);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images/Products", gallery.ImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    gallery.Alt = "Parstech";
                    gallery.IsMain = false;
                    gallery.ProductId = request.productId;
                    var Pgallery = _mapper.Map<Domain.Models.ProductGallery>(gallery);

                    await _productGalleryRep.AddAsync(Pgallery);

                    Response.IsSuccessed = true;
                    

                }
                catch (Exception ex)
                {
                    Response.IsSuccessed = false;
                    Response.Message = "خطا در آپلود فایل: \" + ex.Message";
                    return Response;

                }

            }
            Response.Message = "فایل ها با موفقیت آپلود شد.";
            return Response;
        }
    }
}
