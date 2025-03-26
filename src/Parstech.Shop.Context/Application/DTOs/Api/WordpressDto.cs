namespace Parstech.Shop.Context.Application.DTOs.Api;

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class Address
{
    public string street_1 { get; set; }
    public string street_2 { get; set; }
    public string city { get; set; }
    public string zip { get; set; }
    public string country { get; set; }
    public string state { get; set; }
}

public class Attribute
{
    public int id { get; set; }
    public string name { get; set; }
    public int position { get; set; }
    public bool visible { get; set; }
    public bool variation { get; set; }
    public List<string> options { get; set; }
}


public class Category
{
    public int id { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
}



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






public class MetaData
{
    public int id { get; set; }
    public string key { get; set; }
    public object value { get; set; }
}

public class OgImage
{
    public int width { get; set; }
    public int height { get; set; }
    public string url { get; set; }
    public string type { get; set; }
}



public class resultWordpress
{
    public int  productId { get; set; }
    public string  type { get; set; }
    public string  error { get; set; }
}

public class WordpressDto
{
    public int id { get; set; }
    public string name { get; set; }
    //public string slug { get; set; }
    //public string permalink { get; set; }
    //public DateTime date_created { get; set; }
    //public DateTime date_created_gmt { get; set; }
    //public DateTime date_modified { get; set; }
    //public DateTime date_modified_gmt { get; set; }
    public string type { get; set; }
    //public string status { get; set; }
    //public bool featured { get; set; }
    //public string catalog_visibility { get; set; }
    public string description { get; set; }
    //public string short_description { get; set; }
    public string sku { get; set; }
    public string price { get; set; }
    public string regular_price { get; set; }
    public string sale_price { get; set; }
    //public bool on_sale { get; set; }
    //public bool purchasable { get; set; }
    //public int total_sales { get; set; }
    //public bool downloadable { get; set; }

    //public int download_limit { get; set; }
    //public int download_expiry { get; set; }
    //public string external_url { get; set; }
    //public string button_text { get; set; }
    //public string tax_status { get; set; }
    //public string tax_class { get; set; }
    //public bool manage_stock { get; set; }
    public int? stock_quantity { get; set; } = 0;
    //public string backorders { get; set; }
    //public bool backorders_allowed { get; set; }
    //public bool backordered { get; set; }
    //public object low_stock_amount { get; set; }
    //public bool sold_individually { get; set; }
    //public string weight { get; set; }
    //public bool shipping_required { get; set; }
    //public bool shipping_taxable { get; set; }
    //public string shipping_class { get; set; }
    //public int shipping_class_id { get; set; }
    //public bool reviews_allowed { get; set; }
    //public string average_rating { get; set; }
    //public int rating_count { get; set; }
    public int parent_id { get; set; }
    //public string purchase_note { get; set; }
    public List<Category> categories { get; set; }

    public List<Image> images { get; set; }
    public List<Attribute> attributes { get; set; }
    public List<int> variations { get; set; }

    //public int menu_order { get; set; }
    //public string price_html { get; set; }
    //public List<int> related_ids { get; set; }
    //public List<MetaData> meta_data { get; set; }
    public string stock_status { get; set; }
    //public bool has_options { get; set; }

    public Store store { get; set; }
}





public class Store
{
    public int id { get; set; }
    public string name { get; set; }
    public string shop_name { get; set; }
    public string url { get; set; }
    //public Address address { get; set; }
}






// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class variationAttribute
{
    public int id { get; set; }
    public string name { get; set; }
    public string option { get; set; }
}


public class variationRoot
{
    public int id { get; set; }
    public string sku { get; set; }
    public string price { get; set; }
    public string regular_price { get; set; }
    public string sale_price { get; set; }
        
    public string status { get; set; }
       
       
    public int stock_quantity { get; set; }
    public string stock_status { get; set; }
        
    public List<variationAttribute> attributes { get; set; }
       
        
}

public class Self
{
    public string href { get; set; }
}

public class Up
{
    public string href { get; set; }
}

public class WordpressCateguryDto
{

    public int id { get; set; }
    public string name { get; set; }
    public int parent { get; set; }

}