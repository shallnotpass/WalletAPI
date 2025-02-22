using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(string email);
        Task<User> FindUser(string userId);
        Task<User> EditUser(string userId);
    }
}
