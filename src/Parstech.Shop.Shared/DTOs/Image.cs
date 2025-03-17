namespace Parstech.Shop.Shared.DTOs;

public class Image
{
    public int id { get; set; }
    public DateTime date_created { get; set; }
    public DateTime date_created_gmt { get; set; }
    public DateTime date_modified { get; set; }
    public DateTime date_modified_gmt { get; set; }
    public string src { get; set; }
    public string name { get; set; }
    public string alt { get; set; }
}