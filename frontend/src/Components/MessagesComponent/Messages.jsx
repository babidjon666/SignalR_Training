import React from 'react';

export const Messages = ({ currentChat, currentUser }) => {
    return (
        <div className="Messages">
            {currentChat.messages.map(message => (
                <div key={message.id} className={`Message ${message.user === currentUser ? 'right' : 'left'}`}>
                    <strong>{message.user}:</strong> {message.text}
                </div>
            ))}
        </div>
    );
};