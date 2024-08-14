import axios from 'axios';

export const FindUser = async (userName, token) => {
    if (!userName) throw new Error('Current user is not defined');
    
    if (!token) throw new Error('No token found. Please log in first.');

    try {
        const response = await axios.get(`http://localhost:5138/api/Friends?userName=${userName}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        return response.data;
    } catch (error) {
        throw new Error(`Error fetching chats: ${error.message}`);
    }
};