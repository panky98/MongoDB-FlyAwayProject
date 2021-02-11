import React from 'react'
import useFetch from '../services/useFetch.js';
import Spinner from '../components/Spinner.js';
function LetZaAvioKompaniju({id}) {

    const {data:l, loading:loading1, error:error1}=useFetch("Let/VratiLet/"+id);

    if(error1) throw error1;
    if(loading1) return <Spinner/>;

    return (
        <div class="col-sm-6">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Polazni aerodrom:{l.polazniAerodrom}</h5>
                    <h5 class="card-title">Dolazni aerodrom:{l.dolazniAerodrom}</h5>
                    <p class="card-text">Datum: {l.datumLeta}</p>

                </div>
             </div>
        </div>
                    
    )
}

export default LetZaAvioKompaniju
