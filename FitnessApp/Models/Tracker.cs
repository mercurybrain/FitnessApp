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
        public float b { get; set; }
        public float g { get; set; }
        public float u { get; set; }
        public float CaloriesInTake { get; set; }
    }
}
