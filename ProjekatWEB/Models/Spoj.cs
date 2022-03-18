
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{

    [Table("Spoj")]
    public class Spoj{

        [Key]
        public int ID { get; set; }

        public string Uloga { get; set; }
        
        [JsonIgnore]
        public Film Filmovi { get; set; }
        [JsonIgnore]
        public Glumac Glumci { get; set; } 
    }
}