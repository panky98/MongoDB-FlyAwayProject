import React from 'react';
import {useParams} from "react-router-dom";
import Spinner from '../components/Spinner.js';
import useFetch from '../services/useFetch.js';
import Komentari from './Komentari.js';
import LetZaAvioKompaniju from './LetZaAvioKompaniju.js';

function AvioKompanija() {

    const {id}=useParams();

    const {data:avioKompanija, loading:loading1, error:error1}=useFetch("AvioKompanija/VratiAvioKompaniju/"+id);


    if(error1) throw error1;
    if(loading1) return <Spinner/>;

    console.log(avioKompanija);


    return (
        <div class="card float-container" style={{textAlign:"left"}}>
            <div className={"formCreate"} class="card-body float-container" style={{textAlign:"left"}}>
            <div class="float-child" style={{width:"100%", marginLeft:"20px", marginTop:"5px"}}>
            <h3 style={{color:"#3399FF"}}>Naziv: {avioKompanija.naziv}</h3> </div>
            <div class="float-child" style={{width:"100%", marginLeft:"20px", marginTop:"5px"}}>
            <h5>Godina osnivanja: {avioKompanija.godinaOsnivanja}</h5> </div>
            <div class="float-child" style={{width:"100%", marginLeft:"20px", marginTop:"5px"}}>
            <h5>Sediste kompanije: {avioKompanija.gradPredstavnistva}</h5> </div>
            <div class="float-child" style={{width:"100%", marginLeft:"20px", marginTop:"5px"}}>
            <h3 style={{color:"#3399FF"}}>Istorija letova:</h3>
            {avioKompanija.letovi.length===0 && <p>Trenutno nema letova </p>}
            {avioKompanija.letovi.map(l=>{
                return <LetZaAvioKompaniju id={l} className={id}/>
                
            })}
         </div>
         <div class="float-child" style={{width:"80%", marginLeft:"20px", marginTop:"20px"}}>
            <Komentari avioKompanija={avioKompanija.id}/> </div>
            </div>
        </div>
    )
}

export default AvioKompanija
