using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Text;
using Microsoft.EntityFrameworkCore;
using Object13.Core.DTOs.Account;
using Object13.Core.Email;
using Object13.Core.Security;
using Object13.Core.Services.Interfaces;
using Object13.DataLayer.Models.Account;
using Object13.DataLayer.Repository;

namespace Object13.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        #region Ctor
        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IMailSender _mailSender;
        private readonly IViewRenderService _renderViewService;

        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper, IMailSender mailSender, IViewRenderService renderService)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _mailSender = mailSender;
            _renderViewService = renderService;
        }
        #endregion

        public void Dispose()
        {
            _userRepository?.Dispose();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<UserRegisterDtoResult> RegisterUser(UserRegisterDto newUser)
        {
            if (await IsUserExistByEmail(newUser.Email))
            {
                return UserRegisterDtoResult.EmailExist;
            }

            var user = new User
            {
                Email = newUser.Email.SanitizeText(),
                Password = _passwordHelper.EncodePasswordMd5(newUser.Password),
                FirstName = newUser.FirstName.SanitizeText(),
                LastName = newUser.LastName.SanitizeText(),
                MobileNumber = newUser.MobileNumber.SanitizeText(),
                Gender = newUser.Gender.SanitizeBool(),
                EmailActiveCode = Guid.NewGuid().ToString()
            };
            await _userRepository.AddEntity(user);
            // await _userRepository.SaveChanges();

            try
            {
                var body = await _renderViewService.RenderToStringAsync("Email/ActivateAccount", null);
                _mailSender.Send("saeed779911@gmail.com", "تست فعال سازی ", body);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

            return UserRegisterDtoResult.Success;
        }

        public async Task<bool> IsUserExistByEmail(string email)
        {
            return await _userRepository.GetEntitiesQuery().AnyAsync(u=>u.Email == email.ToLower().Trim());
        }

        public async Task<UserLoginDtoResult> LoginUser(UserLoginDto newUser)
        {
            var password = _passwordHelper.EncodePasswordMd5(newUser.Password);
            var user = await _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(u =>
                u.Email == newUser.Email.ToLower().Trim()
                && u.Password == password);

            if (user == null)
            {
                return UserLoginDtoResult.Failed;
            }

            if (!user.IsActivated)
            {
                return UserLoginDtoResult.NotAcctive;
            }

            return UserLoginDtoResult.Success;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(u =>
                u.Email == email.ToLower().Trim());
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _userRepository.GetEntityById(userId);
        }
    }
}
