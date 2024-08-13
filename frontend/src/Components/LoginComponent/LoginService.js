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

        if (response.data.result.success) {
            message.success(response.data.result.message);
            const user = {
                id: response.data.id,
                email: response.data.email,
                userName: response.data.userName,
                userSurname: response.data.userSurname,
                token: response.data.token,
            };

            localStorage.setItem('token', user.token);
            localStorage.setItem('user', JSON.stringify(user));
            
            navigate('/main', { state: { user } });
        } else {
            message.warning(response.data.result.message);
        }
    } catch (err) {
        message.warning("Такого пользователя нет!");
    }
};