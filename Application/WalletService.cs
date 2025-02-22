using Application.DTOs;
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
        public async Task<User> CreateWallet(UserRegistrationDto userData)
        {
            return await _userRepository.AddUser(userData.Email);
        }

        public Task<User> Deposit(string userId, decimal deposit)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetBalance(string userId)
        {
            return _userRepository.FindUser(userId);
        }

        public Task<User> Withdraw(string userId, decimal withdrawal)
        {
            throw new NotImplementedException();
        }
    }
}
