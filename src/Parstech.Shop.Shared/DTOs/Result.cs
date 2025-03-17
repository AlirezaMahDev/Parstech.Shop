namespace Parstech.Shop.Shared.DTOs;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Result
{
    public int totalCredit { get; set; }
    public int totalRealCredit { get; set; }
    public int checkCredit { get; set; }
    public int bonCredit { get; set; }
    public int cashCredit { get; set; }
    public int totalAssignedCredit { get; set; }
    public int totalSpentCredit { get; set; }
    public int checkUnpassedValue { get; set; }
    public int realCheckCredit { get; set; }
    public object availableBons { get; set; }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);