import React, {useState} from 'react'
import ReactStars from "react-rating-stars-component";
import Spinner from '../components/Spinner.js';

function DodajKomentar({avioKompanija,ime, prezime, tekstKomentara, brZvezdica, handleStateChange, promeniZvezdice}) {

    const [showSpinner,setShowSpinner]=useState(false);

    async function onFormSubmit()
    {
       
        setShowSpinner(true);
        alert(ime + " " + prezime + " " + tekstKomentara + " " + brZvezdica);

        const obj={
            ime:ime,
            prezime:prezime,
            tekstKomentara:tekstKomentara,
            brZvezdica:brZvezdica
        };

        await fetch("https://localhost:44399/Komentar/KreirajKomentar/"+avioKompanija,{
        method:"POST",
        headers:{"Content-Type":"application/json"},
        body: JSON.stringify(obj)    
    }).then(p=>{
        if(p.ok){
            console.log("Uspesno dodato!");
        }
    }).catch(exc=>{
        console.log(exc);
        setShowSpinner(false);
    });
    window.location.reload();
    setShowSpinner(false);
    }
    
    
 

    return (
        <div>
             {showSpinner && <Spinner />}
        <form onSubmit={()=>onFormSubmit()}>
            <div>
                <input placeholder="Ime" name="ime" value={ime} onChange={(ev)=>handleStateChange(ev)}/>
                <input placeholder="Prezime" name="prezime" value={prezime} onChange={(ev)=>handleStateChange(ev)}/>
            </div>
            <textarea placeholder="Unesite Vas komentar" name="tekstKomentara" cols="40" rows="5" value={tekstKomentara}  onChange={(ev)=>handleStateChange(ev)}></textarea>
            <ReactStars
            count={5}
            onChange={(broj)=>promeniZvezdice(broj)}
            size={24}
            activeColor="#ffd700"
            />

            <button className="btn btn-primary">Dodaj komentar</button>
        </form>
        </div>
    )
}

export default DodajKomentar
   