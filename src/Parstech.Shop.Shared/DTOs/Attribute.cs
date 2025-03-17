namespace Parstech.Shop.Shared.DTOs;

public class Attribute
{
    public int id { get; set; }
    public string name { get; set; }
    public int position { get; set; }
    public bool visible { get; set; }
    public bool variation { get; set; }
    public List<string> options { get; set; }
}