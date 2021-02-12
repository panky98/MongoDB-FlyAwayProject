import React, {useState} from 'react'
import useFetch from '../services/useFetch.js';
import Spinner from '../components/Spinner.js';
import {useParams} from "react-router-dom";


function StatusRezervacija()
{

    const[kodRez, setKodRez]=useState();
    const [showSpinner,setShowSpinner]=useState(false);
    const [status,setStatus]=useState("");

    return(
        <div>
            {showSpinner && <Spinner />}
            <label>Kod rezervacije: </label> <input type="text" onChange={(event)=>setKodRez(event.currentTarget.value)}/>
            <button onClick={()=>Proveri()}>Proveri</button>

            <label>{status}</label>
        </div>
    );

    function Proveri(){
        setShowSpinner(true);
        fetch("https://localhost:44399/Rezervacija/VratiRezervacijuPrekoKoda/"+kodRez).then(p=>{
            if(p.ok){
                p.json().then(data=>{
                    setStatus(data.status);
                    setShowSpinner(false);
                })
            }else{
                setStatus("Nije pronadjena rezervacija sa prosledjenim kodom!");
                setShowSpinner(false);
            }
        });
    }
}

export default StatusRezervacija;