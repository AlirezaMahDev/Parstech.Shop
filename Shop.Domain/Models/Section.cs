using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class Section
{
    public int Id { get; set; }

    public string SectionName { get; set; } = null!;

    public int Sort { get; set; }

    public int? CateguryId { get; set; }

    public int SectionTypeId { get; set; }

    public int? ProductId { get; set; }

    public int? StoreId { get; set; }

    public virtual ICollection<ProductStockPriceSection> ProductStockPriceSections { get; set; } = new List<ProductStockPriceSection>();

    public virtual ICollection<SectionDetail> SectionDetails { get; set; } = new List<SectionDetail>();

    public virtual SectionType SectionType { get; set; } = null!;
}
