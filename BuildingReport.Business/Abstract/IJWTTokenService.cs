using BuildingReport.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Abstract
{
    public interface IJWTTokenService
    {
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }


}
