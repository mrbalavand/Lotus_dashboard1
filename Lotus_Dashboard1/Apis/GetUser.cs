using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lotus_Dashboard1.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUser : ControllerBase
    {



        public string getuserinfo()
        {
            try
            {
                var userid = HttpContext.User;
                var UserRole = "";

                var UserName = userid.Claims.Select(x => x.Issuer).FirstOrDefault();
                int Counter = 1;
                foreach (var item in userid.Claims)
                {

                    if (Counter == 5)
                    {
                        UserRole = item.Value;
                    }
                    Counter = Counter + 1;
                }

                return UserName;
            }

            catch (Exception)
            {

                return "";
            }
        }
    }
}



