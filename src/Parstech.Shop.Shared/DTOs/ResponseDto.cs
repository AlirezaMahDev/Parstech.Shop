using FluentValidation.Results;

namespace Parstech.Shop.Shared.DTOs;

public class ResponseDto
{
    public bool IsSuccessed { get; set; }
    public object Object { get; set; } = null!;
    public object Object2 { get; set; } = null!;
    public object role { get; set; } = null!;
    public object CurrentParameter { get; set; } = null!;
    public List<ValidationFailure> Errors { get; set; } = null!;
    public string? Message { get; set; }
    
    // Authentication specific properties
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public bool NeedRegister { get; set; }
    public bool NeedActiveCode { get; set; }
    public bool NeedPassword { get; set; }
    public bool NeedResendActiveCode { get; set; }
    public bool ActiveRegister { get; set; }
}