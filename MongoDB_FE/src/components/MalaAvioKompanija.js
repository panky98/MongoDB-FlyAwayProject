import React from 'react'
import Spinner from '../components/Spinner.js';
import useFetch from '../services/useFetch.js';


function MalaAvioKompanija({id}) {

    const {data:avioKompanija, loading:loading2, error:error2}=useFetch("AvioKompanija/VratiAvioKompaniju/"+id);

    if(error2) throw error2;
    if(loading2) return <Spinner/>

    console.log(avioKompanija);
    return (
        <h3>
            Naziv: {avioKompanija.naziv}
        </h3>
    )
}

export default MalaAvioKompanija
