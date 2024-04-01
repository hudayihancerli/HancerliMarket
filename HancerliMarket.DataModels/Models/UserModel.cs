using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HancerliMarket.DataModels.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;
        public DateTime BirthDay { get; set; } = new DateTime(2001, 01, 20);
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExiprationDate { get; set; }
    }

}
