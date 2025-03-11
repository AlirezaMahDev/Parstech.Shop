using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
    public interface IProductRepresesntationRepository: IGenericRepository<ProductRepresentation>
    {
        Task<ProductRepresentation> GetProductRepresentationOfProduct(int productId);
        Task<List<ProductRepresentation>> GetUniqProductRepresentationFromRepId(int repId);
        Task<int> GetLastQuantityFromProductRepresntation(int productId,int repId);

        //Task<ProductRepresentationPagingDto> GetAllProductRepresenattionByPaging(int Pageid = 1, int Take = 30,
        //    string Filter = "", int Rep = 0);

        Task UpdateProductQuantityByProductRepresentationId(int productRepresentationId);

        Task<List<ProductRepresentation>> GetEnterProductRepresentationWithProductId(int productId);
        Task<List<ProductRepresentation>> GetGetoutProductRepresentationWithProductId(int productId);
        Task<List<ProductRepresentation>> GetReturnProductRepresentationWithProductId(int productId);
        Task<List<ProductRepresentation>> GetEnterManualyProductRepresentationWithProductId(int productId);
        Task<int> GetCountEnterManualyProductRepresentationWithProductId(int productId);
        Task<int> GetCountReturnProductRepresentationWithProductId(int productId);
        Task<int> GetCountGetoutProductRepresentationWithProductId(int productId);
        Task<int> GetCountEnterProductRepresentationWithProductId(int productId);
        Task<List<ProductRepresentation>> GetProductRepresentationsWithRepAndProductId(int repId, int productId);
    }
}
