import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import './Start.css';
import './Header.css';
import './Messager.css';
import './UserProfile.css';
import { Login } from './Components/LoginComponent/Login';
import { Register } from './Components/RegisterComponent/Register';
import { Main } from './Components/MainComponent/Main';
import { Messager } from './Components/Messager/Messager';
import UserProfile from './Components/Messager/UserProfile';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/main" element={<Main />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/messager" element={<Messager />} />
        <Route path="/profile/:userId" element={<UserProfile />} />
        <Route path="/" element={<Login />} />
      </Routes>
    </Router>
  );
}

export default App;