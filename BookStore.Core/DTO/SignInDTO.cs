using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.DTO
{
	public class SignInDTO
	{
		[Required(ErrorMessage = "Username is required")]
		[StringLength(100, MinimumLength = 5, ErrorMessage = "userName must be at least 5 characters long.")]
		public string UserName { get; set; }


		[Required(ErrorMessage = "password is required")]
		[DataType(DataType.Password)]
		[StringLength(100, MinimumLength = 5, ErrorMessage = "Password must be at least 5 characters long.")]
		public string Password { get; set; }


		public bool RememberMe { get; set; } = true;
    }
}
