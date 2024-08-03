import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { message } from 'antd';
import { useNavigate } from 'react-router-dom';
import { handleChange, handleSubmit } from './LoginService';

export const Login = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        email: '',
        password: '',
    });

    const handleChangeWrapper = (e) => handleChange(e, formData, setFormData);
    const handleSubmitWrapper = (e) => handleSubmit(e, formData, navigate, message);

    return (
        <div className='Main'>
            <div className="Form">
            <h1>Log In</h1>

            <form onSubmit={handleSubmitWrapper}>
                <input 
                    name="email"
                    placeholder="Email"
                    className="Input-Field"
                    type="email"
                    value={formData.email}
                    onChange={handleChangeWrapper}
                />

                <input 
                    name="password"
                    placeholder="Password"
                    className="Input-Field"
                    type="password"
                    value={formData.password}
                    onChange={handleChangeWrapper}
                />

                <button className="Button" type="submit">Log In</button>        
            </form>

            <p>
                Don't have an account? 
                <Link to="/register" style={{ color: 'white', cursor: 'pointer', margin: '10px' }}>
                    Register
                </Link>
            </p>
        </div>
        </div>
    );
};