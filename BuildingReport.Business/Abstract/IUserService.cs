using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAllUsers();

        User GetUserById(long id);

        UserResponse CreateUser(UserRequest request);

        User UpdateUser(UserDTO userdto);

        User UpdateUserPatch(int id, JsonPatchDocument<UserDTO> pathdoc);
        string GenerateVerificationToken();
        void SendMail(string to, string subject, string html, string from = null);
        void SendVerificationEmail(User user);
        bool VerifyToken(string token);
        
        void DeleteUser(long id);
        void CheckIfUserExistsByEmail(string email);
        void CheckIfUserExistsById(long id);
        List<User> GetUsersByRole(long roleId);

        User UpdateUserRole(long id);

    }
}
