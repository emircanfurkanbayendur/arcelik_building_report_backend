using arcelik_building_report_backend.Abstract;
using arcelik_building_report_backend.Concrete;
using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using System.Security.Cryptography;
using MimeKit;
using MimeKit.Text;
using Microsoft.AspNetCore.JsonPatch;
using BuildingReport.Business.CustomExceptionMiddleware.UserExceptions;
using System.ComponentModel.DataAnnotations;
using BuildingReport.Business.CustomExceptionMiddleware.IdExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace BuildingReport.Business.Concrete
{

    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IHashService _hashService;
        private readonly IRoleService _roleService;
        private readonly IJWTAuthenticationService _jwtAuthenticationService;
        private readonly IRoleAuthorityService _roleAuthorityService;
        private String key = "ThisIsSigninKey12345";
        public static User LoginUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTTokenService _jwtTokenService;
        private readonly ICacheAuthorityService _cacheAuthorityService;
        

        IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();

        public UserManager(IMapper mapper, IHashService hash, IJWTAuthenticationService jwtAuthenticationService, IRoleAuthorityService roleAuthorityService, IRoleService roleService, IHttpContextAccessor httpContextAccessor, IJWTTokenService jwtTokenService, ICacheAuthorityService cacheAuthorityService)
        {
            _mapper = mapper;
            _hashService = hash;
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
            _jwtAuthenticationService = jwtAuthenticationService;
            _roleAuthorityService = roleAuthorityService;
            _roleService = roleService;
            _httpContextAccessor = httpContextAccessor;
            _jwtTokenService = jwtTokenService;
            _cacheAuthorityService = cacheAuthorityService;
        }

        public LoginResponse Login(LoginRequest loginDto)
        {
            _ = loginDto ?? throw new ArgumentNullException(nameof(loginDto)," cannot be null.");


            var token = _jwtAuthenticationService.Authenticate (loginDto.Email, loginDto.Password);

            if (token == null)
                throw new UnauthorizedAccessException("Invalid email or password.");
               

            byte[] _password = _hashService.HashPassword(loginDto.Password);
            var user = _userRepository.GetAllUsers().Where(u => u.Email == loginDto.Email && u.Password.SequenceEqual(_password)).FirstOrDefault();      
            LoginResponse response = _mapper.Map<LoginResponse>(user);
            response.Token = token;
            LoginUser = user;
            return response;

        }


        public UserResponse CreateUser(UserRequest request)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request), " cannot be null.");

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
            // Tokeni headerdan çekiyoruz
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);


            //Redis cache'de token keyi ile authorityleri kontrol ediyoruz
            List<RedisValue> authorityValues= _cacheAuthorityService.CheckCacheAuthority(token);

            // "Delete" authority'si var mı kontrol ediyoruz
            if (!authorityValues.Contains("Delete"))
            {
                throw new UnauthorizedAccessException();
            }

            ValidateId(id);
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
            ValidateId(id);

            CheckIfUserExistsById(id);

            User new_user = _userRepository.GetUserById(id);
            UserResponse response = _mapper.Map<UserResponse>(new_user);
            return response;
        }

        public List<UserResponse> GetUsersByRole(long roleId)
        {
            ValidateId(roleId);
            
            _roleService.CheckIfRoleExistsById(roleId);

            List<User> users = _userRepository.GetUsersByRole(roleId);
            List<UserResponse> response = users.Select(u => _mapper.Map<UserResponse>(u)).ToList();
            return response;
        }

        public UserResponse UpdateUser(UpdateUserRequest userdto)
        {
            // Tokeni headerdan çekiyoruz
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);

            List<RedisValue> authorityValues = _cacheAuthorityService.CheckCacheAuthority(token);

            // "Update" authority'si var mı kontrol ediyoruz
            if (!authorityValues.Contains("Update"))
            {
               throw new UnauthorizedAccessException();
            }

            _ = userdto ?? throw new ArgumentNullException(nameof(userdto), " cannot be null.");
            
            CheckIfUserExistsById(userdto.Id);

            User o_user = _userRepository.GetUserById(userdto.Id);
            User user = _mapper.Map<User>(userdto);
            user.Password = _hashService.HashPassword(userdto.Password);
            user.CreatedAt = o_user.CreatedAt;
            user.RoleId = o_user.RoleId;
            User new_user = _userRepository.UpdateUser(user);
            UserResponse response = _mapper.Map<UserResponse>(new_user);
            return response;
        }

        public User UpdateUserPatch(int id, JsonPatchDocument<PatchUserRequest> patchdoc)
        {
            // Tokeni headerdan çekiyoruz
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);

            List<RedisValue> authorityValues = _cacheAuthorityService.CheckCacheAuthority(token);

            // "Update" authority'si var mı kontrol ediyoruz
            if (!authorityValues.Contains("Update"))
            {
                throw new UnauthorizedAccessException();
            }



            ValidateId(id);
            
            CheckIfUserExistsById(id);
            User user = _userRepository.GetUserById(id);
            


            var password = user.Password;
            long roleid = user.RoleId;

            PatchUserRequest userDTO = _mapper.Map<PatchUserRequest>(user);

            patchdoc.ApplyTo(userDTO);

            user = _mapper.Map<User>(userDTO);
            user.Password=password;
            user.RoleId =roleid;
            

            return _userRepository.UpdateUser(user);
        }

        public string GenerateVerificationToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(3));
            

            return token;
        }

        public void SendVerificationEmail(User user)
        {
            _ = user ?? throw new ArgumentNullException(nameof(user), " cannot be null.");

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
            if (!new EmailAddressAttribute().IsValid(mail))
            {
                throw new ArgumentException("Invalid email format.", nameof(mail));
            }

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

        
        public UserResponse UpdateUserRole(long id)
        {
            // Tokeni headerdan çekiyoruz
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);

            List<RedisValue> authorityValues = _cacheAuthorityService.CheckCacheAuthority(token);

            // "Update" authority'si var mı kontrol ediyoruz
            if (!authorityValues.Contains("Update"))
            {
                throw new UnauthorizedAccessException();
            }

            CheckIfUserExistsById(id);
            var roleID = _roleRepository.GetAllRoles().Where(r => r.Name == "admin").FirstOrDefault().Id;
            User user = _userRepository.GetUserById(id);
            user.RoleId = roleID;
            user = _userRepository.ChangeRoleToAdmin(user);
            UserResponse response = _mapper.Map<UserResponse>(user);
            return response;
        }

        //BusinessRules
        public void CheckIfUserExistsByEmail(string email)
        {
            if (_userRepository.UserExistsByEmail(email))
            {
                throw new UserAlreadyExistsException("User already exists.");
            }
        }

        public void CheckIfUserExistsById(long id)
        {
            if (!_userRepository.UserExistsById(id))
            {
                throw new UserNotFoundException("User cannot be found.");
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
