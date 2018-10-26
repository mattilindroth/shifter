using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shifter.Model;

namespace Shifter.Interfaces
{
    interface IUserAccessService
    {
        bool UserHasAccess(User user, AccessRightTemplate accessRightTemplate);
        
        //Access rights group management
        ICollection<AccessRightsGroup> GetAccessRightsGroups(int companyId);
        AccessRightsGroup CreateAccessRightsGroup(AccessRightsGroup group);
        void RemoveAccessRightsGroup(AccessRightsGroup group);
        ICollection<User> GetAccessRightsGroupUsers(AccessRightsGroup group);
        AccessRightsGroup GetById(int id);

        //AccessRightTemplates
        ICollection<AccessRightTemplate> GetAccessRightTemplates();
        AccessRightTemplate GetAccessRightTemplateById(int id);

        //Access rights 
        ICollection<AccessRight> GetAccessRightsForGroup(AccessRightsGroup group);
        AccessRight CreateAccessRight(AccessRight accessRight);
        void RemoveAccessRight(AccessRight accessRight);

    }
}
