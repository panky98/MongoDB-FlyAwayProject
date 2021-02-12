import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import './App.css';


//components
import NavBar from './components/NavBar.js';
import Error from './components/Error.js';
import Home from './components/Home.js';
import CreatePutnikForm from './components/CreatePutnikForm';
import Letovi from './components/Letovi.js';
import Let from './components/Let.js';
import AvioKompanija from './components/AvioKompanija';
import AvioKompanije from './components/AvioKompanije.js';
import Proizvodi from './components/Proizvodi';
import InfoRezervacija from './components/InfoRezervacija';
import StatusRezervacija from './components/StatusRezervacija';

function App() {
  return (
    <Router>
    <NavBar/>
    <Switch>
      <Route exact path="/">
        <Home/>
      </Route>
      <Route path="/kreiraj-putnika/:idLeta">
        <CreatePutnikForm/>
      </Route>
      <Route path="/proizvodi/:idRez">
        <Proizvodi/>
      </Route>
      <Route path="/rezervacija/:idRez">
        <InfoRezervacija/>
      </Route>
      <Route path="/status-rezervacije">
        <StatusRezervacija/>
      </Route>
      <Route exact path="/letovi">
        <Letovi/>
      </Route>
      <Route exact path="/letovi/:id">
        <Let/>
      </Route>
      <Route exact path="/avioKompanije">
        <AvioKompanije/>
      </Route>
      <Route exact path="/avioKompanije/:id">
        <AvioKompanija/>
      </Route>
      <Route  path="*">
        <Error />
      </Route>
    </Switch>
</Router>
  );
}

export default App;
