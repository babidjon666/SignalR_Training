import React, { useEffect, useState, useCallback } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import { GetSubs } from "./GetSubs";
import { useNavigate } from "react-router-dom";

const mockUser = {
    userName: "",
    userSurname: "",
    email: "",
    bannerUrl: "https://via.placeholder.com/1200x400.png?text=Banner",
    profilePhotoUrl: "https://via.placeholder.com/120x120.png?text=Profile+Photo",
    photos: [
        { url: "https://via.placeholder.com/150.png?text=Photo+1" },
        { url: "https://via.placeholder.com/150.png?text=Photo+2" },
        { url: "https://via.placeholder.com/150.png?text=Photo+3" },
        { url: "https://via.placeholder.com/150.png?text=Photo+4" }
    ],
    posts: [
        { title: "Post 1", content: "This is the content of post 1." },
        { title: "Post 2", content: "This is the content of post 2." },
        { title: "Post 3", content: "This is the content of post 3." }
    ],
    likedPosts: [
        { title: "Liked Post 1", content: "Content of liked post 1." },
        { title: "Liked Post 2", content: "Content of liked post 2." }
    ],
    friends: [
        { profilePhotoUrl: "https://via.placeholder.com/50.png?text=Friend+1", userName: "Friend 1" },
        { profilePhotoUrl: "https://via.placeholder.com/50.png?text=Friend+2", userName: "Friend 2" },
        { profilePhotoUrl: "https://via.placeholder.com/50.png?text=Friend+3", userName: "Friend 3" }
    ],
    subs: [] // Initially empty, will be populated from API
};

const UserProfile = () => {
    const { userId } = useParams();
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [showLikedPosts, setShowLikedPosts] = useState(false);
    const [showFriends, setShowFriends] = useState(false);
    const [showSubs, setShowSubs] = useState(false);
    const token = localStorage.getItem('token');
    const navigate = useNavigate();

    const handleProfileClick = useCallback((userId) => {
        navigate(`/profile/${userId}`);
    }, [navigate]);

    useEffect(() => {
        const fetchUser = async () => {
            try {
                const userResponse = await axios.get(`http://localhost:5138/api/Profile?id=${userId}`, {
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                });

                const subsResponse = await GetSubs(userId, token);
                console.log(subsResponse.$values);
                mockUser.userName = userResponse.data.userName;
                mockUser.userSurname = userResponse.data.userSurname;
                mockUser.email = userResponse.data.email;
                mockUser.subs = subsResponse.$values; // Update subs with fetched data

                setUser(mockUser);
            } catch (err) {
                setError(err);
            } finally {
                setLoading(false);
            }
        };

        fetchUser();
    }, [userId]);

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error.message}</div>;

    const hasData = user && user.photos && user.posts && user.likedPosts && user.friends;

    return (
        <div className="user-profile">
            <div className="banner" style={{ backgroundImage: `url(${user.bannerUrl})` }}></div>
            <div className="profile-header">
                <div className="profile-photo">
                    <img src={user.profilePhotoUrl} alt={`${user.userName} profile`} />
                </div>
                <div className="profile-info">
                    <h1>{user.userName} {user.userSurname}</h1>
                    <p>Email: {user.email}</p>
                    <div className="profile-actions">
                        <button className="action-button">Add Friend</button>
                        <button className="action-button" onClick={() => setShowLikedPosts(!showLikedPosts)}>
                            {showLikedPosts ? "Hide Liked Posts" : "Show Liked Posts"}
                        </button>
                        <button className="action-button" onClick={() => setShowFriends(!showFriends)}>
                            {showFriends ? "Hide Friends" : "Show Friends"}
                        </button>
                        <button className="action-button" onClick={() => setShowSubs(!showSubs)}>
                            {showSubs ? "Hide Subs" : "Show Subs"}
                        </button>
                    </div>
                </div>
            </div>
            <div className="profile-content">
                <section className="photos">
                    <h2>Photos</h2>
                    <div className="photos-grid">
                        {hasData && user.photos.length > 0 ? (
                            user.photos.map((photo, index) => (
                                <img key={index} src={photo.url} alt={`User photo ${index}`} />
                            ))
                        ) : (
                            <p>No photos available</p>
                        )}
                    </div>
                </section>
                {showLikedPosts && (
                    <section className="liked-posts">
                        <h2>Liked Posts</h2>
                        <div className="posts-list">
                            {hasData && user.likedPosts.length > 0 ? (
                                user.likedPosts.map((post, index) => (
                                    <div key={index} className="post">
                                        <h3>{post.title}</h3>
                                        <p>{post.content}</p>
                                    </div>
                                ))
                            ) : (
                                <p>No liked posts available</p>
                            )}
                        </div>
                    </section>
                )}
                {showFriends && (
                    <section className="friends">
                        <h2>Friends</h2>
                        <ul className="friends-list">
                            {hasData && user.friends.length > 0 ? (
                                user.friends.map((friend, index) => (
                                    <li key={index}>
                                        <img src={friend.profilePhotoUrl} alt={`${friend.userName} profile`} />
                                        <span>{friend.userName}</span>
                                    </li>
                                ))
                            ) : (
                                <li>No friends available</li>
                            )}
                        </ul>
                    </section>
                )}
                {showSubs && (
                    <section className="friends">
                        <h2>Subscribers</h2>
                        <ul className="friends-list">
                            {hasData && user.subs.length > 0 ? (
                                user.subs.map((sub, index) => (
                                    <li key={index}>
                                        <img src={"https://via.placeholder.com/120x120.png?text=Profile+Photo"} alt={`${sub.userName} profile`} />
                                        <span>{sub.userName} {sub.userSurname}</span>
                                        <button className="action-button" onClick={() => handleProfileClick(sub.id)}>Профиль</button>
                                    </li>
                                ))
                            ) : (
                                <li>No subs available</li>
                            )}
                        </ul>
                    </section>
                )}
                <section className="posts">
                    <h2>Posts</h2>
                    <div className="posts-list">
                        {hasData && user.posts.length > 0 ? (
                            user.posts.map((post, index) => (
                                <div key={index} className="post">
                                    <h3>{post.title}</h3>
                                    <p>{post.content}</p>
                                </div>
                            ))
                        ) : (
                            <p>No posts available</p>
                        )}
                    </div>
                </section>
            </div>
        </div>
    );
};

export default UserProfile;