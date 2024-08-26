using BookStore.Core.Domain.IdentityEntities;
using BookStore.Core.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces.Services
{
    public interface IUserAccountService
    {
        /// <summary>
        /// Registers a new user with the specified registration details.
        /// </summary>
        /// <param name="registerDTO">The registration details.</param>
        /// <returns>An IdentityResult indicating the outcome of the registration.</returns>
        Task<IdentityResult> RegisterUserAsync(RegisterDTO registerDTO);

        /// <summary>
        /// Signs in the specified user.
        /// </summary>
        /// <param name="user">The user to sign in.</param>
        /// <param name="isPersistent">Whether the sign-in should be persistent.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task SignInUserAsync(ApplicationUser user, bool isPersistent = true);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInDTO">The user info for sign in with password</param>
        /// <returns>returns a SignInResult indicaiting the outcome of sign in.</returns>
        Task<SignInResult> SignInWithPasswordAsync(SignInDTO signInDTO);


        /// <summary>
        /// Finds a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        Task<ApplicationUser?> FindByEmailAsync(string email);

        /// <summary>
        /// Finds a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        Task<ApplicationUser?> FindByUserNameAsync(string userName);

        /// <summary>
        /// Signs out the current user.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task SignOutAsync();

        /// <summary>
        /// Changes the password for the specified user.
        /// </summary>
        /// <param name="user">The user whose password is to be changed.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>An IdentityResult indicating the outcome of the operation.</returns>
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);

        /// <summary>
        /// Confirms the email address of a user.
        /// </summary>
        /// <param name="user">The user whose email is to be confirmed.</param>
        /// <param name="token">The confirmation token.</param>
        /// <returns>An IdentityResult indicating the outcome of the operation.</returns>
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);

        /// <summary>
        /// Generates an email confirmation token for a user.
        /// </summary>
        /// <param name="user">The user for whom to generate the token.</param>
        /// <returns>The email confirmation token.</returns>
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        /// <summary>
        /// Resets the password for the specified user.
        /// </summary>
        /// <param name="user">The user whose password is to be reset.</param>
        /// <param name="token">The reset token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>An IdentityResult indicating the outcome of the operation.</returns>
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);

        /// <summary>
        /// Generates a password reset token for a user.
        /// </summary>
        /// <param name="user">The user for whom to generate the token.</param>
        /// <returns>The password reset token.</returns>
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);

    }
}
