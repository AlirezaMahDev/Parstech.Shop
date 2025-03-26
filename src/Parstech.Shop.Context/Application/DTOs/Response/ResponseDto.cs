using FluentValidation.Results;

namespace Parstech.Shop.Context.Application.DTOs.Response;

public class ResponseDto
{
    public bool IsSuccessed { get; set; }
    public Object Object { get; set; } = null!;
    public Object Object2 { get; set; } = null!;
    public Object role { get; set; } = null!;
    public Object CurrentParameter { get; set; } = null!;
    public List<ValidationFailure> Errors { get; set; } = null!;
    public string? Message { get; set; }
        
}

public class ErrorList
{
    public string Caption { get; set; }
    public string ErrorMessage { get; set; }
}