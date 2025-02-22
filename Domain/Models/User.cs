using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }

        public bool reduceBalance (decimal subtrahend)
        {
            var difference = Balance - subtrahend;

            if (difference > 0)
            {
                Balance = difference;
                return true;
            }
            return false;
        }
    }
}
