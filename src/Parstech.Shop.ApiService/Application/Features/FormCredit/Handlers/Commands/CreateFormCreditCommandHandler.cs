using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.FormCredit.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.FormCredit.Handlers.Commands;

public class CreateFormCreditCommandHandler : IRequestHandler<CreateFormCreditCommandReq, ResponseDto>
{
    private readonly IFormCreditRepository _formCreditRep;
    private readonly IMapper _mapper;

    public CreateFormCreditCommandHandler(IFormCreditRepository formCreditRep, IMapper mapper)
    {
        _formCreditRep = formCreditRep;
        _mapper = mapper;
    }

    public async Task<ResponseDto> Handle(CreateFormCreditCommandReq request, CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        Domain.Models.FormCredit? item = _mapper.Map<Domain.Models.FormCredit>(request.FormCreditDto);
        item.CreateDate = DateTime.Now;
        item.Status = "در انتظار";
        await _formCreditRep.AddAsync(item);
        response.IsSuccessed = true;
        response.Object = item;
        response.Message = "درخواست اعتبار شما با موفقیت ثبت گردید";
        return response;
    }
}