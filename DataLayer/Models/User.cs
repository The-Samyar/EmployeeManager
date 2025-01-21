using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public enum UserType
    {
        Employer,
        Employee,
        Admin
    }

    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserType UserType { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Employer Employer { get; set; }
    }
}
