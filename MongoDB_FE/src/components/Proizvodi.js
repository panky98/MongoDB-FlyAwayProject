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
        <div className={"formCreate"} class="float-container" style={{textAlign:"center"}}>
            {showSpinner && <Spinner />}
            {proizvodi.map(el=>{
                return (<div className={"formCreate"} class="float-container" style={{textAlign:"center"}}>
                    <div class="float-child" style={{width:"10%"}}>
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
                    </div>
                    <div class="float-child" style={{width:"30%"}}>
                    <p style={{color:"#3399FF"}}>Naziv: {el.naziv}</p><br/>
                    </div>
                    <div class="float-child" style={{width:"30%"}}>
                    <p style={{color:"#3399FF"}}>Cena: {el.cena}</p><br/>
                    </div>
                    <div class="float-child" style={{width:"30%"}}>
                    <img src={"data:image/jpeg;base64,"+el.slikaBytesBase64} style={{"width":"100px","height":"100px"}}/>
                    </div>
                </div>);
            })}
            <div class="float-child" style={{width:"100%"}}>
            <button class="btn btn-primary" onClick={()=>DodajProizvodeRezervaciji()}>Potvrdi</button> </div>
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