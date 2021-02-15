import React from 'react'
import { Link, NavLink } from "react-router-dom";

import Spinner from '../components/Spinner.js';
import useFetch from '../services/useFetch.js';

function AvioKompanije() {

    const {data:kompanije, loading, error}=useFetch("AvioKompanija/VratiAvioKompanije");

    if(error) throw error;
    if(loading) return <Spinner/>

    console.log(kompanije);

    return (
        <div className={"formCreate"} class="row float-container" style={{textAlign:"center"}}>
        {kompanije.map(k=>
            {
                return(
                    <div class="col-sm-6 float-child" style={{width:"100%"}} className={k.id}>
                        <div class="card">
                            <div className={"formCreate"} class="float-container card-body" style={{textAlign:"center"}}>
                            <div class="float-child" style={{width:"100%"}}>
                                <h5 class="card-title">Naziv:{k.naziv}</h5>
                                </div>
                                <div class="float-child" style={{width:"100%"}}>
                                <h5 class="card-title">Grad predstavnistva: {k.gradPredstavnistva}</h5>
                                </div>
                                <div class="float-child" style={{width:"100%"}}>
                                <p class="card-text">Godina osnivanja:{k.godinaOsnivanja}</p> </div>
                                <div class="float-child" style={{width:"100%"}}>
                                <Link to={`/avioKompanije/${k.id}`} className="btn btn-primary">Saznaj vise</Link> </div>
                            </div>
                        </div>
                    </div>
                )
            })} 
        

      </div>
    )
}

export default AvioKompanije
