﻿using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.CustomExceptionMiddleware.AuthorityExceptions;
using BuildingReport.Business.CustomExceptionMiddleware.IdExceptions;
using BuildingReport.Business.CustomExceptionMiddleware.RoleExceptions;
using BuildingReport.Business.CustomExceptions.RoleExceptions;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ICacheAuthorityService _cacheAuthorityService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleManager(IMapper mapper, ICacheAuthorityService cacheAuthorityService,IHttpContextAccessor httpContextAccessor)
        {
            _roleRepository = new RoleRepository();
            _mapper = mapper;
            _cacheAuthorityService = cacheAuthorityService;
            _httpContextAccessor = httpContextAccessor;
        }
        public RoleResponse CreateRole(RoleRequest roleDTO)
        {
            // Tokeni headerdan çekiyoruz
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);


            //Redis cache'de token keyi ile authorityleri kontrol ediyoruz
            List<RedisValue> authorityValues = _cacheAuthorityService.CheckCacheAuthority(token);

            // "Create" authority'si var mı kontrol ediyoruz
            if (!authorityValues.Contains("Create"))
            {
                throw new UnauthorizedAccessException();
            }

            _ = roleDTO ?? throw new ArgumentNullException(nameof(roleDTO)," cannot be null.");
            Entities.Role role = _mapper.Map<Entities.Role>(roleDTO);
            CheckIfRoleExistsByName(role.Name);
            RoleResponse response = _mapper.Map<RoleResponse>(_roleRepository.CreateRole(role));
            return response;
        }

        public void DeleteRole(long id)
        {
            // Tokeni headerdan çekiyoruz
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);


            //Redis cache'de token keyi ile authorityleri kontrol ediyoruz
            List<RedisValue> authorityValues = _cacheAuthorityService.CheckCacheAuthority(token);

            // "Delete" authority'si var mı kontrol ediyoruz
            if (!authorityValues.Contains("Delete"))
            {
                throw new UnauthorizedAccessException();
            }


            ValidateId(id);
            CheckIfRoleExistsById(id);
            _roleRepository.DeleteRole(id);
        }

        public List<RoleResponse> GetAllRoles()
        {
            List<RoleResponse> response = _mapper.Map<List<RoleResponse>>(_roleRepository.GetAllRoles());
            return response;
        }

        public RoleResponse GetRoleById(long id)
        {

            ValidateId(id);
            CheckIfRoleExistsById(id);
            RoleResponse response = _mapper.Map<RoleResponse>(_roleRepository.GetRoleById(id));
            return response;
        }

        public RoleResponse UpdateRole(UpdateRoleRequest roleDTO)
        {
            // Tokeni headerdan çekiyoruz
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);


            //Redis cache'de token keyi ile authorityleri kontrol ediyoruz
            List<RedisValue> authorityValues = _cacheAuthorityService.CheckCacheAuthority(token);

            // "Update" authority'si var mı kontrol ediyoruz
            if (!authorityValues.Contains("Update"))
            {
                throw new UnauthorizedAccessException();
            }


            CheckIfRoleExistsById(roleDTO.Id);
            Entities.Role role = _mapper.Map<Entities.Role>(roleDTO);
            RoleResponse response = _mapper.Map<RoleResponse>(_roleRepository.UpdateRole(role));
            return response;
        }


        //BusinessRules

        public void CheckIfRoleExistsByName(string name)
        {
            if (_roleRepository.RoleExistsByName(name))
            {
                throw new RoleAlreadyExistsException("Role already exists.");
            }
        }
        public void CheckIfRoleExistsById(long id)
        {
            if (!_roleRepository.RoleExistsById(id))
            {
                throw new RoleNotFoundException("Role cannot be found.");
            }
        }

        private void ValidateId(long id)
        {
            if (id <= 0 || id > long.MaxValue)
            {
                throw new IdOutOfRangeException(nameof(id), id);
            }
        }

    }
}
