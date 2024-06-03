import axios from 'axios'
// import {jwtDecode} from 'jwt-decode';

const api = axios.create({
    baseURL: 'http://localhost:5008/api',
    headers: {
        'Content-Type': 'application/json'
    }
})

// Add interceptor to include the token in the request headers
api.interceptors.request.use(config => {
    const token = localStorage.getItem('token')
    if (token) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

export function logout() {
    localStorage.removeItem('token');
}

export default {
    // Authentication
    login(loginDto) {
        return api.post('/auth/login', loginDto)
    },
    register(userDto) {
        return api.post('/auth/register', userDto)
    },
    getUserById(id) {
        return api.get(`/users/${id}`)
    },
    getUserByName(name) {
        return api.get(`/users/username/${name}`)
    },
    getCurrentUser() {
        return api.get(`/users/current`)
    },
    getAllUsers()  {
        return api.get('/users')
    },

    // Content
    getAllContent() {
        return api.get('/content')
    },
    getContentById(id) {
        return api.get(`/content/${id}`)
    },
    getContentByUserId(userId) {
        return api.get(`/content/user/${userId}`)
    },
    getCurrentUserContent() {
        return api.get('/content/user/current');
    },
    createContent(contentDto) {
        return api.post('/content', contentDto)
    },
    updateContent(id, contentDto) {
        return api.put(`/content/${id}`, contentDto)
    },
    deleteContent(id) {
        return api.delete(`/content/${id}`)
    },

    // Friends
    sendFriendRequest(recipientId) {
        // debugger; // eslint-disable-line no-debugger
        return api.post('/friends/request', recipientId)
    },
    acceptFriendRequest(friendId) {
        return api.put('/friends/accept', friendId)
    },
    removeFriend(userId, friendId) {
        return api.delete(`/friends/delete/${friendId}`, friendId)
    },
    getFriendRequests(userId) {
        return api.get(`/friends/requests/${userId}`, userId)
    },
    getFriends(userId) {
        return api.get(`/friends/${userId}`)
    }
}