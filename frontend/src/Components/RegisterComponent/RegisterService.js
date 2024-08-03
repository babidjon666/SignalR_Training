import axios from 'axios';

export const handleChange = (e, formData, setFormData) => {
    setFormData({
        ...formData,
        [e.target.name]: e.target.value,
    });
};

export const handleSubmit = async (e, formData, navigate, message) => {
    e.preventDefault();
    if (formData.password !== formData.repeatPassword) {
        message.warning("Пароли не совпадают!");
        return;
    }

    try {
        const response = await axios.post('http://localhost:5138/api/Register/register', {
            userName: formData.userName,
            userSurname: formData.userSurname,
            email: formData.email,
            password: formData.password,
        });

        message.success(response.data);
        navigate('/login');
    } catch (err) {
        message.error('Server error');
    }
};