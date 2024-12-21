using Lotus_Dashboard1.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lotus_Dashboard.Apis.JWT
{
    public interface IJwtservice
    {
        Task<string> generatetoken(Lotus_Dashboard1User user);
 

    }
}
