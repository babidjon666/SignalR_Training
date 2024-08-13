import React, { useState, useEffect } from 'react';
import { fetchChats, createChat, fetchMessages, sendMessage } from './Api'; 
import { Messages } from '../MessagesComponent/Messages';
import { initializeSignalR } from './signalR'; 

export const Main = () => {
    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    const [chats, setChats] = useState([]);
    const [currentChat, setCurrentChat] = useState(null);
    const [currentMessages, setCurrentMessages] = useState([]);
    const [newMessage, setNewMessage] = useState('');
    const currentUser = user?.userName;
    const currentUserId = user?.id;

    useEffect(() => {
        const fetchData = async () => {
            try {
                const token = localStorage.getItem('token');
                const data = await fetchChats(currentUser, token);

                const chatData = data.$values;
                const chatsWithMessages = chatData.map(chat => ({
                    chatId: chat.chatId,
                    name: chat.user2Name,
                    messages: []
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
                        name: chat.user2Name,
                        messages: []
                    }));
                    setChats(chatsWithMessages);
                })
                .catch(error => console.error(error.message));
        };

        const handleReceiveMessage = (message) => {
            setCurrentMessages(prevMessages => [
                ...prevMessages,
                message
            ]);
        };

        initializeSignalR(handleReceiveChatUpdate, handleReceiveMessage);
    }, [currentUser, currentChat]);

    const handleChatClick = (chat) => {
        setCurrentChat(chat);
        fetchMessages(chat.chatId, localStorage.getItem('token'))
            .then(data => {
                const messageData = data.$values;
                const messagesInChat = messageData.map(message => ({
                    id: message.id,
                    chatId: message.chatId,
                    sender: message.sender,
                    text: message.text,
                    time: message.time
                }));
                setCurrentMessages(messagesInChat);
            });
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

        try {
            const token = localStorage.getItem('token');
            await sendMessage(currentChat.chatId, currentUserId, newMessage, token);
            setNewMessage('');
        } catch (error) {
            console.error(error.message);
        }
    };

    return (
        <div className="Container">
            <div className="Chats">
                <h2 className='textLOGO'>Hi, {currentUser}!</h2>
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
                    <div className="ChatContent">
                        <h2>{currentChat.name}</h2>
                        <div className="MessagesContainer">
                            <Messages
                                currentUser={currentUser}
                                currentMessages={currentMessages}
                            />
                        </div>
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