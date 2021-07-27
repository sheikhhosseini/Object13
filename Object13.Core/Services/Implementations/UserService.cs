using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Object13.Core.Services.Interfaces;
using Object13.DataLayer.Models.Account;
using Object13.DataLayer.Repository;

namespace Object13.Core.Services.Implementations
{
    public class UserService:IUserService
    {
        #region Ctor
        private IGenericRepository<User> _userRepository;
        public UserService(IGenericRepository<User> useRepository)
        {
            _userRepository = useRepository;
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
    }
}
