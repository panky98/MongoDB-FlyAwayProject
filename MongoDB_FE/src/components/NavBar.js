import React from 'react'

import { Link, NavLink } from "react-router-dom";

function NavBar() {
    return (

      <div>
      <nav class="navbar navbar-light bg-light">
        <ul>
          <li class="navbar-brand">
            <Link to="/">
                Fly Away
            </Link>
          </li>  
          <li class="navbar-brand">
            <Link to="/letovi">
                Letovi
            </Link>
          </li> 
          <li class="navbar-brand">
            <Link to="/avioKompanije">
                Avio kompanije
            </Link>
          </li>
          <li class="navbar-brand">
            <Link to="/status-rezervacije">
                Proveri rezervaciju
            </Link>
          </li>        
        </ul>
      </nav>
      </div>
    )
}

export default NavBar
