import React, { useState, useEffect } from 'react';
import { fetchChats, createChat } from './Api'; 
import { Messages } from '../MessagesComponent/Messages';
import { initializeSignalR } from './signalR'; 

const mockMessages = [
    { id: 1, user: 'Марк', text: 'Привет!' },
    { id: 2, user: 'Рома', text: 'Привет, Марчик!' },
    { id: 3, user: 'Марк', text: 'Ты же знаешь зачем я тебе пишу?' },
    { id: 4, user: 'Рома', text: 'Ну.....' },
    { id: 5, user: 'Марк', text: '*Злостно посмотрел*' },
    { id: 6, user: 'Рома', text: '*Начал собирать сумку со взрывчаткой*' }
];

export const Main = () => {
    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    const [chats, setChats] = useState([]);
    const [currentChat, setCurrentChat] = useState(null);
    const [newMessage, setNewMessage] = useState('');
    const currentUser = user?.userName;

    useEffect(() => {
        const fetchData = async () => {
            try {
                const token = localStorage.getItem('token');
                const data = await fetchChats(currentUser, token);
                console.log("API Response:", data);

                const chatData = data.$values;
                const chatsWithMessages = chatData.map(chat => ({
                    chatId: chat.chatId,
                    name: `Chat with ${chat.user2Name}`,
                    messages: mockMessages
                }));
                setChats(chatsWithMessages);
            } catch (error) {
                console.error(error.message);
            }
        };

        fetchData();
    }, [currentUser]);

    useEffect(() => {
        const handleReceiveChatUpdate = () => {
            fetchChats(currentUser, localStorage.getItem('token'))
                .then(data => {
                    const chatData = data.$values;
                    const chatsWithMessages = chatData.map(chat => ({
                        chatId: chat.chatId,
                        name: `Chat with ${chat.user2Name}`,
                        messages: mockMessages
                    }));
                    setChats(chatsWithMessages);
                })
                .catch(error => console.error(error.message));
        };

        const connection = initializeSignalR(handleReceiveChatUpdate);

    }, [currentUser]);

    const handleChatClick = (chat) => {
        setCurrentChat(chat);
    };

    const addChat = async () => {
        const friendUserName = prompt('Enter the username of the friend you want to chat with:');
        if (!friendUserName) return;

        try {
            const token = localStorage.getItem('token');
            await createChat(currentUser, friendUserName, token);
        } catch (error) {
            console.error(error.message);
        }
    };

    const handleMessageChange = (e) => {
        setNewMessage(e.target.value);
    };

    const handleSendMessage = async (e) => {
        e.preventDefault();
        if (!newMessage.trim()) return;

        const message = {
            user: currentUser,
            text: newMessage
        };

        // Here you should add the code to send the message to the server
        // For now, we just clear the input field
        setNewMessage('');
    };

    return (
        <div className="Container">
            <div className="Chats">
                <h2>{currentUser}</h2>
                <button className="AddChatButton" onClick={addChat}>Add Chat</button>
                <ul>
                    {chats.map(chat => (
                        <li key={chat.chatId} onClick={() => handleChatClick(chat)}>
                            {chat.name}
                        </li>
                    ))}
                </ul>
            </div>
            <div className="ChatWindow">
                {currentChat ? (
                    <div>
                        <h2>{currentChat.name}</h2>
                        <Messages
                            currentChat={currentChat}
                            currentUser={currentUser}
                        />
                        <form onSubmit={handleSendMessage} className="MessageInputForm">
                            <input 
                                type="text" 
                                value={newMessage} 
                                onChange={handleMessageChange} 
                                placeholder="Введите сообщение..." 
                                className="MessageInput" 
                            />
                            <button type="submit" className="SendMessageButton">Отправить</button>
                        </form>
                    </div>
                ) : (
                    <p>Please select a chat</p>
                )}
            </div>
        </div>
    );
};