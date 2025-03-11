using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Order;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Handler.Queries
{
    public class ContractOrderDetailQueryHandler : IRequestHandler<ContractOrderDetailQueryReq, ContractDto>
    {
        private readonly IProductStockPriceRepository _productStockPriceRep;
        private readonly IProductRepository _productRep;
        private readonly IOrderDetailRepository _orderDetailRep;
        private readonly IUserStoreRepository _userStoreRep;
        public ContractOrderDetailQueryHandler(IProductStockPriceRepository productStockPriceRep,
            IOrderDetailRepository orderDetailRep,
            IUserStoreRepository userStoreRep, IProductRepository productRep)
        {
            _orderDetailRep = orderDetailRep;
            _productStockPriceRep = productStockPriceRep;
            _userStoreRep = userStoreRep;
            _productRep = productRep;

        }
        public async Task<ContractDto> Handle(ContractOrderDetailQueryReq request, CancellationToken cancellationToken)
        {
            ContractDto Result = new ContractDto();
            
            var productStock =await _productStockPriceRep.GetAsync(request.detail.ProductStockPriceId);
            var product =await _productRep.GetAsync(productStock.ProductId);
            var store =await _userStoreRep.GetAsync(productStock.StoreId);

            if (request.Store == "All" || request.Store == store.LatinStoreName)
            {
                //درصد حق العمل کاری
                var D = 0;
                if (store.PersentOfSale != null)
                {
                    D = store.PersentOfSale.Value;
                }

                //مبلغ کل قلم با احتساب 9 درصد مالیات
                var C = request.detail.Total;

                var X = (double)(C / 100);
                var y = (double)(X * D);

                //سهم شرکت
                long E = (long)Math.Ceiling(y);

                //سهم تامین کننده
                long Z = C - E;
                Result.DetailName = product.Name;
                Result.Total = request.detail.Total;
                Result.We = E;
                Result.Store = Z;
                Result.DetailId = request.detail.Id;
            }
            else 
            {
                Result.We = 0;
                Result.Store = 0;
                Result.DetailId = request.detail.Id;
            }
            

           
            return Result;

        }
    }
}
