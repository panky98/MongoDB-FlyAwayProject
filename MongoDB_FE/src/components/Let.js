import React from 'react'
import {useParams} from "react-router-dom";
import Spinner from '../components/Spinner.js';
import useFetch from '../services/useFetch.js';
import MalaAvioKompanija from './MalaAvioKompanija.js';
import { Link, NavLink } from "react-router-dom";
import moment from 'moment';

function Let() {

    const {id}=useParams();

    const {data:l, loading:loading1, error:error1}=useFetch("Let/VratiLet/"+id);
    let pom="000000000000000000000000";
    if(l!==null)
        pom=l.avioKompanija;

    



    if(error1) throw error1;
    if(loading1) return <Spinner/>;

    console.log(l);
    //console.log(avioKompanija);

    console.log(moment().isBefore(l.datumLeta));
    return (
        <div className={"formCreate"} class="float-container" style={{textAlign:"left"}}>
            <div class="float-child" style={{width:"100%"}}>
            <h3>Polazni aerodrom: {l.polazniAerodrom}</h3> </div>
            <div class="float-child" style={{width:"100%"}}>
            <h3>Dolazni aerodrom: {l.dolazniAerodrom}</h3> </div>
            <div class="float-child" style={{width:"100%"}}>
            <h5>Datum:{l.datumLeta}</h5> </div>
            <div class="float-child" style={{width:"100%"}}>
            {l.avioKompanija!=="" && <MalaAvioKompanija id={l.avioKompanija}/>}
            </div>
            <div class="float-child" style={{width:"250%"}}>
            {l.avioKompanija!=="" && <Link to={`/avioKompanije/${l.avioKompanija}`} className="btn btn-primary">Saznaj o avio kompaniji</Link>} 
            </div>
            <div class="float-child" style={{width:"25%"}}>
            {moment().isBefore(l.datumLeta) && <Link to={`/kreiraj-putnika/${l.id}`} className="btn btn-primary">Rezerviši</Link>} </div>
        </div>
    )
}

export default Let
