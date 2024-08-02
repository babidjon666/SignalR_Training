import React from 'react';
import { Link } from 'react-router-dom';

export const Register = () => {
    return (
        <div className="Form">
            <h1>Sign Up</h1>

            <div className="two-items">
                <input 
                    placeholder="Name"
                    className="Input-Field"
                    type="text"
                />

                <input 
                    placeholder="Surname"
                    className="Input-Field"
                    type="text"
                />
            </div>

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

            <input 
                placeholder="Repeat Password"
                className="Input-Field"
                type="password"
            />

            <button className="Button">Sign Up</button>
            <p>
                Already have an account? 
                <Link to="/login" style={{ color: 'white', cursor: 'pointer', margin: '10px' }}>
                    Log In
                </Link>
            </p>
        </div>
    );
};