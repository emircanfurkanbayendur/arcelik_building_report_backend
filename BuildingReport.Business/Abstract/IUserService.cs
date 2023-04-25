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
        List<UserResponse> GetAllUsers();

        UserResponse GetUserById(long id);

        UserResponse CreateUser(UserRequest request);

        UserResponse UpdateUser(UpdateUserRequest userdto);

        User UpdateUserPatch(int id, JsonPatchDocument<PatchUserRequest> pathdoc);
        string GenerateVerificationToken();
        void SendMail(string to, string subject, string html, string from = null);
        void SendVerificationEmail(User user);
        bool VerifyToken(string token);
        void ForgotPassword(string mail);

        void SendPasswordResetEmail(string mail, string password);

        bool DeleteUser(long id);
        void CheckIfUserExistsByEmail(string email);
        void CheckIfUserExistsById(long id);
        List<UserResponse> GetUsersByRole(long roleId);
        UserResponse UpdateUserRole(long id);
        LoginResponse Login(LoginRequest loginDto);
        

    }
}
