
using Application.Models;
using Domain.Models;

namespace Application
{
    public interface IWalletService
    {
        public Task<User?> CreateWallet(UserRegistrationRequest userData);
        public Task<User?> GetBalance(string userId);
        public Task<TransactionResult> Withdraw(string userId, decimal withdrawal);
        public Task<TransactionResult> Deposit(string userId, decimal deposit);
    }
}
