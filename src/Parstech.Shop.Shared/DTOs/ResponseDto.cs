using FluentValidation.Results;

namespace Parstech.Shop.ApiService.Application.DTOs;

public class ResponseDto
{
    public bool IsSuccessed { get; set; }
    public object Object { get; set; } = null!;
    public object Object2 { get; set; } = null!;
    public object role { get; set; } = null!;
    public object CurrentParameter { get; set; } = null!;
    public List<ValidationFailure> Errors { get; set; } = null!;
    public string? Message { get; set; }
}

public class ErrorList
{
    public string Caption { get; set; }
    public string ErrorMessage { get; set; }
}