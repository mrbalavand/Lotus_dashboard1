using DataModels;
using Lotus_Dashboard.Apis.JWT;
using Lotus_Dashboard1.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;


namespace Lotus_Dashboard1.Apis.JWT
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<Lotus_Dashboard1User> _userManager;
        private readonly SignInManager<Lotus_Dashboard1User> _signInManager;
        private readonly IJwtservice _jwtservice;
        public LoginController(UserManager<Lotus_Dashboard1User> userManager, SignInManager<Lotus_Dashboard1User> signInManager, IJwtservice jwtservice)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtservice = jwtservice;
        }
        [HttpPost]
        //[Authorize]
        public async Task<JsonResult> SignIn([FromBody] List<inputparameters1> Model)
        {


            Lotus_Dashboard1User user = new Lotus_Dashboard1User();

            foreach (var item in Model)
            {

                if (item.Name == "User")
                {
                    user.UserName = item.Value;
                }
                else if (item.Name == "Pass")
                {
                    user.PasswordHash = item.Value;
                }

            }



            var User = await _userManager.FindByNameAsync(user.UserName);
            List<error> errors = new List<error>();
            List<login> login = new List<login>();




            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {


                errors.Add(new error() { timeStamp = JsonConvert.SerializeObject(DateTime.Now).Replace("\"", ""), message = "0" });

                var token = _jwtservice.generatetoken(User);


                return new JsonResult(token.Result);
                
            }
            else
            {
                List<string> errortext = new List<string>();

                errortext.Add("نام کاربری یا پسورد اشتباه می باشد");


                return new JsonResult(errortext);
            }

        }

        [HttpGet]
        //[Authorize]
        public async Task<JsonResult> showusers()
        {
            var User = await _userManager.FindByNameAsync("mr.balavand");


            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync("mr.balavand", "123456", true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return new JsonResult(User.UserName.ToString());
                //return "نام کاربری یا پسورد درست می باشد";
            }
            else
            {
                return new JsonResult("نام کاربری یا پسورد درست نمی باشد");
            }

        }



    }
}
