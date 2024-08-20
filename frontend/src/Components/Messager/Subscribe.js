import axios from 'axios';

export const Subscribe = async (myID, friendID, token, message) => {
    if (!myID) throw new Error('Current user is not defined');
    if (!friendID) throw new Error('Friend user is not defined');
    if (!token) throw new Error('No token found. Please log in first.');

    try {
        const response = await axios.post(`http://localhost:5138/api/Profile/Subscribe?myID=${myID}&friendID=${friendID}`, {}, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        message.success("Подписался!")
        return response;
    } catch (error) {
        throw error;
    }
};

export const Unsubscribe = async (myID, friendID, token) => {
    if (!myID) throw new Error('Current user is not defined');
    if (!friendID) throw new Error('Friend user is not defined');
    if (!token) throw new Error('No token found. Please log in first.');

    try {
        const response = await axios.post(`http://localhost:5138/api/Profile/Unsubscribe?myID=${myID}&friendID=${friendID}`, {}, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        return response;
    } catch (error) {
        throw error;
    }
};