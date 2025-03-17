using Microsoft.AspNetCore.Mvc;

using Parstech.Shop.ApiService.Application.Generator;

namespace Parstech.Shop.ApiService.Controllers;

public class UploadSuccess
{
    public int Uploaded { get; set; }
    public string FileName { get; set; }
    public string Url { get; set; }
}

public class CkEditorController : Controller
{
    [HttpPost]
    [Route("file-upload")]
    public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
    {
        if (upload.Length <= 0)
        {
            return null;
        }

        string fileName = "";

        try
        {
            // 1. بررسی وجود فایل
            if (upload == null || upload.Length == 0)
            {
                return Json(new { uploaded = false });
            }

            // 2. بررسی فرمت فایل
            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            string fileExtension = Path.GetExtension(upload.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return Json(new { uploaded = false });
            }

            // 3. بررسی اندازه فایل (مثلاً حداکثر 5 مگابایت)
            int maxFileSize = 5 * 1024 * 1024; // 5 MB
            if (upload.Length > maxFileSize)
            {
                return Json(new { uploaded = false });
            }

            // اگر همه چیز درست بود، فایل را ذخیره کنید
            fileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(upload.FileName);
            string imagePath =
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images/Products", fileName);
            using (FileStream stream = new(imagePath, FileMode.Create))
            {
                upload.CopyTo(stream);
            }

            string url = $"{"/Shared/Images/Products/"}{fileName}";

            return Json(new { uploaded = true, url });
        }
        catch (Exception ex)
        {
            return Json(new { uploaded = false });
        }


        //try
        //{
        //    if (upload != null)
        //    {

        //        fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
        //        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images/Products", fileName);
        //        using (var stream = new FileStream(imagePath, FileMode.Create))
        //        {
        //            upload.CopyTo(stream);
        //        }
        //    }


        //}
        //catch (Exception e)
        //{
        //}
    }


    [HttpPost]
    [Route("UploadImage")]
    public async Task<JsonResult> UploadImage([FromForm] IFormFile upload)
    {
        if (upload.Length <= 0)
        {
            return null;
        }

        //your custom code logic here

        //1)check if the file is image

        //2)check if the file is too large

        //etc

        try
        {
            // 1. بررسی وجود فایل
            if (upload == null || upload.Length == 0)
            {
                return Json(new { uploaded = false });
            }

            // 2. بررسی فرمت فایل
            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            string fileExtension = Path.GetExtension(upload.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return Json(new { uploaded = false });
            }

            // 3. بررسی اندازه فایل (مثلاً حداکثر 5 مگابایت)
            int maxFileSize = 5 * 1024 * 1024; // 5 MB
            if (upload.Length > maxFileSize)
            {
                return Json(new { uploaded = false });
            }

            // اگر همه چیز درست بود، فایل را ذخیره کنید
            string fileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(upload.FileName);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", fileName);
            using (FileStream stream = new(imagePath, FileMode.Create))
            {
                upload.CopyTo(stream);
            }

            string url = $"{"/Shared/Images/"}{fileName}";

            UploadSuccess success = new() { Uploaded = 1, FileName = fileName, Url = url };

            return new(success);
        }
        catch (Exception ex)
        {
            return Json(new { uploaded = false });
        }


        //var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();


        //var filePath = Path.Combine(
        //    Directory.GetCurrentDirectory(), "wwwroot/Shared/Images",
        //    fileName);

        //using (var stream = System.IO.File.Create(filePath))
        //{
        //    await upload.CopyToAsync(stream);
        //}
    }
}