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

function App() {
  return (
    <Router>
    <NavBar/>
    <Switch>
      <Route exact path="/">
        <Home/>
      </Route>
      <Route path="/kreiraj-putnika">
        <CreatePutnikForm/>
      </Route>
      <Route exact path="/letovi">
        <Letovi/>
      </Route>
      <Route exact path="/letovi/:id">
        <Let/>
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
