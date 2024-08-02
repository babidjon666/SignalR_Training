import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import './App.css';
import { Login } from './Components/Login';
import { Register } from './Components/Register';

function App() {
  return (
    <Router>
      <div className="Main">
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route exact path="/" element={<Login />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;