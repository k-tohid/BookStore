using BookStore.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces.Services
{
    public interface IUserRoleService
    {
        /// <summary>
        /// Assigns the Default Role to User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IdentityResult> AssignDefaultRoleAsync(ApplicationUser user);
    }
}
