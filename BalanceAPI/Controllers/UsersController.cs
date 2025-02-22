using Application;
using Application.DTOs;
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
        public async Task<IActionResult> Register(UserRegistrationDto model)
        {
            //var validationResult = _registrationValidator.Validate(model);
            if (model.Email.Length == 0)
            {
                return BadRequest("Email field is empty");
            }

            var user = await _walletService.CreateWallet(model);
            return Ok(new { user.Id, user.Email, user.Balance });
        }

        [HttpGet]
        [Route("{id}/balance")]
        public async Task<IActionResult> GetBalance(Guid id)
        {
            var user = await _walletService.GetBalance(id.ToString());
            return Ok(new { user.Id, user.Email, user.Balance });
        }
    }
}
