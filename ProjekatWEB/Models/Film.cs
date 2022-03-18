

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Models{
    [Table("Film")]
    public class Film{

        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }

        [Range(0,10)]
        public float Ocena { get; set; }
        
        [JsonIgnore]
        public List<Spoj> FilmGlumac { get; set; }

        [JsonIgnore]
        public Zanr Zanr { get; set; }
        [JsonIgnore]
        public Reziser Reziser { get; set; }

    }

}