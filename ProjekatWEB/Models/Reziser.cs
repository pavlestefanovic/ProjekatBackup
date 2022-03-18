
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{

    [Table("Reziser")]
    public class Reziser{

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Ime { get; set; }
        [Required]
        [MaxLength(20)]
        public string Prezime { get; set; }
        [JsonIgnore]
        public List<Film> Filmovi { get; set; }
    }
}