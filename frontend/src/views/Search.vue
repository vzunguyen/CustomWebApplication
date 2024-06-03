<template>
  <div class="search container">
    <h1 class="mb-4">Search</h1>
    <div class="search-options mb-4">
      <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" v-model="searchOption" value="users" id="searchUsers" />
        <label class="form-check-label" for="searchUsers">Users</label>
      </div>
      <div class="form-check form-check-inline">
        <input class="form-check-input" type="radio" v-model="searchOption" value="contents" id="searchContents" />
        <label class="form-check-label" for="searchContents">Contents</label>
      </div>
    </div>
    <input type="text" class="form-control mb-4" v-model="searchQuery" placeholder="Search..." />
    <div class="search-results row">
      <div v-if="searchOption === 'users'" class="col-12">
        <div class="user-card card mb-4 p-4" v-for="user in filteredUsers" :key="user.id">
          <h4>{{ user.username }}</h4>
          <p>{{ user.email }}</p>
          <div class="friend-options">
            <button class="btn btn-primary me-2" @click="sendFriendRequest(user.id)" v-if="!isFriend(user.id) && !hasSentFriendRequest(user.id)">
              Add Friend
            </button>
            <button class="btn btn-secondary me-2" v-if="!isFriend(user.id) && hasSentFriendRequest(user.id)" disabled>
              Friend Request Sent
            </button>
            <button class="btn btn-danger me-2" v-if="isFriend(user.id)" @click="removeFriend(user.id)">
              Remove Friend
            </button>
            <button class="btn btn-success" v-if="hasReceivedFriendRequest(user.id)" @click="acceptFriendRequest(user.id)">
              Accept Friend Request
            </button>
          </div>
        </div>
      </div>
      <div v-else-if="searchOption === 'contents'" class="col-12">
        <div class="content-card card mb-4 p-4" v-for="content in filteredContents" :key="content.id">
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
      sentFriendRequests: [],
      receivedFriendRequests: []
    }
  },
  async created() {
    try {
      const usersResponse = await api.getAllUsers()
      //filter out the current user
      const userResponse = await api.getCurrentUser()
      const currentUser = userResponse.data
      this.users = usersResponse.data.filter(user => user.id !== currentUser.id)

      const contentsResponse = await api.getAllContent()
      this.contents = contentsResponse.data

      const friendsResponse = await api.getFriends()
      this.friends = friendsResponse.data

      const sentFriendRequestsResponse = await api.getSentRequests()
      this.sentFriendRequests = sentFriendRequestsResponse.data

      const receivedFriendRequestsResponse = await api.getFriendRequests()
      this.receivedFriendRequests = receivedFriendRequestsResponse.data
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
    hasReceivedFriendRequest(userId) {
      return this.receivedFriendRequests.some(request => request.id === userId)
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
        this.friends.push(this.receivedFriendRequests.find(request => request.id === requestId))
        this.receivedFriendRequests = this.receivedFriendRequests.filter(request => request.id !== requestId)
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

<style scoped>
.card {
  margin-bottom: 1.5rem;
}
</style>
