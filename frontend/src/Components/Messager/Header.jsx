import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser } from "@fortawesome/free-solid-svg-icons";
import { FindUser } from "./FindUsers";
import { useNavigate } from "react-router-dom";

export const Header = () => {
    const [searchTerm, setSearchTerm] = useState("");
    const [suggestions, setSuggestions] = useState([]);
    const token = localStorage.getItem('token');
    const userString = localStorage.getItem('user');
    const user = userString ? JSON.parse(userString) : {};
    const currentUserId = user.id; // Получите ID текущего пользователя
    const navigate = useNavigate(); 

    const handleSearchChange = async (e) => {
        const value = e.target.value;
        setSearchTerm(value);

        if (value) {
            try {
                const users = await FindUser(value, token);
                setSuggestions(users.$values || []);
            } catch (error) {
                console.error("Error fetching users:", error);
                setSuggestions([]);
            }
        } else {
            setSuggestions([]);
        }
    };

    const handleProfileClick = (userId) => {
        navigate(`/profile/${userId}`);
    };

    return (
        <header className="head">
            <div className="header-content">
                <div className="logo">Header</div>
                <div className="search-and-profile">
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
                                    suggestions.map((suggestion) => (
                                        <li key={suggestion.id} className="suggestion-item">
                                            <span className="username">
                                                {suggestion.userName} {suggestion.userSurname}
                                                {suggestion.id === currentUserId && ' (Вы)'}
                                            </span>
                                            <div className="actions">
                                                <button
                                                    className="action-button view-profile-button"
                                                    onClick={() => handleProfileClick(suggestion.id)}
                                                >
                                                    Посмотреть профиль
                                                </button>
                                                <button className="action-button add-friend-button">
                                                    Добавить в друзья
                                                </button>
                                            </div>
                                        </li>
                                    ))
                                ) : (
                                    <li>Пользователи не найдены</li>
                                )}
                            </ul>
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