<template>
  <div class="friend">
    <h1>Friends</h1>
    <div class="friend-list">
      <FriendCard v-for="friend in friends" :key="friend.id" :friend="friend" @view-profile="viewProfile" />
    </div>
  </div>
</template>

<script>
import api from '../services/api'
import router from "../router/index"
import FriendCard from '../components/FriendCard.vue'

export default {
  name: "FriendView",
  components: {
    FriendCard
  },
  data() {
    return {
      friends: []
    }
  },
  async created() {
    try {
      const userId = localStorage.getItem('userId') // Assuming the user ID is stored in local storage
      const response = await api.getFriends(userId)
      this.friends = response.data
    } catch (error) {
      console.error(error)
    }
  },
  methods: {
    viewProfile(friendId) {
      // Redirect to the friend's profile page
      router.push({ name: 'Profile', params: { id: friendId } })
    }
  }
}
</script>