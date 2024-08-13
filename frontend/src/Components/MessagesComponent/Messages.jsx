import React, { useRef, useEffect } from 'react';

export const Messages = ({ currentMessages, currentUser }) => {
    // Создайте реф для контейнера сообщений
    const messagesEndRef = useRef(null);

    // Функция для прокрутки до конца контейнера сообщений
    const scrollToBottom = () => {
        messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
    };

    // Прокручиваем контейнер до конца каждый раз, когда меняются сообщения
    useEffect(() => {
        scrollToBottom();
    }, [currentMessages]);

    return (
        <div className="Messages">
            {currentMessages && currentMessages.map((message, index) => (
                <div 
                    key={message.id || `${message.sender}-${message.time}-${index}`} 
                    className={`Message ${message.sender === currentUser ? 'right' : 'left'}`}
                >
                    <strong>{message.sender}:</strong> {message.text}
                </div>
            ))}
            {/* Скрытый элемент для прокрутки в конец */}
            <div ref={messagesEndRef} />
        </div>
    );
};