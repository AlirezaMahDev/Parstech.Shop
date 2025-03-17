namespace Parstech.Shop.ApiService.Application.Images;

public static class ImageDownloadAndSave
{
    public static async Task<string> DownloadImageAsync(string fileName, Uri uri)
    {
        using HttpClient httpClient = new();

        // Get the file extension
        string uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
        string fileExtension = Path.GetExtension(uriWithoutQuery);

        // Create file path and ensure directory exists

        string path = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot/Shared/Images/Products",
            $"{fileName}{fileExtension}");
        //Directory.CreateDirectory(directoryPath);

        // Download the image and write to the file
        byte[] imageBytes = await httpClient.GetByteArrayAsync(uri);
        //file
        await File.WriteAllBytesAsync(path, imageBytes);
        return $"{fileName}{fileExtension}";
    }
}