using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object13.DataLayer.Models.Account;

namespace Object13.Core.Services.Interfaces
{
    public interface IUserService :IDisposable
    {
        Task<List<User>> GetAllUsers();
    }
}
