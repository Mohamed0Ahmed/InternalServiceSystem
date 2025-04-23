using System.Application.Abstraction;
using System.Application.Interfaces;
using System.Domain.Entities;

namespace System.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> GetOrCreateCustomerAsync(string phoneNumber, int storeId)
        {
            var customer = await _unitOfWork.Repository<Customer, int>()
                .GetAsync(c => c.PhoneNumber == phoneNumber && c.StoreId == storeId);

            if (customer != null)
            {
                return customer;
            }

            customer = new Customer
            {
                PhoneNumber = phoneNumber,
                StoreId = storeId
            };

            await _unitOfWork.Repository<Customer, int>().AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _unitOfWork.Repository<Customer, int>()
                .GetAsync(c => c.Id == customerId);

            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            return customer;
        }
    }
}