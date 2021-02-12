import React, {useState} from 'react'
import useFetch from '../services/useFetch.js';
import Spinner from '../components/Spinner.js';
import {useParams} from "react-router-dom";

function InfoRezervacija()
{
    const {idRez}=useParams();

    const {data:rezervacija, loading, error}=useFetch("Rezervacija/VratiRezervacijuPrekoIda/"+idRez);
    if(error) throw error;
    if(loading) return <Spinner/>

    console.log(rezervacija);
    return(
        <div>
            <p>Kod rezervacije: {rezervacija.kodRezervacije} BITAN ZA PROVERU STATUSA</p><br/>
            <p>Status rezervacije: {rezervacija.status}</p>
             <div>
                 <p>Lista proizvoda: </p><br/>
                 {rezervacija.proizvodi.map((el=>{
                     return <div>{el}</div>
                 }))}
             </div>
        </div>
    );
}

export default InfoRezervacija;