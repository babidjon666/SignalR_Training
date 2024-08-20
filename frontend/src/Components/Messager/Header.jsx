import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser, faBell } from "@fortawesome/free-solid-svg-icons";
import { useNavigate } from "react-router-dom";
import { message } from 'antd';

import { FindUser } from "./FindUsers";
import { Subscribe } from "./Subscribe";

export const Header = () => {
    const [searchTerm, setSearchTerm] = useState("");
    const [suggestions, setSuggestions] = useState([]);
    const [subscribedUsers, setSubscribedUsers] = useState({});
    const [notificationsOpen, setNotificationsOpen] = useState(false);
    const token = localStorage.getItem('token');
    const userString = localStorage.getItem('user');
    const user = userString ? JSON.parse(userString) : {};
    const currentUserId = user.id;
    const navigate = useNavigate();

    const handleSearchChange = async (e) => {
        const value = e.target.value;
        setSearchTerm(value);

        if (value) {
            try {
                const users = await FindUser(currentUserId, value, token);
                setSuggestions(users.$values || []);
            } catch (error) {
                setSuggestions([]);
            }
        } else {
            setSuggestions([]);
        }
    };

    const handleProfileClick = (userId) => {
        navigate(`/profile/${userId}`);
    };

    const handleAddFriendClick = async (userId, isSubscribed) => {
        if (!isSubscribed) {
            try {
                await Subscribe(currentUserId, userId, token, message);
                setSubscribedUsers((prevSubscribedUsers) => ({
                    ...prevSubscribedUsers,
                    [userId]: true,
                }));
            } catch (error) {
                message.error("Ошибка при подписке");
            }
        }
    };

    const toggleNotifications = () => {
        setNotificationsOpen(!notificationsOpen);
    };

    return (
        <header className="head">
            <div className="header-content">
                <div className="logo">хуйня</div>
                <div className="search-bar">
                    <input
                        type="text"
                        placeholder="Найти друзей..."
                        value={searchTerm}
                        onChange={handleSearchChange}
                    />
                    {searchTerm && (
                        <ul className="suggestions">
                            {suggestions.length > 0 ? (
                                suggestions.map((suggestion) => {
                                    const isSubscribed = subscribedUsers[suggestion.user.id] || suggestion.isSubscribed;
                                    return (
                                        <li key={suggestion.user.id} className="suggestion-item">
                                            <span className="username">
                                                {suggestion.user.userName} {suggestion.user.userSurname}
                                                {suggestion.user.id === currentUserId && ' (Вы)'}
                                            </span>
                                            <div className="actions">
                                                <button
                                                    className="action-button view-profile-button"
                                                    onClick={() => handleProfileClick(suggestion.user.id)}
                                                >
                                                    Посмотреть профиль
                                                </button>
                                                <button
                                                    className="action-button add-friend-button"
                                                    onClick={() => handleAddFriendClick(suggestion.user.id, isSubscribed)}
                                                >
                                                    {isSubscribed ? 'Подписан' : 'Добавить в друзья'}
                                                </button>
                                            </div>
                                        </li>
                                    );
                                })
                            ) : (
                                <li>Пользователи не найдены</li>
                            )}
                        </ul>
                    )}
                </div>
                <div className="profile-and-notifications">
                    <div className="notification-icon" onClick={toggleNotifications}>
                        <FontAwesomeIcon icon={faBell} size="lg" />
                        {notificationsOpen && (
                            <div className="notifications-dropdown">
                                <ul>
                                    <li>Уведомление 1</li>
                                    <li>Уведомление 2</li>
                                    <li>Уведомление 3</li>
                                </ul>
                            </div>
                        )}
                    </div>
                    <div className="profile-icon">
                        <FontAwesomeIcon icon={faUser} size="lg" />
                    </div>
                </div>
            </div>
        </header>
    );
};
