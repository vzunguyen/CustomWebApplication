<template>
  <div class="search">
    <h1>Search</h1>
    <div class="search-options">
      <label>
        <input type="radio" v-model="searchOption" value="users" /> Users
      </label>
      <label>
        <input type="radio" v-model="searchOption" value="contents" /> Contents
      </label>
    </div>
    <input type="text" v-model="searchQuery" placeholder="Search..." />
    <div class="search-results">
      <div v-if="searchOption === 'users'">
        <div class="user-card card p-4" v-for="user in filteredUsers" :key="user.id">
          <h4>{{ user.username }}</h4>
          <p>{{ user.email }}</p>
          <div class="friend-options">
            <button @click="sendFriendRequest(user.id)">
              Add Friend
            </button>
            <button v-if="!isFriend(user.id) && hasSentFriendRequest(user.id)" @click="acceptF">
              Friend Request Sent
            </button>
            <button v-if="isFriend(user.id)" @click="removeFriend(user.id)">
              Remove Friend
            </button>
          </div>
        </div>
      </div>
      <div v-else-if="searchOption === 'contents'">
        <div class="content-card card p-4" v-for="content in filteredContents" :key="content.id">
          <h4>{{ content.title }}</h4>
          <p>{{ content.description }}</p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import api from '../services/api'

export default {
  name: 'SearchView',
  data() {
    return {
      searchOption: 'users',
      searchQuery: '',
      users: [],
      contents: [],
      friends: [],
      sentFriendRequests: []
    }
  },
  async created() {
    try {
      const usersResponse = await api.getAllUsers()
      this.users = usersResponse.data

      const contentsResponse = await api.getAllContent()
      this.contents = contentsResponse.data

      const friendsResponse = await api.getFriends()
      this.friends = friendsResponse.data

      const sentFriendRequestsResponse = await api.getFriendRequests()
      this.sentFriendRequests = sentFriendRequestsResponse.data
    } catch (error) {
      console.error(error)
    }
  },
  computed: {
    filteredUsers() {
      return this.users.filter(user =>
          user.username.toLowerCase().includes(this.searchQuery.toLowerCase())
      )
    },
    filteredContents() {
      return this.contents.filter(content =>
          content.title.toLowerCase().includes(this.searchQuery.toLowerCase())
      )
    }
  },
  methods: {
    isFriend(userId) {
      return this.friends.some(friend => friend.id === userId)
    },
    hasSentFriendRequest(userId) {
      return this.sentFriendRequests.some(request => request.id === userId)
    },
    async sendFriendRequest(friendId) {
      try {
        const response = await api.sendFriendRequest(friendId)
        console.log(response.data)
        this.sentFriendRequests.push({id: friendId})
      } catch (error) {
        console.error(error)
      }
    },
    async acceptFriendRequest(requestId) {
      try {
        await api.acceptFriendRequest(requestId)
        this.friends.push(this.sentFriendRequests.find(request => request.id === requestId))
        this.sentFriendRequests = this.sentFriendRequests.filter(request => request.id !== requestId)
      } catch (error) {
        console.error(error)
      }
    },
    async removeFriend(userId) {
      try {
        await api.removeFriend(userId)
        this.friends = this.friends.filter(friend => friend.id !== userId)
      } catch (error) {
        console.error(error)
      }
    }
  }
}
</script>