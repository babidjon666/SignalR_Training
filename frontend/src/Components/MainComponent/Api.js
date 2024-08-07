// src/api.js
import axios from 'axios';

const BASE_URL = 'http://localhost:5138/api';

export const fetchChats = async (currentUser, token) => {
    if (!currentUser) throw new Error('Current user is not defined');
    
    if (!token) throw new Error('No token found. Please log in first.');

    try {
        const response = await axios.get(`${BASE_URL}/Chat/Get?UserName=${currentUser}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        return response.data;
    } catch (error) {
        throw new Error(`Error fetching chats: ${error.message}`);
    }
};

export const createChat = async (currentUser, friendUserName, token) => {
    if (!currentUser || !friendUserName) throw new Error('Invalid input data');
    
    if (!token) throw new Error('No token found. Please log in first.');

    try {
        await axios.post(`${BASE_URL}/Chat/Create`, {
            Me: currentUser,
            Friend: friendUserName
        }, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    } catch (error) {
        throw new Error(`Error creating chat: ${error.message}`);
    }
};