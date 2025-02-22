
using Application.DTOs;
using Domain.Models;

namespace Application
{
    public interface IWalletService
    {
        public Task<User> CreateWallet(UserRegistrationDto userData);
        public Task<User> GetBalance(string userId);
        public Task<User> Withdraw(string userId, decimal withdrawal);
        public Task<User> Deposit(string userId, decimal deposit);
    }
}
