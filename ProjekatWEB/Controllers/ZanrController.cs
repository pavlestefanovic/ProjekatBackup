using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace ProjekatWEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZanrController : ControllerBase
    {
        
        ProdajaDiskovaContext Context { get; set; }

        public ZanrController(ProdajaDiskovaContext context){
            Context = context;
        }
        
        [Route("PreuzmiZanr")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiZanr(){
            try{
                return Ok(await Context.Zanrovi.Select(p => new {p.ID,p.Naziv}).ToListAsync());
            }catch(Exception e ){
                return BadRequest(e.Message);
            }
       }
        
    }
}
