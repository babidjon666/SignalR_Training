import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import './Start.css';
import { Login } from './Components/LoginComponent/Login';
import { Register } from './Components/RegisterComponent/Register';
import { Main } from './Components/MainComponent/Main';

function App() {
  return (
    <Router>
      <Routes>
          <Route path="/main" element={<Main />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route exact path="/" element={<Login />} />
        </Routes>
    </Router>
  );
}

export default App;