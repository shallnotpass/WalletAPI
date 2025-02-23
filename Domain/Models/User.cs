namespace Domain.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public decimal Balance {  get; private set; }

        public bool Withdraw (decimal amount)
        {
            var difference = Balance - amount;

            if (difference > 0)
            {
                Balance = difference;
                return true;
            }
            return false;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
    }
}
