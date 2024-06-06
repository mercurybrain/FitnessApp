using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FitnessApp.Models
{
    [Table("CaloriesTracker")]
    public class Tracker
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(User))]
        public string Username { get; set; }
        public DateTime DateTrack { get; set; }
        public double b { get; set; }
        public double g { get; set; }
        public double u { get; set; }
        public int CaloriesInTake { get; set; }
    }
}
