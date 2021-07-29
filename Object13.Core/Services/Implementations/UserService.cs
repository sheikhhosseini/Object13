using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Object13.Core.DTOs.Account;
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

        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
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
                Age = newUser.Age,
                Password = _passwordHelper.EncodePasswordMd5(newUser.Password),
                FirstName = newUser.FirstName.SanitizeText(),
                LastName = newUser.LastName.SanitizeText(),
                MobileNumber = newUser.MobileNumber.SanitizeText(),
                Gender = newUser.Gender,
                EmailActiveCode = Guid.NewGuid().ToString()
            };
            await _userRepository.AddEntity(user);
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
    }
}
