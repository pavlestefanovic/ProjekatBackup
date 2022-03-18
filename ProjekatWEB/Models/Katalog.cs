using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models{

    [Table("Katalog")]
    public class Katalog{

        [Key]
        public int ID { get; set; }
        public string naziv { get; set; }
        public List<Spoj> Filmovi { get; set; }


    }
}