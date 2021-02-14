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
        <div className={"formCreate"} class="float-container" style={{textAlign:"center"}}>
             <div class="float-child" style={{width:"100%"}}>
            <p >Kod rezervacije: {rezervacija.kodRezervacije} BITAN ZA PROVERU STATUSA</p><br/>
           </div>
           <div class="float-child" style={{width:"100%"}}>
            <p style={{color:"#3399FF"}}>Status rezervacije: {rezervacija.status}</p>
            </div>
            <div class="float-child" style={{width:"100%"}}>
                 <p style={{color:"#3399FF"}}>Lista proizvoda: </p><br/>
                 {rezervacija.proizvodi.map((el=>{
                     return <div>{el}</div>
                 }))}
             </div>
        </div>
    );
}

export default InfoRezervacija;