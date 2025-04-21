using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Categury.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Categury.Handlers.Commands
{
    public class CateguryDeleteCommandHandler : IRequestHandler<CateguryDeleteCommandReq, ResponseDto>
    {

        private readonly IProductCateguryRepository _productCateguryRep;
        private readonly IPropertyRepository _propertyRep;
        private readonly ICateguryRepository _categuryRep;
        public CateguryDeleteCommandHandler(IProductCateguryRepository productCateguryRep, ICateguryRepository categuryRep, IPropertyRepository propertyRep)
        {
            _propertyRep = propertyRep;
            _productCateguryRep = productCateguryRep;
            _categuryRep = categuryRep;

        }
        public async Task<ResponseDto> Handle(CateguryDeleteCommandReq request, CancellationToken cancellationToken)
        {
            ResponseDto response = new ResponseDto();
            var item = await _categuryRep.GetAsync(request.categuryId);
            if (!await _productCateguryRep.ExistProductCateguryForCateguryId(item.GroupId))
            {
                response.IsSuccessed = false;
                response.Message = "تا زمانی که محصولاتی به این دسته بندی اختصاص دارند امکان حذف وجود ندارد";
            }
            else if (!await _propertyRep.ExistPropertyForCateguryId(item.GroupId))
            {
                response.IsSuccessed = false;
                response.Message = "تا زمانی که ویژگی هایی به این دسته بندی اختصاص دارند امکان حذف وجود ندارد";
            }
            else
            {
                
                await _categuryRep.DeleteAsync(item);
                response.IsSuccessed = true;
                response.Message = "عملیات حذف با موفقیت انجام شد";
            }
            return response;
        }
    }
}
