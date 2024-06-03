import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5008/api',
    headers: {
        'Content-Type': 'application/json'
    }
});

// Add interceptor to include the token in the request headers
api.interceptors.request.use(config => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export function logout() {
    localStorage.removeItem('token');
    window.location.reload(); // Reload the page after logout
    window.location.href = '/login'; // Redirect to login page
}

export default {
    // Authentication
    login(loginDto) {
        return api.post('/auth/login', loginDto).then(response => {
            localStorage.setItem('token', response.data.token);
            window.location.reload(); // Reload the page after login
            window.location.href = '/'; // Redirect to home page
            return response;
        });
    },
    register(userDto) {
        return api.post('/auth/register', userDto);
    },
    getUserById(id) {
        return api.get(`/users/${id}`);
    },
    getUserByName(name) {
        return api.get(`/users/username/${name}`);
    },
    getCurrentUser() {
        return api.get(`/users/current`);
    },
    getAllUsers() {
        return api.get('/users');
    },

    // Content
    getAllContent() {
        return api.get('/content');
    },
    getContentById(id) {
        return api.get(`/content/${id}`);
    },
    getContentByUserId(userId) {
        return api.get(`/content/user/${userId}`);
    },
    getCurrentUserContent() {
        return api.get('/content/user/current');
    },
    createContent(contentDto) {
        return api.post('/content', contentDto);
    },
    updateContent(id, contentDto) {
        return api.put(`/content/${id}`, contentDto);
    },
    deleteContent(id) {
        return api.delete(`/content/${id}`).then(response => {
            window.location.reload(); // Reload the page after content deletion
            return response;
        });
    },

    // Friends
    sendFriendRequest(recipientId) {
        return api.post(`/friends/request?friendId=${recipientId}`);
    },
    acceptFriendRequest(friendId) {
        return api.put(`/friends/accept?friendId=${friendId}`);
    },
    removeFriend(friendId) {
        return api.delete(`/friends/delete/${friendId}`).then(response => {
            window.location.reload(); // Reload the page after friend deletion
            return response;
        }
        );
    },
    getFriendRequests(userId) {
        return api.get(`/friends/requests/${userId}`, userId);
    },
    getFriends(userId) {
        return api.get(`/friends/${userId}`);
    },
    getSentRequests() {
        return api.get(`/friends/sent/`);
    }
};
