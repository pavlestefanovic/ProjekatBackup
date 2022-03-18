using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace ProjekatWEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        
        ProdajaDiskovaContext Context { get; set; }

        public FilmController(ProdajaDiskovaContext context){
            Context = context;
        }
        [Route("PreuzmiRezisera/{filmId}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiRezisera(int filmId){
            try{
                return Ok(await Context.Filmovi.Where(p=>p.ID == filmId).Include(p=>p.Reziser).FirstOrDefaultAsync());
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }
       
        [Route("IzbrojiFilmove")]
        [HttpGet]
        public ActionResult IzbrojiFilmove(){
           return Ok(Context.Filmovi.Count());
        }
        [Route("IzbrojiFilmovePretraga/{naziv}")]
        [HttpGet]
        public ActionResult IzbrojiFilmovePretraga(string naziv){
            if(naziv == "" )
                    return BadRequest("greska pri unosu");
           return Ok(Context.Filmovi.Where(p=>p.Naziv.Contains(naziv)).Count());
        }
        [Route("Pretraga/{naziv}")]
        [HttpGet]
        public async Task<ActionResult> Pretraga(string naziv){
            try{
                if(naziv == "" )
                    return BadRequest("greska pri unosu");
                var filmovi = Context.Filmovi.Where(p=>p.Naziv.Contains(naziv))
                .Include(p=>p.Zanr);
                return Ok(await filmovi.ToListAsync());
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("PreuzmiStranu/{strana}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiStranu(int strana){
            try{
                if(strana < 0)
                    return BadRequest("greska pri unosu");
            var Filmovi = Context.Filmovi
            .Include(p => p.Zanr)
            .Include(p => p.Reziser);

            return Ok(await Filmovi.Skip(strana*10).Take(10).ToListAsync()/*await Filmovi.OrderByDescending(p => p.ID).Skip(strana*10).Take(10).ToListAsync()*/);
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("PreuzmiGlumceUloge/{filmId}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiGlumceUloge(int filmId){
            try{
                if(filmId < 0)
                    return BadRequest("greska pri unosu");
                var UlogeGlumci = Context.Filmovi
                .Include( p => p.FilmGlumac)
                .ThenInclude(p=>p.Glumci)
                .Where(p=>p.ID == filmId);

                return Ok(await UlogeGlumci.Select(p=> new{
                    filmID = p.ID,
                    filmNaziv = p.Naziv,
                    filmReziser = p.Reziser,
                    
                    spojevi = p.FilmGlumac.Select( q=> new {
                        uloga = q.Uloga,
                        spojId = q.ID,
                        glumacId = q.Glumci.ID,
                        glumacIme = q.Glumci.Ime,
                        glumacPrezime = q.Glumci.Prezime
                        
                        
                    })
                }).ToListAsync()
                );
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        



        [Route("DodajFilm/{naziv}/{ocena}/{zanrId}")]
        [HttpPost]
        public async Task<ActionResult> DodajFilm(string naziv,float ocena,int zanrId){
            
            try{
                if(naziv == "" || ocena < 1 || ocena > 10  || zanrId < 0)
                    return BadRequest("greska pri unosu");
                var zanr = Context.Zanrovi.Where(p => p.ID == zanrId).FirstOrDefault();
                Film film = new Film {
                    Naziv = naziv,
                    Ocena = ocena,
                    Zanr = zanr
                };

                Context.Filmovi.Add(film);
                await Context.SaveChangesAsync();
                return Ok("Film sacuvan u bazu");

            }catch(Exception e){
                return BadRequest(e.Message);
            }

        }

        [Route("IzmeniFilm/{id}/{naziv}/{ocena}/{zanrId}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniFilm(int id,string naziv,float ocena,int zanrId){
            
            try{
                if(naziv == "" || id < 0 || ocena < 1 || ocena > 10 ||zanrId < 0)
                    return BadRequest("greska pri unosu");

                var film = Context.Filmovi.Where(p=>p.ID == id).FirstOrDefault();

                if(film != null){
                    film.Naziv = naziv;
                    film.Ocena = ocena;
                    var zanr = Context.Zanrovi.Where(p=>p.ID == zanrId).FirstOrDefault();
                    film.Zanr = zanr;

                    await Context.SaveChangesAsync();
                    return Ok("Film je izmenjen");
                }else{
                    return BadRequest("Nije uspelo trazenje filma");
                }
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("ObrisiFilm/{filmId}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiFilm(int filmId){
            try{
                if(filmId < 0 )
                    return BadRequest("greska pri unosu");
                var film = await Context.Filmovi.FindAsync(filmId);
                Context.Filmovi.Remove(film);
                await Context.SaveChangesAsync();
                return Ok("Film je izbrisan");
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}
