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
            user.VerificationToken = GenerateVerificationToken();

            User new_user = _userRepository.CreateUser(user);
            UserResponse response = _mapper.Map<UserResponse>(new_user);
            SendVerificationEmail(user);
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

        public string GenerateVerificationToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(3));
            
            var tokenIsUnique = _userRepository.GetAllUsers().Any(x => x.VerificationToken == token);
            if (!tokenIsUnique)
                return GenerateVerificationToken();

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
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "mail@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", to);
            mimeMessage.To.Add(mailboxAddressTo);
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate("mail@gmail.com", "sifre");
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

        public void ForgotPassword(string mail)
        {
            var user = _userRepository.GetAllUsers().SingleOrDefault(x => x.Email == mail);

            if (user == null) return;

            var password = Convert.ToHexString(RandomNumberGenerator.GetBytes(4));
            user.Password = _hashService.HashPassword(password);

            _userRepository.UpdateUser(user);

            SendPasswordResetEmail(user.Email,password);
        }

        public void SendPasswordResetEmail(string mail, string password)
        {
            string message;
                message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                            <p><code>{password}</code></p>";

            SendMail(
                to: mail,
                subject: "Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                        {message}"
            );
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
