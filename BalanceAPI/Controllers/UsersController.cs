using Application;
using Application.Models;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BalanceAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IValidator<UserRegistrationRequest> _registrationValidator;
        public UsersController(IWalletService walletService, IValidator<UserRegistrationRequest> validator) 
        {
            _walletService = walletService;
            _registrationValidator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationRequest request)
        {
            var validationResult = _registrationValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = await _walletService.CreateWallet(request);

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
                return NotFound("The user id does not exsists");
            }

            return Ok(new { userId = user.Id, balance = user.Balance });
        }

        [HttpPost]
        [Route("{id}/deposit")]
        public async Task<IActionResult> Deposit(Guid id, DepositRequest request)
        {
            if (request.Amount<0)
            {
                return BadRequest("Incorrect amount for a transaction");
            }

            var transactionResult = await _walletService.Deposit(id.ToString(), request.Amount);

            if(!transactionResult.IsSuccessful)
            {
                return NotFound(transactionResult?.error?.ErrorMessage);
            }

            return Ok(new { userId = transactionResult?.user?.Id, newBalance = transactionResult?.user?.Balance });
        }

        [HttpPost]
        [Route("{id}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid id, WithdrawalRequest request)
        {
            if (request.Amount < 0)
            {
                return BadRequest("Incorrect amount for a transaction");
            }

            var transactionResult = await _walletService.Withdraw(id.ToString(), request.Amount);

            if (!transactionResult.IsSuccessful)
            {
                switch(transactionResult.error.type)
                {
                    case Domain.Errors.ErrorType.NotFound:
                        return NotFound(transactionResult?.error?.ErrorMessage);

                    case Domain.Errors.ErrorType.TransactionError:
                        return Conflict(transactionResult?.error?.ErrorMessage);
                }
            }

            return Ok(new { userId = transactionResult?.user?.Id, newBalance = transactionResult?.user?.Balance });
        }
    }
}
