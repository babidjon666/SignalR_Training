import React, { useRef, useEffect } from 'react';

export const Messages = ({ currentMessages, currentUser }) => {
    const messagesEndRef = useRef(null);

    const scrollToBottom = () => {
        messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
    };

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
                    <div className="Message-time">
                        <small>{message.time.slice(11, 16)}</small>
                    </div>
                </div>
            ))}
            <div ref={messagesEndRef} />
        </div>
    );
};