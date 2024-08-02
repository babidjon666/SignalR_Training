import React from 'react';
import { Link } from 'react-router-dom';

export const Login = () => {
    return (
        <div className="Form">
            <h1>Log In</h1>

            <input 
                placeholder="Email"
                className="Input-Field"
                type="email"
            />

            <input 
                placeholder="Password"
                className="Input-Field"
                type="password"
            />

            <button className="Button">Log In</button>
            <p>
                Don't have an account? 
                <Link to="/register" style={{ color: 'white', cursor: 'pointer', margin: '10px' }}>
                    Register
                </Link>
            </p>
        </div>
    );
};