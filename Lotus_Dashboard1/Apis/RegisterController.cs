
using DataModels;
using Lotus_Dashboard1.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {


        private readonly SignInManager<Lotus_Dashboard1User> _signInManager;
        private readonly UserManager<Lotus_Dashboard1User> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        public RegisterController(SignInManager<Lotus_Dashboard1User> signInManager, UserManager<Lotus_Dashboard1User> userManager, RoleManager<IdentityRole> rolemanager)
        {

            _signInManager = signInManager;
            _userManager = userManager;
            _rolemanager = rolemanager;
        }



        [HttpGet]
        public async Task<JsonResult> GetData()
        {

            return new JsonResult("");
        }

        [HttpPost]
        public async Task<JsonResult> GetData([FromBody] List<inputparameters1> Model)
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
                    user.Password1 = item.Value;
                }
                else if (item.Name == "Email")
                {
                    user.Email = item.Value;
                    user.NationalCode1 = "0";
                }
                else if (item.Name == "Mobile")
                {
                    user.PhoneNumber = item.Value;
                }
                else if (item.Name == "Fname")
                {
                    user.Fname1 = item.Value;
                }
              

            }


            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            var roleCheck = await _rolemanager.RoleExistsAsync("User");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                var roleResult = await _rolemanager.CreateAsync(new IdentityRole("User"));
            }
            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
            var user1 = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(user1, "User");


            List<string> errortext = new List<string>();

            if (result.Succeeded)
            {
                return new JsonResult(true);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    errortext.Add(item.Code.ToString());
                }

                return new JsonResult(errortext);
            }









        }







    }
}
