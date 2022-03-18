import { Katalog } from "./Katalog.js";
import { Film } from "./Film.js";
import { Zanr } from "./Zanr.js";
import { Reziser } from "./Reziser.js";


var listaZanrova = [];

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
var katalog = new Katalog(listaZanrova,brStrana);

    katalog.Crtaj(document.body);

