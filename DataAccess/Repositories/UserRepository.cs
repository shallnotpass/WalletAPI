using Domain.Models;
using Microsoft.EntityFrameworkCore;
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
                Email = email
            };

            user.Id = Guid.NewGuid().ToString();

            // Добавление нового пользователя
            await _dbAccess.Users.AddAsync(user);
            await _dbAccess.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            _dbAccess.Update(user);
            _dbAccess.SaveChanges();
            return user;
        }

        public async Task<User?> FindUser(string userId)
        {
            return _dbAccess.Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}
