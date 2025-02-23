using Application.Models;
using DataAccess;
using DataAccess.Repositories;
using Domain.Models;

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

        public async Task<User?> Deposit(string userId, decimal deposit)
        {
            var user = await _userRepository.FindUser(userId);

            if (user == null)
                return null;

            user.Deposit(deposit);
            return await _userRepository.UpdateUser(user);
        }

        public Task<User?> GetBalance(string userId)
        {
            return _userRepository.FindUser(userId);
        }

        public async Task<User?> Withdraw(string userId, decimal withdrawal)
        {
            var user = await _userRepository.FindUser(userId);

            if (user == null)
                return null;

            if (user.Withdraw(withdrawal))
                return await _userRepository.UpdateUser(user);
            else
                return null;
        }
    }
}
