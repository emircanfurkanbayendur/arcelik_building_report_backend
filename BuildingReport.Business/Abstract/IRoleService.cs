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
    public interface IRoleService
    {
        List<RoleResponse> GetAllRoles();

        RoleResponse GetRoleById(long id);

        RoleResponse CreateRole(RoleRequest request);

        RoleResponse UpdateRole(UpdateRoleRequest request);

        void DeleteRole(long id);
        void CheckIfRoleExistsByName(string name);
        void CheckIfRoleExistsById(long id);

    }
}
