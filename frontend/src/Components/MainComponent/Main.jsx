import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';
import { Messages } from '../MessagesComponent/Messages';

const mockMessages = [
    { id: 1, user: 'Марк', text: 'Привет!' },
    { id: 2, user: 'Рома', text: 'Привет, Марчик!' },
    { id: 3, user: 'Марк', text: 'Ты же знаешь зачем я тебе пишу?' },
    { id: 4, user: 'Рома', text: 'Ну.....' },
    { id: 5, user: 'Марк', text: '*Злостно посмотрел*' },
    { id: 6, user: 'Рома', text: '*Начал собирать сумку со взрывчаткой*' }
];

export const Main = () => {
    const location = useLocation();
    const { formData } = location.state || {};
    const [chats, setChats] = useState([
        { id: 1, name: 'Chat 1', messages: mockMessages },
        { id: 2, name: 'Chat 2', messages: mockMessages }
    ]);
    const [currentChat, setCurrentChat] = useState(null);
    const [newMessage, setNewMessage] = useState('');
    const currentUser = 'Марк';

    const handleChatClick = (chat) => {
        setCurrentChat(chat);
    };

    const addChat = () => {
        const newChat = { id: chats.length + 1, name: `Chat ${chats.length + 1}`, messages: mockMessages };
        setChats([...chats, newChat]);
    };

    const handleMessageChange = (e) => {
        setNewMessage(e.target.value);
    };

    const handleSendMessage = (e) => {
        e.preventDefault();
        if (!newMessage.trim()) return; // Не отправлять пустое сообщение

        const updatedChats = chats.map(chat => {
            if (chat.id === currentChat.id) {
                return {
                    ...chat,
                    messages: [
                        ...chat.messages,
                        {
                            id: chat.messages.length + 1,
                            user: currentUser,
                            text: newMessage
                        }
                    ]
                };
            }
            return chat;
        });

        setChats(updatedChats);
        setNewMessage('');
    };

    return (
        <div className="Container">
            <div className="Chats">
                <h2>{formData?.email}</h2>
                <button className="AddChatButton" onClick={addChat}>Add Chat</button>
                <ul>
                    {chats.map((chat) => (
                        <li key={chat.id} onClick={() => handleChatClick(chat)}>
                            {chat.name}
                        </li>
                    ))}
                </ul>
            </div>
            <div className="ChatWindow">
                {currentChat ? (
                    <div>
                        <h2>{currentChat.name}</h2>
                        <Messages currentChat={currentChat} currentUser={currentUser} />
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