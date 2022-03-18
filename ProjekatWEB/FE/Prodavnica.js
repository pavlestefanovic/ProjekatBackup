import { Film } from "./Film.js";
export class Prodavnica {


    constructor(id,nazivlistaZanrova,ukupnoFilmova){
        this.listaZanrova = listaZanrova;
        this.id = id;
        this.naziv = naziv;
        this.nazivlistaZanrova = nazivlistaZanrova;
        this.ukupnoFilmova = ukupnoFilmova; //promeni kad dodajes
        this.izabraniFilm = null;

        this.trenutnaStrana = 0;
        this.kontejner = null;
    }


    Crtaj(host){
        if(!host)
            throw new Error("Nije unet host!");

        this.kontejner = document.createElement("div");
        this.kontejner.className = "kontejner";
        host.appendChild(this.kontejner);

        this.CrtajDodavanjeBrisanje(this.kontejner);



        let tabelaElementiDiv = document.createElement("div");
        tabelaElementiDiv.className = "tabelaElementi";
        this.kontejner.appendChild(tabelaElementiDiv);

        this.CrtajPretragu(tabelaElementiDiv);
        this.CrtajFilmoveGlumca(tabelaElementiDiv);

        this.CrtajTabelu(tabelaElementiDiv);

        this.PreuzmiFilmove(this.trenutnaStrana);
        this.CrtajTabeluFo(tabelaElementiDiv);
        


    }

    DodajGlumca(host){
        if(!host)
            throw new Error("nije unet host!");
        
        let dodajGlumcaDiv = document.createElement("div");
        dodajGlumcaDiv.className = "dodajGlumcaDiv";
        host.appendChild(dodajGlumcaDiv);

        let dodajGlumcaLab = document.createElement("h4");
        dodajGlumcaLab.innerHTML = "Dodaj Glumca";
        dodajGlumcaDiv.appendChild(dodajGlumcaLab);
        //IME
        let imeDiv = document.createElement("div");
        imeDiv.className = "imeDiv"
        dodajGlumcaDiv.appendChild(imeDiv);

        let imeLab = document.createElement("label");
        imeLab.innerHTML = "Ime ";
        imeDiv.appendChild(imeLab);

        let imeIn = document.createElement("input");
        imeDiv.appendChild(imeIn);
        //PREZIME
        let prezimeDiv = document.createElement("div");
        prezimeDiv.className = "imeDiv"
        dodajGlumcaDiv.appendChild(prezimeDiv);

        let prezimeLab = document.createElement("label");
        prezimeLab.innerHTML = "Prezime ";
        prezimeDiv.appendChild(prezimeLab);

        let prezimeIn = document.createElement("input");
        prezimeDiv.appendChild(prezimeIn);

        let btn = document.createElement("button");
        btn.innerHTML = "Dodaj";
        btn.onclick = (ev) => {
            if(imeIn.value != "" && prezimeIn.value != "" ){
            fetch(`http://localhost:5001/Glumac/DodajGlumca/${imeIn.value}/${prezimeIn.value}`,{method: "POST"})
            }else {alert("Nisu popunjena sva polja")}
        }
        dodajGlumcaDiv.appendChild(btn);

    }

    

    CrtajTabeluFo(host){
        if(!host)
            throw new Error("Nije unet host!");
        let brojacStranicaDiv = document.createElement("div");
        brojacStranicaDiv.className = "brojacStranicaDiv";
        host.appendChild(brojacStranicaDiv);
        if(this.trenutnaStrana > 0){
            let btnLevo = document.createElement("button");
            btnLevo.className = "btnLevo";
            btnLevo.innerHTML = "<";
            brojacStranicaDiv.appendChild(btnLevo);
            btnLevo.onclick = (ev) =>{
                this.trenutnaStrana--;
                //brisanje
                let t = document.querySelector(".tabelaBody");
                while(t.firstChild != null)
                    t.removeChild(t.firstChild);
                document.querySelector(".tabelaElementi").removeChild(document.querySelector(".brojacStranicaDiv"));
                //azuriranje
                this.PreuzmiFilmove(this.trenutnaStrana);
                this.CrtajTabeluFo(document.querySelector(".tabelaElementi"));
            }
        }
        let brojacStranicaBroj = document.createElement("label");
        let c = this.trenutnaStrana + 1;
        brojacStranicaBroj.innerHTML = c;
        brojacStranicaDiv.appendChild(brojacStranicaBroj);
        if(this.trenutnaStrana < Math.floor(this.ukupnoFilmova/10)){
            let btnDesno = document.createElement("button");
            btnDesno.className = "btnDesno";
            btnDesno.innerHTML = ">";
            btnDesno.onclick = (ev) => {
                this.trenutnaStrana++;
                //brisanje
                let t = document.querySelector(".tabelaBody");
                while(t.firstChild)
                    t.removeChild(t.firstChild);
                document.querySelector(".tabelaElementi").removeChild(document.querySelector(".brojacStranicaDiv"));
                //azuriranje
                this.PreuzmiFilmove(this.trenutnaStrana);
                this.CrtajTabeluFo(document.querySelector(".tabelaElementi"));
            }
            brojacStranicaDiv.appendChild(btnDesno);
        }

    }

    CrtajTabelu(host){

        if(!host)
            throw new Error("nije unet host");

        var tabelaDiv = document.createElement("div");
        tabelaDiv.className = "tabelaDiv";
        host.appendChild(tabelaDiv);

        var tabela = document.createElement("table");
        tabela.className = "tabela1";
        tabelaDiv.appendChild(tabela);

        let red = document.createElement("tr");
        tabela.appendChild(red);
    
        var tabelaBody = document.createElement("tbody");
        tabelaBody.className = "tabelaBody";
        tabela.appendChild(tabelaBody);

        let zaglavlje = ["Naziv","Ocena","Zanr"];
        let elem;
        zaglavlje.forEach(e =>{
            elem = document.createElement("th");
            elem.innerHTML = e;
            red.appendChild(elem);
        })
        
    }

    PreuzmiFilmove(str){
        fetch(`http://localhost:5001/Film/PreuzmiStranu/${str}`)
        .then(p => {p.json().then( filmovi=>{
            filmovi.forEach(film => {
                var tab = document.querySelector(".tabelaBody");
                var a = new Film(film.id,film.naziv,film.ocena,film.zanr.naziv,this);
                a.Crtaj(tab);
        
            })
        })
    })
}

    PreuzmiFilmoveGlumca(str,glumacIme,glumacPrezime){

        //uklanjanje prethodnog sadrzaja
        let tab = document.querySelector(".tabelaBody");
                while(tab.firstChild != null)
                    tab.removeChild(tab.firstChild);
        

        fetch(`http://localhost:5001/Glumac/PreuzmiFilmoveGlumca/${str}/${glumacIme}/${glumacPrezime}`)
        .then(p => {p.json().then( filmovi=>{
            filmovi.forEach(film => {
                var a = new Film(film.s[0].filmoviId,
                    film.s[0].filmoviNaziv,
                    film.s[0].filmoviOcena,
                    film.s[0].filmoviZanr.naziv,
                    this
                    );
                a.Crtaj(tab);

               // }else{
                //    console.log("Nije pronadjen ni jedan film");
                //}
        
            })
        })
    })
    }

    CrtajDodavanjeBrisanje(host){
        if(!host)
            throw new Error("Nije unet host!");
        
        let dodavanjeBrisanjeDiv = document.createElement("div");
        dodavanjeBrisanjeDiv.className = "dodavanjeBrisanjeDiv";
        host.appendChild(dodavanjeBrisanjeDiv);

        //DODAVANJE
        let dodavanjeDiv = document.createElement("div");
        dodavanjeDiv.className = "dodavanjeDiv";
        dodavanjeBrisanjeDiv.appendChild(dodavanjeDiv);
            //naslov
        let dodavanjeH = document.createElement("h4");
        dodavanjeH.innerHTML = "Dodaj film";
        dodavanjeDiv.appendChild(dodavanjeH);
            //naziv ocena
        let dodajNazivDiv = document.createElement("div");
        dodavanjeDiv.appendChild(dodajNazivDiv);

        let dodajNazivLab = document.createElement("label");
        dodajNazivLab.innerHTML = "Naziv ";
        dodajNazivDiv.appendChild(dodajNazivLab);

        let dodajNazivIn = document.createElement("input");
        dodajNazivDiv.appendChild(dodajNazivIn);
            //ocena
        let dodajOcenaDiv = document.createElement("div");
        dodavanjeDiv.appendChild(dodajOcenaDiv);

        let dodajOcenaLab = document.createElement("label");
        dodajOcenaLab.innerHTML = "Ocena ";
        dodajOcenaDiv.appendChild(dodajOcenaLab);

        let dodajOcenaIn = document.createElement("input");
        dodajOcenaDiv.appendChild(dodajOcenaIn);

            //zanr
        let dodavanjeZanrDiv = document.createElement("div");

        let dodavanjeZanrLab = document.createElement("label");
        dodavanjeZanrLab.innerHTML = "Zanr ";
        let selekcija = document.createElement("select");
        let opcija;
        this.listaZanrova.forEach(e=>{
            opcija = document.createElement("option");
            opcija.innerHTML = e.naziv;
            opcija.value = e.id;
            selekcija.appendChild(opcija);
        })
        dodavanjeZanrDiv.appendChild(dodavanjeZanrLab);
        dodavanjeZanrDiv.appendChild(selekcija);
        dodavanjeDiv.appendChild(dodavanjeZanrDiv);

            //dugme
        let btn = document.createElement("button");
        btn.innerHTML = "Dodaj";
        btn.onclick = async (ev) =>{
            if(dodajNazivIn.value != "" && dodajOcenaIn != ""){
            await fetch(`http://localhost:5001/Film/DodajFilm/${dodajNazivIn.value}/${dodajOcenaIn.value}/${selekcija.value}`,{
                method: "POST"
            });
            document.body.removeChild(this.kontejner);
            this.Crtaj(document.body);
        }else{alert("Nisu popunjena sva polja")}
        }
        dodavanjeBrisanjeDiv.appendChild(btn);
   
        this.DodajGlumca(dodavanjeBrisanjeDiv);
        
    }

    CrtajFilmoveGlumca(host){
        if(!host)
            throw new Error("Nije unet host!");
        
        let filmoviGlumcaDiv = document.createElement("div");
        filmoviGlumcaDiv.className = "filmoviGlumcaDiv";
        host.appendChild(filmoviGlumcaDiv);
        //Ime
        let imeGlumcaLab = document.createElement("label");
        imeGlumcaLab.innerHTML = "Ime glumca: ";
        filmoviGlumcaDiv.appendChild(imeGlumcaLab);

        let imeGlumcaIn = document.createElement("input");
        imeGlumcaIn.className = "imeGlumcaIn";
        filmoviGlumcaDiv.appendChild(imeGlumcaIn);
        //Prezime
        let prezimeGlumcaLab = document.createElement("label");
        prezimeGlumcaLab.innerHTML = "Prezime glumca: ";
        filmoviGlumcaDiv.appendChild(prezimeGlumcaLab);

        let prezimeGlumcaIn = document.createElement("input");
        prezimeGlumcaIn.className = "PrezimeGlumcaIn";
        filmoviGlumcaDiv.appendChild(prezimeGlumcaIn);

        let btn = document.createElement("button");
        btn.innerHTML = "Pronadji";
        btn.onclick = (ev) =>{
            if(imeGlumcaIn.value != "" && prezimeGlumcaIn.value != "" ){
            this.trenutnaStrana = 0;
            this.ukupnoFilmova = 0;
            document.querySelector(".tabelaElementi").removeChild(document.querySelector(".brojacStranicaDiv"));
            this.PreuzmiFilmoveGlumca(this.trenutnaStrana,imeGlumcaIn.value,prezimeGlumcaIn.value);
            this.CrtajTabeluFo(document.querySelector(".tabelaElementi"));
            }else{alert("Nisu popunjena sva polja")}
        }
        filmoviGlumcaDiv.appendChild(btn);


    }

    CrtajPretragu(host){
        if(!host)
            throw new Error("Nije unet host!");
        
        let pretragaDiv = document.createElement("div");
        pretragaDiv.className = "pretragaDiv";
        host.appendChild(pretragaDiv);

        let pretragaIn = document.createElement("input");
        pretragaIn.className = "pretragaIn";
        pretragaDiv.appendChild(pretragaIn);
        

        let btn = document.createElement("button");
        btn.innerHTML = "Pretrazi";
        pretragaDiv.appendChild(btn);

        

        btn.onclick = async (ev) =>{
            if(pretragaIn.value != ""){
            let tab = document.querySelector(".tabelaBody");
            while(tab.firstChild != null)
                tab.removeChild(tab.firstChild);

            this.trenutnaStrana = 0;
            this.ukupnoFilmova = 0;
            document.querySelector(".tabelaElementi").removeChild(document.querySelector(".brojacStranicaDiv"));
            await fetch("http://localhost:5001/Film/Pretraga/" + pretragaIn.value,{method:"GET"})
            .then(p => {p.json().then( filmovi=>{
                filmovi.forEach(film => {
                    var a = new Film(film.id,film.naziv,film.ocena,film.zanr.naziv,this);
                    a.Crtaj(tab);
                })
            })
            fetch("http://localhost:5001/Film/IzbrojiFilmovePretraga/" + pretragaIn.value,{method:"GET"})
            .then(p=>{p.json().then( p=>{
                    this.ukupnoFilmova = p;
                    console.log(this.ukupnoFilmova)
                })
            })
            
            this.CrtajTabeluFo(document.querySelector(".tabelaElementi"));
        })
    }else{
        document.body.removeChild(this.kontejner);
        this.Crtaj(document.body);
    }
        }
        

    }

}

