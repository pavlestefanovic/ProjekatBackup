export class Glumac {

    constructor(id,ime,prezime,uloga,film,ulogaId){
        this.id = id;
        this.ime = ime;
        this.prezime = prezime;
        this.uloga = uloga;
        this.ulogaId = ulogaId;
        this.film = film;
    }

    Crtaj(host){
        if(!host)
            throw new Error("Nije unet host!");
        console.log(this);
        let ulogaDiv = document.createElement("div");
        
        host.appendChild(ulogaDiv);

        let ulogaLab = document.createElement("label");
        ulogaLab.innerHTML = `Glumac: ${this.ime} igra ulogu: ${this.uloga}`;
        ulogaDiv.appendChild(ulogaLab);

        let btn = document.createElement("button");
        btn.innerHTML = "Ukloni ulogu";
        btn.onclick = (ev) =>{
            fetch(`http://localhost:5001/Glumac/UkloniUlogu/${this.ulogaId}`,{
                method: "DELETE"
            }).then( alert("Uloga uklonjena")).then(this.film.UcitajGlumce(this.film.prodavnica.kontejner))
            
        }
        ulogaDiv.appendChild(btn);

    }
}