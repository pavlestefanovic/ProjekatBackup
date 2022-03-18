using Microsoft.EntityFrameworkCore;

namespace Models
{
    

    public class ProdajaDiskovaContext : DbContext{


        public DbSet<Film> Filmovi { get; set; }
        public DbSet<Glumac> Glumci { get; set; }
        public DbSet<Reziser> Reziseri { get; set; }
        public DbSet<Spoj> FilmoviGlumci { get; set; }
        public DbSet<Zanr> Zanrovi { get; set; }
        public DbSet<Katalog> Katalog { get; set; }
        public ProdajaDiskovaContext(DbContextOptions options) : base(options){

        }



    }
}