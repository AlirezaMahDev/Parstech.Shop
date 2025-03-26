namespace Parstech.Shop.Context.Application.Images;

public static class ImageDownloadAndSave
{
    public static async Task<string> DownloadImageAsync( string fileName, Uri uri)
    {
        using var httpClient = new HttpClient();

        // Get the file extension
        var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
        var fileExtension = Path.GetExtension(uriWithoutQuery);

        // Create file path and ensure directory exists
           
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images/Products", $"{fileName}{fileExtension}");
        //Directory.CreateDirectory(directoryPath);

        // Download the image and write to the file
        var imageBytes = await httpClient.GetByteArrayAsync(uri);
        //file
        await File.WriteAllBytesAsync(path, imageBytes);
        return $"{fileName}{fileExtension}";
    }
}