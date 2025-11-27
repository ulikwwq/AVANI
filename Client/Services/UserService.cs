using System;
using System.Threading.Tasks;
using AVANI.Client.Models;

namespace AVANI.Client.Services
{
    /// <summary>
    /// Simple in-memory holder for the current user on the client.
    /// Use `SetCurrentUser` to set the active user and `GetCurrentUser` to retrieve it.
    /// </summary>
    public class UserService
    {
        private UserModel? _currentUser;

        public UserModel? CurrentUser => _currentUser;

        public Task SetCurrentUser(UserModel user)
        {
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            return Task.CompletedTask;
        }

        public Task<UserModel?> GetCurrentUser()
        {
            return Task.FromResult(_currentUser);
        }

        public bool IsParent()
        {
            return string.Equals(_currentUser?.Role, "Parent", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsChild()
        {
            return string.Equals(_currentUser?.Role, "Child", StringComparison.OrdinalIgnoreCase);
        }

        // Backwards-compatible async getter
        public Task<UserModel?> GetCurrentUserAsync() => GetCurrentUser();

        public Task SignInAsync(string email, string password)
        {
            // TODO: authenticate against backend or Firebase and call SetCurrentUser
            return Task.CompletedTask;
        }
    }
}