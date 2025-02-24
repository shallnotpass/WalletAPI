using Application.Models;
using DataAccess;
using DataAccess.Repositories;
using Domain.Models;
using Domain.Errors;
namespace Application
{
    public class WalletService : IWalletService
    {
        private readonly IUserRepository _userRepository;
        public WalletService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task<User?> CreateWallet(UserRegistrationRequest userData)
        {
            return await _userRepository.AddUser(userData.Email);
        }

        public async Task<TransactionResult> Deposit(string userId, decimal deposit)
        {
            var user = await _userRepository.FindUser(userId);

            if (user == null)
                return new TransactionResult
                {
                    user = null,
                    error = new Error("The user was not found")
                };

            user.Deposit(deposit);

            return new TransactionResult
            {
                user = await _userRepository.UpdateUser(user),
                IsSuccessful = true
            };
        }

        public Task<User?> GetBalance(string userId)
        {
            return _userRepository.FindUser(userId);
        }

        public async Task<TransactionResult> Withdraw(string userId, decimal withdrawal)
        {
            var user = await _userRepository.FindUser(userId);

            if (user == null)
                return new TransactionResult 
                { 
                    user = null, 
                    error = new Error("The user was not found") 
                };

            if (user.Withdraw(withdrawal))
                return new TransactionResult
                {
                    user = await _userRepository.UpdateUser(user),
                    IsSuccessful = true
                };
            else
                return new TransactionResult 
                { 
                    user = user, 
                    error = new Error("Insufficient funds") 
                };
        }
    }
}
