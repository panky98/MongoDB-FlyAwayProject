import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import './App.css';


//components
import NavBar from './components/NavBar.js';
import Error from './components/Error.js';
import Home from './components/Home.js';


function App() {
  return (
    <Router>
    <NavBar/>
    <Switch>
      <Route exact path="/">
        <Home/>
      </Route>
      <Route  path="*">
        <Error />
      </Route>
    </Switch>
</Router>
  );
}

export default App;
