import React from 'react'
import ReactStars from "react-rating-stars-component";

function DodajKomentar({ime, prezime, tekstKomentara, brZvezdica, handleStateChange, promeniZvezdice}) {


    function onFormSubmit()
    {
        alert(ime + " " + prezime + " " + tekstKomentara + " " + brZvezdica);
    } 

    return (
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

            <button>Dodaj komentar</button>
        </form>
    )
}

export default DodajKomentar
   