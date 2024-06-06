using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FitnessApp.Models
{
    public class Calories
    {
        // Calories myDeserializedClass = JsonConvert.DeserializeObject<List<Calories>>(myJsonResponse);
        public string id { get; set; }
        public string name { get; set; }
        public string bgu { get; set; }
        public string kcal { get; set; }

        public string b  => bgu.Split(',')[0];
        public string g => bgu.Split(',')[1];
        public string u => bgu.Split(',')[2];
    }
    [JsonSerializable(typeof(List<Calories>))]
    internal sealed partial class CaloriesContext : JsonSerializerContext
    {

    }
}
