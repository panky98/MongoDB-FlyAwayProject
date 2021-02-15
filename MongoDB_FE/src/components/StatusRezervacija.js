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
        <div className={"formCreate"} class="float-container" style={{textAlign:"left"}}>
            {showSpinner && <Spinner />}
            <div class="float-child" style={{width:"100%"}}>
            <label style={{color:"#3399FF"}}>Kod rezervacije: </label> 
            <input class="form-control" style={{width:"20%"}} type="text" onChange={(event)=>setKodRez(event.currentTarget.value)}/>
           </div>
            <div class="float-child" style={{width:"100%"}}>
            <button class="btn btn-primary" onClick={()=>Proveri()}>Proveri</button>
            </div>
            <h3 style={{color:"#3399FF", marginLeft:"15px"}}>{status}</h3>
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