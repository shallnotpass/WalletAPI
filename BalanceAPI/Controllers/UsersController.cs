using Application;
using Application.Models;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

            return Ok(
                new { 
                    userId = user.Id,
                    email = user.Email,
                    balance = user.Balance 
                });
        }

        [HttpGet]
        [Route("{id}/balance")]
        public async Task<IActionResult> GetBalance(Guid id)
        {
            var user = await _walletService.GetBalance(id.ToString());

            if (user == null)
            {
                return NotFound("The user id does not exsists");//BadRequest("Email field is empty");
            }

            return Ok(new { userId = user.Id, balance = user.Balance });
        }

        [HttpPost]
        [Route("{id}/deposit")]
        public async Task<IActionResult> Deposit(Guid id, DepositRequest depositData)
        {
            var transactionResult = await _walletService.Deposit(id.ToString(), depositData.Amount);

            if(!transactionResult.IsSuccessful)
            {
                return NotFound(transactionResult?.error?.ErrorMessage);
            }

            return Ok(new { userId = transactionResult?.user?.Id, newBalance = transactionResult?.user?.Balance });
        }

        [HttpPost]
        [Route("{id}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid id, WithdrawalRequest withdrawalData)
        {
            var transactionResult = await _walletService.Withdraw(id.ToString(), withdrawalData.Amount);

            if (!transactionResult.IsSuccessful)
            {
                switch(transactionResult.error.type)
                {
                    case Domain.Errors.ErrorType.NotFound:
                        return NotFound(transactionResult?.error?.ErrorMessage);

                    case Domain.Errors.ErrorType.TransactionResult:
                        return Conflict(transactionResult?.error?.ErrorMessage);
                }
            }

            return Ok(new { userId = transactionResult?.user?.Id, newBalance = transactionResult?.user?.Balance });
        }
    }
}
