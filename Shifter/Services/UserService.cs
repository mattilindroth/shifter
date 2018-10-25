using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shifter.Model;

namespace Shifter.Services
{
    public class UserService : IUserService
    {
        private DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public User Authenticate(string emailAddress, string password)
        {
            if(string.IsNullOrEmpty(emailAddress) ||string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _dataContext.Users.SingleOrDefault(x => x.EmailAddress == emailAddress);

            if (user == null)
                return null;
            byte[] passwordHashBytes = System.Text.Encoding.UTF8.GetBytes(user.PasswordHash);
            byte[] passwordSaltBytes = System.Text.Encoding.UTF8.GetBytes(user.PasswordSalt);
            if (!VerifyPasswordHash(password, passwordHashBytes, passwordSaltBytes))
                return null;

            return user;
        }

        public User Create(User user, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = System.Text.Encoding.UTF8.GetString(passwordHash);
            user.PasswordSalt = System.Text.Encoding.UTF8.GetString(passwordSalt);

            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
            return user;
        }

        List<Model.User> GetActiveUsersForOrganization(int OrganizationId)
        {
            return _dataContext.Users.Where(x => x.Organization.Id == OrganizationId && x.IsDeleted == false).ToList();
        }

        public void Delete(User user)
        {
            
        }

        public User GetById(int id)
        {
            return _dataContext.Users.SingleOrDefault(x => x.Id == id);
        }

        public User Update(User user, string password = null)
        {
            throw new NotImplementedException();
        }


        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            if (Password == null) throw new ArgumentNullException(nameof(Password));
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Value cannot be empty or whitespace only.", nameof(Password));

            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }

        private bool VerifyPasswordHash(string Password, byte[] storedHash, byte[] storedSalt)
        {
            if (Password == null) throw new ArgumentNullException(nameof(Password));
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Value cannot be empty or whitespace only.", nameof(Password));
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(storedSalt));

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
