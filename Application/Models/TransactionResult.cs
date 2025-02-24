using Domain.Errors;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TransactionResult
    {
        public User? user;
        public Error? error;
        public bool IsSuccessful;
    }
}
