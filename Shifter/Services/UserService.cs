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
            throw new NotImplementedException();
        }

        public User Create(User user, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User Update(User user, string password = null)
        {
            throw new NotImplementedException();
        }
    }
}
