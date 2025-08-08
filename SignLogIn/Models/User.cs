using SQLite;
using System.ComponentModel.DataAnnotations.Schema;
namespace SignLogIn.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; } // Optional: User profile image
        

    }
}
