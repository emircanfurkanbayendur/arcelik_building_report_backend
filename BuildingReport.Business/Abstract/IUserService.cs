﻿using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
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
        string GenerateVerificationToken();
        void SendMail(string to, string subject, string html, string from = null);
        void SendVerificationEmail(User user);
        bool VerifyToken(string token);
        void ForgotPassword(string mail);

        void SendPasswordResetEmail(string mail, string password);

        void DeleteUser(long id);
        void CheckIfUserExistsByEmail(string email);
        void CheckIfUserExistsById(long id);
        List<User> GetUsersByRole(long roleId);

        User UpdateUserRole(long id);

    }
}
