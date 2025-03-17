namespace Parstech.Shop.ApiService.Application.DTOs;

public class ContractsDto
{
    public List<ContractDto> Details { get; set; }
    public long TotalStore { get; set; }
    public long TotalWe { get; set; }
}

public class ContractDto
{
    public int DetailId { get; set; }
    public string DetailName { get; set; }
    public long Total { get; set; }
    public long Store { get; set; }
    public long We { get; set; }
}