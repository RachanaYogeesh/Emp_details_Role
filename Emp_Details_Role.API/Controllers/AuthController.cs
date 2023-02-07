using Emp_Details_Role.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Emp_Details_Role.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository,ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }
        [HttpPost]
        [Route("login")]
        //public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        //{
        //    ////validate request

        //    ////check username,password to authenticate
        //    //var user = await userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
        //    //if (user != null)
        //    //{
        //    //    //generate JWT 
        //    //    var token = await tokenHandler.CreateTokenAsync(user);
        //    //    return Ok(token);
        //    //}
        //    //return BadRequest("Username or Password Incorrect");

        //    var isAuthenticated = await userRepository.AuthenticateAsync(
        //        loginRequest.Username, loginRequest.Password);
        //    if (isAuthenticated)
        //    {
        //        //token
        //    }
        //    return BadRequest("Username or Password Incorrect");
        //}
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            ////validate request

            ////check username,password to authenticate
            var user = await userRepository.AuthenticateUserAsync(loginRequest.Username, loginRequest.Password);
            if (user != null)
            {
                //generate JWT 
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }
            //return BadRequest("Username or Password Incorrect");

            
            return BadRequest("Username or Password Incorrect");
        }
    }
}
