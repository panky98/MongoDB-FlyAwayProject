import React, {useState} from 'react'
import useFetch from '../services/useFetch.js';
import Spinner from '../components/Spinner.js';
import { ImagePicker,FilePicker } from 'react-file-picker'
import {useParams} from "react-router-dom";
import ReactFileReader from 'react-file-reader';



function CreatePutnikForm(){
    const {idLeta}=useParams();
    const [passportBase64,setPassportBase64]=useState("");
    const [fullPassportBase64,setFullPassportBase64]=useState("");
    const [jmbg,setJmbg]=useState("");
    const [ime,setIme]=useState("");
    const [prezime,setPrezime]=useState("");
    const [godRodjenja,setGodRodjenja]=useState("");
    const [pol,setPol]=useState("");
    const [pcrTestFileName,setPcrTestFileName]=useState();
    const [pcrTestBase64,setPcrTestBase64]=useState("");
    const [prtljag,setPrtljag]=useState("");
    const [showSpinner,setShowSpinner]=useState(false);



    const {data:tipoviPrtljaga, loading, error}=useFetch("Kofer/VratiSveKofere");
    

    if(error) throw error;
    if(loading) return <Spinner/>

    return(
        <div className={"formCreate"} class="float-container" style={{textAlign:"left"}}>
          {showSpinner && <Spinner />}
          <div class="float-child" style={{width:"100%"}}>
      <label style={{color:"#3399FF"}}>JMBG: </label><input type="text" class="form-control" onChange={(event)=>setJmbg(event.currentTarget.value)}/>
      {(jmbg.length==0||jmbg.length!=13)&&<label style={{"color":"red"}}>*Obavezno polje</label>}<br/> </div>
      <div class="float-child" style={{width:"100%"}}>
      <label style={{color:"#3399FF"}}>Ime: </label><input type="text" class="form-control" onChange={(event)=>setIme(event.currentTarget.value)}/>
      {ime.length==0&&<label style={{"color":"red"}}>*Obavezno polje</label>}<br/> </div>
      <div class="float-child" style={{width:"100%"}}>
      <label style={{color:"#3399FF"}}>Prezime: </label><input type="text" class="form-control" onChange={(event)=>setPrezime(event.currentTarget.value)}/>
      {prezime.length==0&&<label style={{"color":"red"}}>*Obavezno polje</label>}<br/> </div>
      <div class="float-child" style={{width:"100%"}}>
      <label style={{color:"#3399FF"}}>Godina rodjenja: </label><input type="number" class="form-control" onChange={(event)=>setGodRodjenja(event.currentTarget.value)}/>
      {(godRodjenja.length!=4)&&<label style={{"color":"red"}}>*Obavezno polje</label>}<br/> </div>
      <div class="float-child" style={{width:"100%"}}>
      <label style={{color:"#3399FF"}}>Pol: </label>&nbsp;&nbsp;&nbsp;
      <label>M&nbsp;</label><input name="polRB" type="radio" value="M" onChange={(event)=>setPol(event.target.value)} />&nbsp;&nbsp;
      <label>Z&nbsp;</label><input name="polRB" type="radio" value="Z" onChange={(event)=>{setPol(event.target.value);}} />&nbsp;&nbsp;
      {(pol=="")&&<label style={{"color":"red"}}>*Obavezno polje</label>}<br/> </div>
      <div class="float-child" style={{width:"100%"}}>
      <label style={{color:"#3399FF"}}>Slika pasosa:</label>
      </div>
      <div class="float-child" style={{width:"100%"}}>
      <ImagePicker
        extensions={['jpg', 'jpeg', 'png']}
        maxSize={5}
        dims={{minWidth: 100, maxWidth: 2200, minHeight: 100, maxHeight: 2200}}
        onChange={base64 => {setPassportBase64(base64.slice(23,base64.length));setFullPassportBase64(base64);}}
        onError={errMsg => alert(errMsg)}
      >
        <button class="btn btn-light">
          Pronadji sliku
        </button>
      </ImagePicker>
      <img src={fullPassportBase64} style={{"width":"250px","height":"250px"}}></img>
      {(passportBase64=="")&&<label style={{"color":"red"}}>*Obavezno polje</label>}<br/>
      <br/><br/>
      </div>
      <div class="float-child" style={{width:"100%"}}>
      <label style={{color:"#3399FF"}}>PCR test:</label>
      <ReactFileReader fileTypes={[".pdf",".docx"],".png",".jpg",".jpeg"} base64={true} multipleFiles={false} handleFiles={(files)=>{
        setPcrTestFileName(files.fileList[0].name);
        let indOfComma=files.base64.indexOf(",");
        setPcrTestBase64(files.base64.slice(indOfComma+1,files.base64.length));
      }}>
           <button className='btn' class="btn btn-light">Pronadji fajl</button>
      </ReactFileReader> <label>{pcrTestFileName}</label>
      {(pcrTestBase64=="")&&<label style={{"color":"red"}}>*Obavezno polje</label>}<br/>
      <br/><br/>
      </div>
      <div class="float-child" style={{width:"100%"}}>
      <label style={{color:"#3399FF"}}>Tip prtljaga(Kolicina):</label>
      <div class="kontejnerPrtljaga">
        <div style={{"display":"flex"}}>
        <input name="prtljagRB" type="radio" value="mali" onChange={(event)=>setPrtljag(event.target.value)} />
        <img src="../mali.jpg" style={{"width":"100px","height":"100px"}}/>
          <p>Mali (0-21kg)</p>
        </div>
        <div style={{"display":"flex"}}>
          <input name="prtljagRB" type="radio" value="srednji" onChange={(event)=>setPrtljag(event.target.value)} />
          <img src="../srednji.jpg" style={{"width":"100px","height":"100px"}}/>
          <p>Srednji (21-50kg)</p>
        </div>
        <div style={{"display":"flex"}}>
          <input name="prtljagRB" type="radio" value="veliki" onChange={(event)=>setPrtljag(event.target.value)} />
          <img src="../veliki.jpg" style={{"width":"100px","height":"100px"}}/>
          <p>Veliki (50-100kg)</p>
        </div>
      </div>
      </div>
      <br/><br/>
      <button class="btn btn-dark" disabled={
        ((jmbg.length==13)&&ime.length>0&&prezime.length>0&&godRodjenja.length==4&&pol!=""&&passportBase64!=""&&pcrTestBase64!=""?false:true)
      } onClick={()=>CreateReservationAndPutnik()}>Dalje</button>
      </div>
    )

    function CreateReservationAndPutnik()
    {
      setShowSpinner(true);
      fetch("https://localhost:44399/Putnik/KreirajPutnika",{
        method:"POST",
        headers:{"Content-Type":"application/json"},
        body: JSON.stringify({"jmbg":jmbg,"ime":ime,"prezime":prezime,"godinaRodjenja":parseInt(godRodjenja),"pol":pol.charAt(0)})
      }).then(p=>{
          if(p.ok){
            p.json().then(data=>{
              fetch("https://localhost:44399/Rezervacija/KreirajRezervaciju",{
                method:"POST",
                headers:{"Content-Type":"application/json"},
                body:JSON.stringify({"brojSedista":Math.round(Math.random()*10),"pasosBytesBase64":passportBase64,"covidTestBytesBase64":pcrTestBase64,"status":"Na cekanju","putnik":data,"let":idLeta,"prtljag":prtljag})
              }).then(p=>{
                  if(p.ok)
                  {
                      p.json().then(data=>{
                          window.location.replace("/proizvodi/"+data)
                      });
                  }
              }).catch(exc=>{
                console.log("EXC"+exc);
              })
            });
          }
      }).catch(exc=>{
        console.log(exc);
        setShowSpinner(false);
      })
      setShowSpinner(false);
    }
}

export default CreatePutnikForm;