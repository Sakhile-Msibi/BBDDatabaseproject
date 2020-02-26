using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Website.Models
{

    public class UserModel
    {
        [Key] //sets itself on creation counting upwards
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,50}$", 
         ErrorMessage = "Password must meet requirements: lowercase, capital, digit, 8+ length")]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        public bool Admin { get; set; }

        /* public static bool EmailExists(string Email, MyDbContext _db)
        {
            var user = _db.Users.FirstOrDefault(p => p.Email == Email);
            if (user == null) return false;
            return true;
        } */
    }
}