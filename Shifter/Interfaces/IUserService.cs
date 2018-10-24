using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shifter
{
    public interface IUserService
    {
        Model.User Authenticate(string emailAddress, string password);
        Model.User GetById(int id);
        Model.User Create(Model.User user, string password);
        Model.User Update(Model.User user, string password = null);
        void Delete(Model.User user);
    }
}
