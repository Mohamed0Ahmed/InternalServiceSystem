using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetProductsByBranchAsync(int branchId)
        {
            return await _unitOfWork.Repository<Product, int>()
                .FindAsync(p => p.BranchId == 1 && p.IsVisible);
        }

        public async Task<Product> CreateProductAsync(int branchId, string name, decimal price, bool isVisible)
        {
            var product = new Product
            {
                BranchId = branchId,
                Name = name,
                Price = price,
                IsVisible = isVisible
            };

            await _unitOfWork.Repository<Product, int>().AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return product;
        }
    }
}