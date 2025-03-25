using AutoMapper;

using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.FormCredit.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.FormCredit.Requests.Queries;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Services;

public class FormCreditGrpcService : FormCreditService.FormCreditServiceBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IFormCreditRepository _formCreditRepository;

    public FormCreditGrpcService(IMediator mediator, IMapper mapper, IFormCreditRepository formCreditRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _formCreditRepository = formCreditRepository;
    }

    public override async Task<FormCreditResponse> CreateFormCredit(CreateFormCreditRequest request,
        ServerCallContext context)
    {
        try
        {
            // Map gRPC request to domain DTO
            FormCreditDto formCreditDto = new()
            {
                Name = request.FormCredit.Name,
                Family = request.FormCredit.Family,
                PersonalCode = request.FormCredit.PersonalCode,
                InternationalCode = request.FormCredit.InternationalCode,
                Mobile = request.FormCredit.Mobile,
                State = request.FormCredit.State,
                RequestPrice = request.FormCredit.RequestPrice,
                TextRequestPrice = request.FormCredit.TextRequestPrice
            };

            // Process with MediatR
            var response = await _mediator.Send(new CreateFormCreditCommandReq(formCreditDto));

            // Map response back to gRPC
            var grpcResponse = new FormCreditResponse { IsSuccess = response.IsSuccessed, Message = response.Message };

            // Add form credit details if available
            if (response.Object != null && response.IsSuccessed)
            {
                var formCredit = response.Object as FormCredit;
                grpcResponse.FormCredit = new Shared.Protos.FormCredit.FormCreditDto
                {
                    Id = formCredit.Id,
                    Name = formCredit.Name,
                    Family = formCredit.Family,
                    PersonalCode = formCredit.PersonalCode,
                    InternationalCode = formCredit.InternationalCode,
                    Mobile = formCredit.Mobile,
                    State = formCredit.State,
                    RequestPrice = formCredit.RequestPrice,
                    TextRequestPrice = formCreditDto.TextRequestPrice,
                    CreateDate = formCredit.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Status = formCredit.Status
                };
            }

            // Map validation errors if any
            if (response.Errors != null)
            {
                foreach (var error in response.Errors)
                {
                    grpcResponse.Errors.Add(new ErrorDetail
                    {
                        PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage
                    });
                }
            }

            return grpcResponse;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<FormCreditDto> GetFormCredit(GetFormCreditRequest request, ServerCallContext context)
    {
        try
        {
            var formCreditDto = await _mediator.Send(new ReadFormCreditCommandReq(request.Id));

            return new Shared.Protos.FormCredit.FormCreditDto
            {
                Id = formCreditDto.Id,
                Name = formCreditDto.Name,
                Family = formCreditDto.Family,
                PersonalCode = formCreditDto.PersonalCode,
                InternationalCode = formCreditDto.InternationalCode,
                Mobile = formCreditDto.Mobile,
                State = formCreditDto.State,
                RequestPrice = formCreditDto.RequestPrice,
                TextRequestPrice = formCreditDto.TextRequestPrice,
                CreateDate = formCreditDto.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                CreateDateShmai = formCreditDto.CreateDateShmai,
                Status = formCreditDto.Status
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<FormCreditsResponse> GetFormCredits(GetFormCreditsRequest request,
        ServerCallContext context)
    {
        try
        {
            List<FormCredit> formCredits = await _formCreditRepository.Search(
                request.Filter ?? string.Empty,
                request.FromDate ?? string.Empty,
                request.ToDate ?? string.Empty);

            var response = new FormCreditsResponse();

            foreach (FormCredit credit in formCredits)
            {
                response.FormCredits.Add(new Shared.Protos.FormCredit.FormCreditDto
                {
                    Id = credit.Id,
                    Name = credit.Name,
                    Family = credit.Family,
                    PersonalCode = credit.PersonalCode,
                    InternationalCode = credit.InternationalCode,
                    Mobile = credit.Mobile,
                    State = credit.State,
                    RequestPrice = credit.RequestPrice,
                    CreateDate = credit.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Status = credit.Status
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<PagedFormCreditsResponse> GetPagedFormCredits(GetPagedFormCreditsRequest request,
        ServerCallContext context)
    {
        try
        {
            // Get all form credits with the provided filter
            List<FormCredit> formCredits = await _formCreditRepository.Search(
                request.Filter ?? string.Empty,
                request.FromDate ?? string.Empty,
                request.ToDate ?? string.Empty);

            // Create the response
            var response = new PagedFormCreditsResponse
            {
                TotalCount = formCredits.Count,
                PageCount = (int)Math.Ceiling(formCredits.Count / (double)request.Take)
            };

            // Apply pagination manually
            IEnumerable<FormCredit>? pagedResults = formCredits
                .Skip(request.Skip)
                .Take(request.Take);

            // Add items to the response
            foreach (FormCredit? credit in pagedResults)
            {
                response.FormCredits.Add(new Shared.Protos.FormCredit.FormCreditDto
                {
                    Id = credit.Id,
                    Name = credit.Name,
                    Family = credit.Family,
                    PersonalCode = credit.PersonalCode,
                    InternationalCode = credit.InternationalCode,
                    Mobile = credit.Mobile,
                    State = credit.State,
                    RequestPrice = credit.RequestPrice,
                    TextRequestPrice = credit.RequestPrice.ToString("N0"),
                    CreateDate = credit.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    CreateDateShmai =
                        _mapper.Map<Shop.Application.DTOs.FormCredit.FormCreditDto>(credit).CreateDateShmai,
                    Status = credit.Status
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<FormCreditResponse> ChangeFormCreditStatus(ChangeFormCreditStatusRequest request,
        ServerCallContext context)
    {
        try
        {
            var response = await _mediator.Send(new ChangeStatusFormCreditQueryReq(request.Id, request.Type));

            return new FormCreditResponse { IsSuccess = response.IsSuccessed, Message = response.Message };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }
}