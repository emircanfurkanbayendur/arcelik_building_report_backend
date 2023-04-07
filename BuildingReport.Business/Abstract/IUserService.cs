﻿using BuildingReport.DTO;
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
    public interface IUserService
    {
        List<User> GetAllUsers();

        User GetUserById(long id);

        UserResponse CreateUser(UserRequest request);

        User UpdateUser(UserDTO userdto);

        void DeleteUser(long id);
        void CheckIfUserExistsByEmail(string email);
        void CheckIfUserExistsById(long id);
        List<User> GetUsersByRole(long roleId);

        User UpdateUserRole(long id);

    }
}
