using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.Core.DTOs.Account;
using Object13.DataLayer.Models.Account;

namespace Object13.Core.Services.Interfaces
{
    public interface IUserService :IDisposable
    {
        Task<List<User>> GetAllUsers();
        Task<UserRegisterDtoResult> RegisterUser(UserRegisterDto newUser);
        Task<bool> IsUserExistByEmail(string email);
        Task<UserLoginDtoResult> LoginUser(UserLoginDto newUser);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(long userId);
        Task<User> GetUserByActiveCode(string activeCode);
        Task ActivateUser(User user);
    }
}
