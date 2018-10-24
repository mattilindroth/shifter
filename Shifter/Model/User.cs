using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shifter.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public AccessRightsGroup AccessRightsGroup { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string PasswordSatl { get; set; }
    }
}
