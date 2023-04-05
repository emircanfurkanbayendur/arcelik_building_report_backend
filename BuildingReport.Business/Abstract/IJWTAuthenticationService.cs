using BuildingReport.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Abstract
{
    public interface IJWTAuthenticationService
    {
        string Authenticate(string email, byte[] password);
        ReturnDto Login(LoginDto loginDto);
    }


}
