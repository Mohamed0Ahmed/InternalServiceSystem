using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsByBranchAsync(int branchId);
        Task<Product> CreateProductAsync(int branchId, string name, decimal price, bool isVisible);
    }
}