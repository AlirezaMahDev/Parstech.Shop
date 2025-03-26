namespace Parstech.Shop.Context.Application.DTOs.Api;

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

public class Root
{
    public Result result { get; set; }
}


    
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class ApiBuyClass
{
    public bool haveCash { get; set; } = false;
    public bool haveCredit { get; set; } = true;
    public bool haveCheck { get; set; } = false;
    public bool haveBon { get; set; } = false;
    public bool taavoniDebt { get; set; } = false;
    public int cashTransactionValue { get; set; } = 0;
    public int creditTransactionValue { get; set; }
    public int checkTransactionValue { get; set; } = 0;
    public int bonTransactionValue { get; set; } = 0;
    public string nationalCode { get; set; }
    public int sellerId { get; set; } = 23;
    public int phoneNumberId { get; set; }
    public object checksJsonData { get; set; } = null;
    public object bonsJsonData { get; set; } = null;
    public string personelCode { get; set; } = "";
    public string explains { get; set; } = "";
}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class ApiUserInfoNumber
{
    public int id { get; set; }
}

public class ApiUserInfoResult
{
    public List<ApiUserInfoNumber> numbers { get; set; }
}

public class ApiUserInfoRoot
{
    public ApiUserInfoResult result { get; set; }
}