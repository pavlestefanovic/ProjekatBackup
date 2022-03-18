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
    public class ReziserController : ControllerBase
    {
        
        ProdajaDiskovaContext Context { get; set; }

        public ReziserController(ProdajaDiskovaContext context){
            Context = context;
        }

        [Route("PreuzmiRezisera")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiRezisera(){
            try{
                return Ok(await Context.Reziseri.Select(p => new {p.ID,p.Ime,p.Prezime}).ToListAsync());
            }catch(Exception e ){
                return BadRequest(e.Message);
            }
       }
        
    }
}
