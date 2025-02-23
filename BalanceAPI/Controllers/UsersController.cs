using Application;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BalanceAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWalletService _walletService;
        public UsersController(IWalletService walletService) 
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationRequest model)
        {
            //var validationResult = _registrationValidator.Validate(model);
            var user = await _walletService.CreateWallet(model);

            if (user == null)
            {
                return BadRequest("Email field is empty");
            }

            return Ok(new { user.Id, user.Email, user.Balance });
        }

        [HttpGet]
        [Route("{id}/balance")]
        public async Task<IActionResult> GetBalance(Guid id)
        {
            var user = await _walletService.GetBalance(id.ToString());

            if (user == null)
            {
                return BadRequest("Email field is empty");
            }

            return Ok(new { user.Id, user.Email, user.Balance });
        }

        [HttpPost]
        [Route("{id}/deposit")]
        public async Task<IActionResult> Deposit(Guid id, DepositRequest depositData)
        {
            var user = await _walletService.Deposit(id.ToString(), depositData.Amount);

            if (user == null)
            {
                return BadRequest("Email field is empty");
            }

            return Ok(new { user.Id, user.Email, user.Balance });
        }

        [HttpPost]
        [Route("{id}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid id, WithdrawalRequest withdrawalData)
        {
            var user = await _walletService.Withdraw(id.ToString(), withdrawalData.Amount);

            if (user == null)
            {
                return BadRequest("Email field is empty");
            }

            return Ok(new { user.Id, user.Email,  user.Balance });
        }
    }
}
