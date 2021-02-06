import React, {useState} from 'react'
import useFetch from '../services/useFetch.js';
import Spinner from '../components/Spinner.js';
import { ImagePicker } from 'react-file-picker'



function CreatePutnikForm(){
    const [passportBase64,setPassportBase64]=useState("");
    return(
        <div>
        <ImagePicker
        extensions={['jpg', 'jpeg', 'png']}
        maxSize={2}
        dims={{minWidth: 100, maxWidth: 1024, minHeight: 100, maxHeight: 1024}}
        onChange={base64 => {setPassportBase64(base64.slice(23,base64.length));}}
        onError={errMsg => console.log(errMsg)}
      >
        <button>
          Click to upload image
        </button>
      </ImagePicker>
      <button onClick={()=>console.log(passportBase64)}>log</button>
      </div>
    )
}

export default CreatePutnikForm;