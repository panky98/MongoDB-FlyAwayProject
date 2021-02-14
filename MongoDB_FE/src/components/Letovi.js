import React, {useState} from 'react'
import { Link, NavLink } from "react-router-dom";

import Spinner from '../components/Spinner.js';
import useFetch from '../services/useFetch.js';

function Letovi() {

    const [tipLeta, setTipLeta]=useState("svi");

    let pathUpita="Let/VratiSveLetove";
    if(tipLeta==="gotov")
        pathUpita="Let/VratiGotoveLetove";
    if(tipLeta==='trenutni')
        pathUpita="Let/VratiTrenutneLetove";
    const {data:letovi, loading, error}=useFetch(pathUpita);
    

    if(error) throw error;
    if(loading) return <Spinner/>

    console.log(letovi);
    console.log(pathUpita);

    return (
        <div className={"formCreate"} class="float-container" style={{textAlign:"center"}}>
            <div class="float-child" style={{width:"100%"}}>
            <select style={{width:"100%", height:"30px"}} class="form-control" value={tipLeta} onChange={(ev)=>setTipLeta(ev.target.value)}>

                <option key={"svi"} value={"svi"}>Svi letovi</option>
                <option key={"gotov"} value={"gotov"}>Gotovi</option>
                <option key={"trenutni"} value={"trenutni"}>Letovi na raspolaganju</option>
            </select>
            </div>
        <div class="row" class="float-child" style={{width:"100%"}}>
        {letovi.map(l=>
            {
                return(
                    <div class="col-sm-6">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Polazni aerodrom:{l.polazniAerodrom}</h5>
                                <h5 class="card-title">Dolazni aerodrom:{l.dolazniAerodrom}</h5>
                                <p class="card-text">Datum: {l.datumLeta}</p>
  
                                <Link to={`/letovi/${l.id}`} className="btn btn-primary">Saznaj vise</Link>
                            </div>
                        </div>
                    </div>
                )
            })} 
        

      </div>
      </div>
    )
}

export default Letovi
