import { Prodavnica } from "./Prodavnica.js";
import { Film } from "./Film.js";
import { Zanr } from "./Zanr.js";
import { Reziser } from "./Reziser.js";


var listaZanrova = [];
var listaRezisera = [];

await fetch("http://localhost:5001/Zanr/PreuzmiZanr")
.then( p => {
    p.json().then( zanrovi => {
        zanrovi.forEach(zanr => {
            var a = new Zanr(zanr.id,zanr.naziv);
            listaZanrova.push(a);
            
        });

    })
})



var brStrana = await fetch("http://localhost:5001/Film/IzbrojiFilmove").then(p => p.json());

console.log(brStrana);
var prodavnica1 = new Prodavnica(id,naziv,listaZanrova,brStrana);

    prodavnica1.Crtaj(document.body);




var prodavnica2 = new Prodavnica(listaZanrova,listaRezisera,brStrana);
prodavnica2.Crtaj(document.body);