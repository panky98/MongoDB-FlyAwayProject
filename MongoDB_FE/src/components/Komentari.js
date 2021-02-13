import React, {useState} from 'react'

import Spinner from '../components/Spinner.js';
import useFetch from '../services/useFetch.js';
import DodajKomentar from '../components/DodajKomentar.js';
import ReactStars from "react-rating-stars-component";

function Komentari({avioKompanija}) {

    const {data:komentari, loading:loading, error:error}=useFetch("Komentar/VratiKomentareZaAvioKompaniju/" + avioKompanija);
    const [prikaziDodajKomentar, setPrikaziDodajKomentar]=useState(true);
    const [ime, setIme]=useState("");
    const [prezime, setPrezime]=useState("");
    const [tekstKomentara, setTekstKomentara]=useState("");
    const [brZvezdica, setBrZvezdica]=useState(0);

    if(error) throw error;
    if(loading) return <Spinner/>;

    //let komentari=kom.filter(k=>k.)
    console.log(komentari);

    const tekstDugme=prikaziDodajKomentar===true ? "Dodaj komentar" : "Zatvori";

    const handleStateChange=(ev)=>{
        console.log(ev.target.name);
        if(ev.target.name==="ime")
            setIme(ev.target.value);
        if(ev.target.name==="prezime")
            setPrezime(ev.target.value);
        if(ev.target.name==="tekstKomentara")
            setTekstKomentara(ev.target.value);
    }

    const promeniZvezdice=(broj)=>{
        setBrZvezdica(broj);
    }
    return (


    <div class="container">
  
        <div class="row">
            <div class="col-md-8">
                <div class="page-header">
                    <h1><small class="pull-right">{komentari.length}</small> Komentari </h1>
                    <button onClick={()=>setPrikaziDodajKomentar(!prikaziDodajKomentar)}>{tekstDugme}</button>
                    {!prikaziDodajKomentar && <DodajKomentar 
                                                ime={ime}
                                                prezime={prezime} 
                                                tekstKomentara={tekstKomentara} 
                                                brZvezdica={brZvezdica}
                                                handleStateChange={handleStateChange}
                                                promeniZvezdice={promeniZvezdice}
                                                avioKompanija={avioKompanija}
                                                />}
                </div>
                {komentari.map(k=>
                {   
                    return(
                    <div class="comments-list" className={k.id}>
                        <div class="media">
                            <div class="media-body">
                                 <h4 class="media-heading user_name">{k.ime} {k.prezime}</h4>
                                 <p>{k.tekstKomentara}</p>       
                                 <ReactStars
                                    count={5}
                                    value={k.brZvezdica}
                                    size={24}
                                    activeColor="#ffd700"
                                />
                            </div>
                        </div>
                    </div>
                )})}
            </div>
        </div>
    </div>

    )
}

export default Komentari
