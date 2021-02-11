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
        <div class="row">
        {kompanije.map(k=>
            {
                return(
                    <div class="col-sm-6" className={k.id}>
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">Naziv:{k.naziv}</h5>
                                <h5 class="card-title">Grad predstavnistva: {k.gradPredstavnistva}</h5>
                                <p class="card-text">Godina osnivanja:{k.godinaOsnivanja}</p>
  
                                <Link to={`/avioKompanije/${k.id}`} className="btn btn-primary">Saznaj vise</Link>
                            </div>
                        </div>
                    </div>
                )
            })} 
        

      </div>
    )
}

export default AvioKompanije
