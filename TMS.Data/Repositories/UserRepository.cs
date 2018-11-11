using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TMS.Entities;
using TMS.Interfaces;

namespace TMS.Data.Repositories
{
    public class UserRepository : IRepositoryAsync<UserApp>
    {
        private readonly UserManager<UserApp> _userManager;
        public UserRepository(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        public System.Threading.Tasks.Task CreateAsync(UserApp item)
        {
            return _userManager.CreateAsync(item);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public System.Threading.Tasks.Task UpdateAsync(UserApp item)
        {
            return _userManager.UpdateAsync(item);
        }

        public async Task<UserApp> FindAsync(Expression<Func<UserApp, bool>> predicate)
        {
            return await _userManager.Users.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<UserApp>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public Task<UserApp> GetItemAsync(int id)
        {
            return _userManager.FindByIdAsync(id.ToString());
        }

        public System.Threading.Tasks.Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
