using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FitnessApp.Models
{
    [Table("BodyMetrics")]
    public class BodyMetrics
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Column("gender")]
        public int Gender { get; set; }
        [Column("height")]
        public double HeightCm { get; set; }
        [Column("weight")]
        public double Weight { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("goal_weight")]
        public double GoalWeight { get; set; }
        [Column("activity_level")]
        public double ActivityLevel { get; set; }
        [Column("tdee")]
        public double TDEE { get; set; }
        [Column("bmr")]
        public double BMR { get; set; }
        [ForeignKey(typeof(User))]
        public string Username { get; set; }
    }
}
