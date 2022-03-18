import { Glumac } from "./Glumac.js";
export class Film{


    constructor(id,naziv,ocena,zanrId,katalog){
        this.id = id;
        this.naziv = naziv;
        this.ocena = ocena;
        this.zanrId = zanrId;
        this.katalog = katalog;
        this.kontejner = null;
    }

    UcitajGlumce(host){
        fetch("http://localhost:5001/Film/PreuzmiGlumceUloge/"+this.id)
        .then(p=>{
            p.json().then(p=>{
                //brisi prethodni sadrzaj
                let t = document.querySelector(".glumciDiv");
                if(t)
                     t.parentElement.removeChild(t);
                //novi sadrzaj
                 let glumciDiv = document.createElement("div");
                 glumciDiv.className = "glumciDiv";
                 host.appendChild(glumciDiv);
                
                 let naslov = document.createElement("h3");
                 naslov.innerHTML = `Uloge u filmu ${this.naziv} :`;
                 glumciDiv.appendChild(naslov);

                 

                for(let i = 0; i < p[0].spojevi.length;i++){
                    console.log(p[0].spojevi[i]);
                    
                    var a = new Glumac(
                        p[0].spojevi[i].glumacId,
                        p[0].spojevi[i].glumacIme,
                        p[0].spojevi[i].glumacPrezime,
                        p[0].spojevi[i].uloga,
                        this,
                        p[0].spojevi[i].spojId
                        );
                        a.Crtaj(glumciDiv);
                    }
                    //Dodavanje uloge

                    let dodajUloguDiv = document.createElement("div");
                    dodajUloguDiv.className = "dodajUloguDiv";
                    
                    let dodajUloguLab = document.createElement("label");
                    dodajUloguLab.innerHTML = "Uloga: ";
                    
                    let dodajUloguIn = document.createElement("input");
                    
                    let dodajGlumcaLab = document.createElement("label");
                    dodajGlumcaLab.innerHTML = "Glumac: ";
                    
                    let dodajGlumcaImeLab = document.createElement("label");
                    dodajGlumcaImeLab.innerHTML = "Ime";
                    let dodajGlumcaImeIn = document.createElement("input");

                    let dodajGlumcaPrezimeLab = document.createElement("label");
                    dodajGlumcaPrezimeLab.innerHTML = "Prezime";
                    let dodajGlumcaPrezimeIn = document.createElement("input");
               
              
                    
    
                    let btn = document.createElement("button");
                    btn.innerHTML = "Dodaj ulogu";
                    btn.onclick = async (ev) =>{
                    if(dodajUloguIn.value != "" && dodajGlumcaImeIn.value != "" && dodajGlumcaPrezimeIn.value != ""){
                        fetch(`http://localhost:5001/Glumac/PreuzmiGlumce/${dodajGlumcaImeIn.value}/${dodajGlumcaPrezimeIn.value}`,{
                            method: "GET"
                        }).then( p=> {p.json().then( p=>{
                            if(p==0)
                                alert("Nema glumca sa datim imenom u bazi");
                            else{
                                fetch(`http://localhost:5001/Glumac/DodajUlogu/${this.id}/${p[0].id}/${dodajUloguIn.value}`,{
                                    method: "POST"
                                }).then(this.UcitajGlumce(host))
                            }
                        })
                    })    
                }else{ alert("Nisu popunjena sva polja");}           
            }
                    glumciDiv.appendChild(dodajUloguDiv);
                    dodajUloguDiv.appendChild(dodajUloguLab);
                    dodajUloguDiv.appendChild(dodajUloguIn);
                    dodajUloguDiv.appendChild(dodajGlumcaLab);
                    dodajUloguDiv.appendChild(dodajGlumcaImeLab);
                    dodajUloguDiv.appendChild(dodajGlumcaImeIn);
                    dodajUloguDiv.appendChild(dodajGlumcaPrezimeLab);
                    dodajUloguDiv.appendChild(dodajGlumcaPrezimeIn);
                    dodajUloguDiv.appendChild(btn);
                })
            })
        
    }

     CrtajGlumce(host){
        if(!host)
            throw new Error("Nije unet host!");
        let t = document.querySelector(".glumciDiv");
        if(t)
            t.parentElement.removeChild(t);
        this.listaGlumaca = [];
        
        this.UcitajGlumce();
        console.log(this.listaGlumaca[0]);
        let glumciDiv = document.createElement("div");
        glumciDiv.className = "glumciDiv";
        host.appendChild(glumciDiv);
        this.listaGlumaca.forEach(e=>{
            console.log(e);
            e.Crtaj(glumciDiv);
        })

    }

    Crtaj(host){
        if(!host)
            throw new Error("Nije unet host!");
        
        let a;
        this.kontejner = document.createElement("tr");
        host.appendChild(this.kontejner);

        a = document.createElement("td");
        a.innerHTML = this.naziv;
        this.kontejner.appendChild(a);

        a = document.createElement("td");
        a.innerHTML = this.ocena;
        this.kontejner.appendChild(a);

        a = document.createElement("td");
        a.innerHTML = this.zanrId;
        this.kontejner.appendChild(a);

        a = document.createElement("button");
        a.innerHTML = "Izbrisi";
        a.onclick = async (ev) =>{
            await fetch("http://localhost:5001/Film/ObrisiFilm/" + this.id, { method: "DELETE"});
            document.body.removeChild(this.katalog.kontejner);
            this.katalog.Crtaj(document.body);
        }
        this.kontejner.appendChild(a);

        
        fetch("http://localhost:5001/Film/PreuzmiRezisera/" + this.id,{method:"GET"}).then(p=>{
            p.json().then(p=>{
                if(p.reziser != null){
                a = document.createElement("td");
                a.innerHTML = p.reziser.ime +" "+ p.reziser.prezime;
                this.kontejner.appendChild(a);
                }
            })
        })

        this.kontejner.onclick = (ev) =>{
            
            if(this.katalog.izabraniFilm != null)
                this.katalog.izabraniFilm.style.backgroundColor = "";
            this.katalog.izabraniFilm = this.kontejner;
            this.katalog.izabraniFilm.style.backgroundColor = "orange";

            this.UcitajGlumce(this.katalog.kontejner);
            
            this.IzmenaFilm(document.querySelector(".dodavanjeBrisanjeDiv"));
        }
        
    }

    CrtajInfo(host){
        if(!host)
            throw new Error("Nije unet host!");

        let infoDiv = document.createElement("div");
        infoDiv.className = "infoDiv";

    }

    IzmenaFilm(host){
        if(!host)
            throw new Error("Nije unet host!");

        if(document.querySelector(".izmenaDiv") != null)
            document.querySelector(".dodavanjeBrisanjeDiv").removeChild(document.querySelector(".izmenaDiv"));

        let izmenaDiv = document.createElement("div");
        izmenaDiv.className = "izmenaDiv";
        host.appendChild(izmenaDiv);

        let izmenaNaslov = document.createElement("h5");
        izmenaNaslov.innerHTML = "Izmeni: " + this.naziv;
        izmenaDiv.appendChild(izmenaNaslov);

            //naziv ocena
        let izmeniNazivDiv = document.createElement("div");
        izmenaDiv.appendChild(izmeniNazivDiv);

        let izmeniNazivLab = document.createElement("label");
        izmeniNazivLab.innerHTML = "Naziv ";
        izmenaDiv.appendChild(izmeniNazivLab);

        let izmeniNazivIn = document.createElement("input");
        izmeniNazivIn.value = this.naziv;
        izmenaDiv.appendChild(izmeniNazivIn);

        let izmeniOcenaDiv = document.createElement("div");
        izmenaDiv.appendChild(izmeniOcenaDiv);

        let izmeniOcenaLab = document.createElement("label");
        izmeniOcenaLab.innerHTML = "Ocena ";
        izmenaDiv.appendChild(izmeniOcenaLab);

        let izmeniOcenaIn = document.createElement("input");
        izmeniOcenaIn.value = this.ocena;
        izmeniOcenaIn.type = "number";
        izmeniOcenaIn.min = 1;
        izmeniOcenaIn.max = 10;
        izmeniOcenaIn.step = "0.1";
        izmenaDiv.appendChild(izmeniOcenaIn);
        

            //zanr
        let izmenaZanrDiv = document.createElement("div");

        let izmenaZanrLab = document.createElement("label");
        izmenaZanrLab.innerHTML = "Zanr ";
        let selekcija = document.createElement("select");
        let opcija;
        this.katalog.listaZanrova.forEach(e=>{
            opcija = document.createElement("option");
            opcija.innerHTML = e.naziv;
            opcija.value = e.id;
            selekcija.appendChild(opcija);
        })
        izmenaZanrDiv.appendChild(izmenaZanrLab);
        izmenaZanrDiv.appendChild(selekcija);
        izmenaDiv.appendChild(izmenaZanrDiv);

            //dugme
        let btn = document.createElement("button");
        btn.innerHTML = "Izmeni film";
        izmenaDiv.appendChild(btn);

        btn.onclick = async (ev) => {

            await fetch(`http://localhost:5001/Film/IzmeniFilm/${this.id}/${izmeniNazivIn.value}/${izmeniOcenaIn.value}/${selekcija.value}`,{
                method: "PUT"
            });
            document.body.removeChild(this.katalog.kontejner);
            this.katalog.Crtaj(document.body);
            
            

        }
    }
    
    

    
}