using SQLite;
using SQLiteNetExtensions.Attributes;
namespace FitnessApp.Models
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey]
        [Column("username")]
        public string Username { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("pass_hash")]
        public string PasswordHash { get; set; }
        [Column("calories_goal")]
        public int CaloriesGoal { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public BodyMetrics bms { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Tracker tracker {get; set;}
    }
}
