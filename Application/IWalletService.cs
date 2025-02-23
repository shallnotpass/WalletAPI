
using Application.Models;
using Domain.Models;

namespace Application
{
    public interface IWalletService
    {
        public Task<User?> CreateWallet(UserRegistrationRequest userData);
        public Task<User?> GetBalance(string userId);
        public Task<User?> Withdraw(string userId, decimal withdrawal);
        public Task<User?> Deposit(string userId, decimal deposit);
    }
}
