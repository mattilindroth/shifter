using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shifter.Interfaces;
using Shifter.Model;

namespace Shifter.Services
{
    public class UserAccessService : IUserAccessService
    {
        private DataContext _dataContext;
        public UserAccessService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public AccessRight CreateAccessRight(AccessRight accessRight)
        {
            _dataContext.AccessRight.Add(accessRight);
            _dataContext.SaveChanges();
            return accessRight;
        }

        public AccessRightsGroup CreateAccessRightsGroup(AccessRightsGroup group)
        {
            _dataContext.AccessRightsGroup.Add(group);
            _dataContext.SaveChanges();
            return group;
        }

        public ICollection<AccessRight> GetAccessRightsForGroup(AccessRightsGroup accessRightGroup)
        {
            var rights = from ar in _dataContext.AccessRight where ar.AccessRightGroup.Id == accessRightGroup.Id select ar;
            return rights.ToList();
        }

        public ICollection<AccessRightsGroup> GetAccessRightsGroups(int organizationId)
        {
            var groups = from arg in _dataContext.AccessRightsGroup where arg.Organization.Id == organizationId select arg;
            return groups.ToList();
        }

        public ICollection<User> GetAccessRightsGroupUsers(AccessRightsGroup accessRightGroup)
        {
            var users = from u in _dataContext.Users where u.AccessRightsGroup.Id == accessRightGroup.Id select u;
            return users.ToList();
        }

        public AccessRightTemplate GetAccessRightTemplateById(int id)
        {
            return _dataContext.AccessRightTemplate.Where(x => x.Id == id).SingleOrDefault();
        }

        public ICollection<AccessRightTemplate> GetAccessRightTemplates()
        {
            var templates = from art in _dataContext.AccessRightTemplate select art;
            return templates.ToList();
        }

        public AccessRightsGroup GetById(int id)
        {
            return _dataContext.AccessRightsGroup.Where(x => x.Id == id).SingleOrDefault();
        }

        public void RemoveAccessRight(AccessRight accessRight)
        {
            _dataContext.AccessRight.Remove(accessRight);
            _dataContext.SaveChanges();
        }

        public void RemoveAccessRightsGroup(AccessRightsGroup group)
        {
            _dataContext.AccessRightsGroup.Remove(group);
            _dataContext.SaveChanges();
        }

        public bool UserHasAccess(User user, AccessRightTemplate accessRightTemplate)
        {
            return true;
        }
    }
}
