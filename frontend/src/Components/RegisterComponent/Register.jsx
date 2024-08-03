import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { message } from 'antd';
import { useNavigate } from 'react-router-dom';
import { handleChange, handleSubmit } from './RegisterService';

export const Register = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        userName: '',
        userSurname: '',
        email: '',
        password: '',
        repeatPassword: '',
    });

    const handleChangeWrapper = (e) => handleChange(e, formData, setFormData);
    const handleSubmitWrapper = (e) => handleSubmit(e, formData, navigate, message);

    return (
        <div className='Main'>
            <div className="Form">
                <h1>Sign Up</h1>

                <form onSubmit={handleSubmitWrapper}>
                    <div className="two-items">
                        <input 
                            name="userName"
                            placeholder="Name"
                            className="Input-Field"
                            type="text"
                            value={formData.userName}
                            onChange={handleChangeWrapper}
                        />

                        <input 
                            name="userSurname"
                            placeholder="Surname"
                            className="Input-Field"
                            type="text"
                            value={formData.userSurname}
                            onChange={handleChangeWrapper}
                        />
                    </div>

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

                    <input 
                        name="repeatPassword"
                        placeholder="Repeat Password"
                        className="Input-Field"
                        type="password"
                        value={formData.repeatPassword}
                        onChange={handleChangeWrapper}
                    />

                    <button className="Button" type="submit">Sign Up</button>
                </form>
                <p>
                    Already have an account? 
                    <Link to="/login" style={{ color: 'white', cursor: 'pointer', margin: '10px' }}>
                        Log In
                    </Link>
                </p>
            </div>
        </div>
    );
};