using arcelik_building_report_backend.Abstract;
using arcelik_building_report_backend.Concrete;
using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using MimeKit;
using MimeKit.Text;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuildingReport.Business.Concrete
{

    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IHashService _hashService;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        private readonly IRoleAuthorityService _roleAuthorityService;
        private String key = "ThisIsSigninKey12345";
        public static User LoginUser;

        public UserManager(IMapper mapper, IHashService hash, IJWTAuthenticationService jwtAuthenticationService, IRoleAuthorityService roleAuthorityService)
        {
            _mapper = mapper;
            _hashService = hash;
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
            _jwtAuthenticationService = jwtAuthenticationService;
            _roleAuthorityService = roleAuthorityService;
        }

        public LoginResponse Login(LoginRequest loginDto)
        {
            var token = _jwtAuthenticationService.Authenticate(loginDto.Email, loginDto.Password);

            if (token == null)
                return null;

            byte[] _password = _hashService.HashPassword(loginDto.Password);
            var user = _userRepository.GetAllUsers().Where(u => u.Email == loginDto.Email && u.Password.SequenceEqual(_password)).FirstOrDefault();      
            LoginResponse response = _mapper.Map<LoginResponse>(user);
            response.Token = token;
            LoginUser = user;
            return response;

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
            user.VerificationToken = GenerateVerificationToken();

            User new_user = _userRepository.CreateUser(user);
            UserResponse response = _mapper.Map<UserResponse>(new_user);
            //SendVerificationEmail(user);
            return response;
        }

        public bool DeleteUser(long id)
        {
            if (!_roleAuthorityService.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 3))
            {
                return false;
            }
            CheckIfUserExistsById(id);
            _userRepository.DeleteUser(id);
            return true;
        }

        public List<UserResponse> GetAllUsers()
        {
            List<User> users = _userRepository.GetAllUsers();

            var responses = _mapper.Map<List<UserResponse>>(users);
  
            return responses;
        }

        public UserResponse GetUserById(long id)
        {
            CheckIfUserExistsById(id);
            User new_user = _userRepository.GetUserById(id);
            UserResponse response = _mapper.Map<UserResponse>(new_user);
            return response;
        }

        public List<UserResponse> GetUsersByRole(long roleId)
        {
            List<User> users = _userRepository.GetUsersByRole(roleId);
            List<UserResponse> response = users.Select(u => _mapper.Map<UserResponse>(u)).ToList();
            return response;
        }

        public UserResponse UpdateUser(UpdateUserRequest userdto)
        {
            if (!_roleAuthorityService.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 4))
            {
                return null;
            }
            User o_user = _userRepository.GetUserById(userdto.Id);
            User user = _mapper.Map<User>(userdto);
            user.Password = _hashService.HashPassword(userdto.Password);
            user.CreatedAt = o_user.CreatedAt;
            user.RoleId = o_user.RoleId;
            User new_user = _userRepository.UpdateUser(user);
            UserResponse response = _mapper.Map<UserResponse>(new_user);
            return response;
        }

        public User UpdateUserPatch(int id, JsonPatchDocument<UpdateUserRequest> patchdoc)
        {
            User user = _userRepository.GetUserById(id);
            if(user == null)
            {
                throw new Exception($"User with id {id} not found.");
            }

            var password = user.Password;
            long roleid = user.RoleId;

            UpdateUserRequest userDTO = _mapper.Map<UpdateUserRequest>(user);

            patchdoc.ApplyTo(userDTO);

            user = _mapper.Map<User>(userDTO);
            user.Password=password;
            user.RoleId =roleid;
            

            return _userRepository.UpdateUser(user);
        }

        public string GenerateVerificationToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(3));
            
            //var tokenIsUnique = _userRepository.GetAllUsers().Any(x => x.VerificationToken == token);
            //if (!tokenIsUnique)
                //return GenerateVerificationToken();

            return token;
        }

        public void SendVerificationEmail(User user)
        {
            string message = $@"<p>Please use the below token to verify your email address with the <code>/user/verifyToken</code> api route:</p>
                            <p><code>{user.VerificationToken}</code></p>";
            SendMail(
            to: user.Email,
            subject: "Sign-up Verification Verify Email",
            html: $@"<h4>Verify Email</h4>
                        <p>Thanks for registering!</p>
                        {message}"
        );

        }

        public void SendMail(string to, string subject, string html, string from = null)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom=new MailboxAddress("Admin","mailgiriniz");
            mimeMessage.From.Add(mailboxAddressFrom);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", to);
            mimeMessage.To.Add(mailboxAddressTo);
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate("mailgiriniz", "şifre");
            smtp.Send(mimeMessage);
            smtp.Disconnect(true);
        }

        public bool VerifyToken(string token)
        {
            var user = _userRepository.GetAllUsers().SingleOrDefault( x => x.VerificationToken == token);

            if (user == null)
                return false;
            user.VerificationToken = null;

            _userRepository.UpdateUser(user);
            return true;
        }
        public UserResponse UpdateUserRole(long id)
        {
            if (!_roleAuthorityService.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 4))
            {
                return null;
            }
            CheckIfUserExistsById(id);
            var roleID = _roleRepository.GetAllRoles().Where(r => r.Name == "admin").FirstOrDefault().Id;
            User user = _userRepository.GetUserById(id);
            user.RoleId = roleID;
            User new_user = _userRepository.UpdateUser(user);
            UserResponse response = _mapper.Map<UserResponse>(new_user);
            return response;
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
