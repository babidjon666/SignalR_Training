import axios from 'axios';

export const GetSubs = async (id, token) => {
    if (!id) throw new Error('Current user is not defined');
    
    if (!token) throw new Error('No token found. Please log in first.');

    try {
        const response = await axios.get(`http://localhost:5138/api/Profile/GetSubs?id=${id}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        return response.data;
    } catch (error) {
        throw new Error(`Error fetching users: ${error.message}`);
    }
};