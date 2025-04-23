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

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Product,int>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Product, int>().GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetByBranchIdAsync(int branchId)
        {
            return await _unitOfWork.Repository<Product, int>().FindAsync(p => p.BranchId == branchId);
        }

        public async Task AddAsync(Product product)
        {
            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(product.BranchId);
            if (branch == null)
            {
                throw new InvalidOperationException($"Branch with ID {product.BranchId} not found.");
            }

            await _unitOfWork.Repository<Product, int>().AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var branch = await _unitOfWork.Repository<Branch, int>().GetByIdAsync(product.BranchId);
            if (branch == null)
            {
                throw new InvalidOperationException($"Branch with ID {product.BranchId} not found.");
            }

            _unitOfWork.Repository<Product, int>().Update(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(id);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with ID {id} not found.");
            }

            _unitOfWork.Repository<Product, int>().Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}