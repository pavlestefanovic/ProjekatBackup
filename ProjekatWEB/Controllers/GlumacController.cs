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
    public class GlumacController : ControllerBase
    {
        
        ProdajaDiskovaContext Context { get; set; }

        public GlumacController(ProdajaDiskovaContext context){
            Context = context;
        }
        [Route("PreuzmiGlumce/{ime}/{prezime}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiGlumce(string ime,string prezime){
            try{
                if(await Context.Glumci.Where(p=>p.Ime == ime && p.Prezime == prezime).ToListAsync() == null)
                    return Ok("0");
                return Ok(await Context.Glumci.Where(p=>p.Ime == ime && p.Prezime == prezime).ToListAsync());
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }


        [Route("DodajGlumca/{ime}/{prezime}")]
        [HttpPost]
        public async Task<ActionResult> DodajGlumca(string ime, string prezime){
            try{

                Glumac glumac = new Glumac{
                    Ime = ime,
                    Prezime = prezime
                };
                Context.Glumci.Add(glumac);
                await Context.SaveChangesAsync();
                return Ok("Glumac je dodat");
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }
       
        [Route("PreuzmiFilmoveGlumca/{strana}/{glumacIme}/{glumacPrezime}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiFilmoveGlumca(int strana,string glumacIme,string glumacPrezime){
            try{
                
                
                var filmoviGlumca = Context.Glumci
                .Include(p=>p.Filmovi)
                .ThenInclude(p=>p.Filmovi)
                .Where(p=>p.Ime == glumacIme && p.Prezime == glumacPrezime);
                //.ThenInclude(e=>e.);
                return Ok(await filmoviGlumca.Select(p=> new {
                    s = p.Filmovi.Select(q=>new {
                        filmoviId = q.Filmovi.Naziv,
                        filmoviNaziv = q.Filmovi.Naziv,
                        filmoviOcena = q.Filmovi.Ocena,
                        filmoviZanr = q.Filmovi.Zanr
                    })
                }).Skip(strana*10).Take(10).ToListAsync());

            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }


        [Route("DodajUlogu/{filmId}/{glumacId}/{uloga}")]
        [HttpPost]
        public async Task<ActionResult> DodajUlogu(int filmId,int glumacId,string uloga){
            try{
                var film = await Context.Filmovi.Where(p=>p.ID == filmId).FirstOrDefaultAsync();
                var glumac = await Context.Glumci.Where(p=>p.ID == glumacId).FirstOrDefaultAsync();

                Spoj novaUloga = new Spoj{
                    Filmovi = film,
                    Glumci = glumac,
                    Uloga = uloga
                };

                Context.FilmoviGlumci.Add(novaUloga);
                await Context.SaveChangesAsync();
                return Ok("Uloga dodata");

            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("UkloniUlogu/{ulogaId}")]
        [HttpDelete]
        public async Task<ActionResult> UkloniUlogu(int ulogaId){
            try{
                var uloga = await Context.FilmoviGlumci.FindAsync(ulogaId);
                Context.FilmoviGlumci.Remove(uloga);
                await Context.SaveChangesAsync();
                return Ok("Uloga uklonjena");
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

    }
}
