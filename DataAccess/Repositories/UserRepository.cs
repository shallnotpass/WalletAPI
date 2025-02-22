using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbAccess _dbAccess;
        public UserRepository(DbAccess dbAccess) 
        {
            _dbAccess = dbAccess;
        }
        public async Task<User> AddUser(string email)
        {
            var user = new User
            {
                Email = email,
                Balance = 0
            };

            user.Id = Guid.NewGuid().ToString();

            // Добавление нового пользователя
            await _dbAccess.Users.AddAsync(user);
            await _dbAccess.SaveChangesAsync();
            return user;
        }

        public Task<User> EditUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindUser(string userId)
        {
            return _dbAccess.Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}
