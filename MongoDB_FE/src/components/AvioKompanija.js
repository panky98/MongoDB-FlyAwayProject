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
        <div>
            <h3>Naziv: {avioKompanija.naziv}</h3>
            <h3>Godina osnivanja: {avioKompanija.godinaOsnivanja}</h3>
            <h3>Sediste kompanije: {avioKompanija.gradPredstavnistva}</h3>
            <h3>Dostupni letovi:</h3>
            {avioKompanija.letovi.length===0 && <p>Trenutno nema letova </p>}
            {avioKompanija.letovi.map(l=>{
                return <LetZaAvioKompaniju id={l} className={id}/>
            })}
        
            <Komentari/>
        </div>
    )
}

export default AvioKompanija
