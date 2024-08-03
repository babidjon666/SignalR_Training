import axios from 'axios';

export const handleChange = (e, formData, setFormData) => {
    setFormData({
        ...formData,
        [e.target.name]: e.target.value,
    });
};

export const handleSubmit = async (e, formData, navigate, message) => {
    e.preventDefault();

    try {
        const response = await axios.post('http://localhost:5138/api/Login', {
            email: formData.email,
            password: formData.password,
        });

        message.success(response.data);
        navigate('/main', { state: { formData } });
    } catch (err) {
        message.warning("Такого пользователя нет!");
    }
};