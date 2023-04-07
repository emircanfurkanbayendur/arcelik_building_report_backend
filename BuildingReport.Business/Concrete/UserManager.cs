using arcelik_building_report_backend.Abstract;
using arcelik_building_report_backend.Concrete;
using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Concrete
{

    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IHashService _hashService;


        public UserManager(IMapper mapper, IHashService hash)
        {
            _mapper = mapper;
            _hashService = hash;
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
        }

        public UserManager()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
        }

        public UserResponse CreateUser(UserRequest request)
        {
            CheckIfUserExistsByEmail(request.Email);
            var roleID = _roleRepository.GetAllRoles().Where(r => r.Name == "guest").FirstOrDefault().Id;

            User user = _mapper.Map<User>(request);
            user.CreatedAt = DateTime.Now;
            user.IsActive = true;
            user.RoleId = roleID;
            user.Password = _hashService.HashPassword(request.Password);
            User new_user = _userRepository.CreateUser(user);
            UserResponse response = _mapper.Map<UserResponse>(new_user);
            return response;
        }

        public void DeleteUser(long id)
        {
            CheckIfUserExistsById(id);
            _userRepository.DeleteUser(id);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(long id)
        {
            CheckIfUserExistsById(id);
            return _userRepository.GetUserById(id);
        }

        public List<User> GetUsersByRole(long roleId)
        {
            return _userRepository.GetUsersByRole(roleId);
        }

        public User UpdateUser(UserDTO userdto)
        {
            User o_user = GetUserById(userdto.Id);
            User user = _mapper.Map<User>(userdto);
            user.Password = _hashService.HashPassword(userdto.Password);
            user.CreatedAt = o_user.CreatedAt;
            user.RoleId = o_user.RoleId;
            return _userRepository.UpdateUser(user);
        }

        public User UpdateUserRole(long id)
        {
            CheckIfUserExistsById(id);
            var roleID = _roleRepository.GetAllRoles().Where(r => r.Name == "admin").FirstOrDefault().Id;
            var user = GetUserById(id);
            user.RoleId = roleID;
            User new_user = _userRepository.UpdateUser(user);
            return new_user;
        }

        //BusinessRules
        public void CheckIfUserExistsByEmail(string email)
        {
            if (_userRepository.UserExistsByEmail(email))
            {
                throw new NotImplementedException("User already exists.");
            }
        }

        public void CheckIfUserExistsById(long id)
        {
            if (!_userRepository.UserExistsById(id))
            {
                throw new NotImplementedException("User cannot found.");
            }
        }
    }
}
