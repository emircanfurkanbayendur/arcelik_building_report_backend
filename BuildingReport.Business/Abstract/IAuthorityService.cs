using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Abstract
{
    public interface IAuthorityService
    {
        List<AuthorityResponse> GetAllAuthorities();

        AuthorityResponse GetAuthorityById(long id);

        AuthorityResponse CreateAuthority(AuthorityRequest request);

        AuthorityResponse UpdateAuthority(UpdateAuthorityRequest request);

        void DeleteAuthority(long id);
        void checkIfAuthorityExistsByName(string name);
        void checkIfAuthorityExistsById(long id);
    }
}
