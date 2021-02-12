import React, {useState} from 'react'
import useFetch from '../services/useFetch.js';
import Spinner from '../components/Spinner.js';
import {useParams} from "react-router-dom";


function Proizvodi()
{
    const {idRez}=useParams();

    const {data:proizvodi, loading, error}=useFetch("Proizvod/VratiProizvode");
    const [showSpinner,setShowSpinner]=useState(false);
    const [addedProducts,setAddedProducts]=useState([]);

    if(error) throw error;
    if(loading) return <Spinner/>

    console.log(proizvodi);
    return(
        <div>
            {showSpinner && <Spinner />}
            {proizvodi.map(el=>{
                return (<div>
                    <input type="checkbox" onChange={(event)=>{
                            if(event.currentTarget.checked){
                                addedProducts.push(el.id);
                            }
                            else{
                                let index=addedProducts.indexOf(el.id);
                                if(index!=-1){
                                    addedProducts.splice(index,1);
                                }
                            }
                            console.log(addedProducts);
                    }}/>
                    <p>Naziv: {el.naziv}</p><br/>
                    <p>Cena: {el.cena}</p><br/>
                    <img src={"data:image/jpeg;base64,"+el.slikaBytesBase64} style={{"width":"100px","height":"100px"}}/>
                </div>);
            })}

            <button onClick={()=>DodajProizvodeRezervaciji()}>Potvrdi</button>
        </div>
    );
    
    function DodajProizvodeRezervaciji(){
        setShowSpinner(true);
        fetch("https://localhost:44399/Rezervacija/DodajProizvodeRezervaciji/"+idRez,{
            method:"PUT",
            headers:{"Content-Type":"application/json"},
            body: JSON.stringify(addedProducts)
        }).then(p=>{
            if(p.ok){
                window.location.replace("../rezervacija/"+idRez);
            }
        }).catch(exc=>{
            console.log(exc);
        })
        setShowSpinner(false);
    }
}

export default Proizvodi;