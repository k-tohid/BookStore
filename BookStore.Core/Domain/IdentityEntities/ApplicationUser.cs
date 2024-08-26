using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Domain.IdentityEntities
{
	public class ApplicationUser : IdentityUser<int>
	{
		public string? Address { get; set; }
	}
}
