using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.FormCredit.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.FormCredit.Handlers.Queries;

public class ChangeStatusFormCreditQueryHandler : IRequestHandler<ChangeStatusFormCreditQueryReq, ResponseDto>
{

    private readonly IFormCreditRepository _formCreditRep;
    public ChangeStatusFormCreditQueryHandler(IFormCreditRepository formCreditRep)
    {
        _formCreditRep = formCreditRep;
    }
    public async Task<ResponseDto> Handle(ChangeStatusFormCreditQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto response=new();
        var item = await _formCreditRep.GetAsync(request.Id);
        if (request.Type == "Valid")
        {
            item.Status = "تائید شده";
        }
        else
        {
            item.Status = "در انتظار";
        }
        await _formCreditRep.UpdateAsync(item);
        response.IsSuccessed=true;
        response.Message="عملیات با موفقیت انجام شد";
        return response;
    }
}