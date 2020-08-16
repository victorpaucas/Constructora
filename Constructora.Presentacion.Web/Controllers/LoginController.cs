using Constructora.Presentacion.Web.Models;
using Constructora.Presentacion.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Constructora.Presentacion.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> Logger;
        private readonly IUserRepository UserRepository;
        private readonly ITokenRepository TokenRepository;

        public LoginController(ILogger<LoginController> logger, IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            Logger = logger;
            UserRepository = userRepository;
            TokenRepository = tokenRepository;
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Validate([FromBody] User user)
        {
            try
            {
                string token = null;

                var login = await UserRepository.Validate(user);

                if (login != null)
                {
                    var createDate = DateTime.Now;
                    var expireDate = DateTime.Now.AddHours(1);

                    token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken
                        (new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("RmF2aWRldkZhdmlkZXY")), SecurityAlgorithms.HmacSha256)), new JwtPayload
                            (
                                "https://favidev.com/",
                                "https://favidev.com/",
                                new[]
                                {
                                new Claim(ClaimTypes.Name, login.Name),
                                new Claim(ClaimTypes.Role, login.UserTypeId.ToString())
                                },
                                createDate,
                                expireDate
                            )
                    ));

                    TokenRepository.Add(new Token()
                    {
                        UserId = login.Id,
                        Key = token,
                        Remove = false,
                        CreateDate = createDate,
                        ExpireDate = expireDate,
                    });

                    return Json((token, userTypeId: login.UserTypeId.ToString()));
                }

                return Json(null);
            }
            catch (Exception exception)
            {
                Logger.LogInformation(exception.Message);
                return new EmptyResult();
            }
        }
    }
}
