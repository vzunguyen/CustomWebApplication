<template>
  <div class="card">
    <div class="card-body">
      <h5 class="card-title">{{ friend.username }}</h5>
      <p class="card-text">{{ friend.email }}</p>
      <div class="button-group">
        <button @click="viewProfile" class="btn btn-primary">View Profile</button>
        <button @click="removeFriend" class="btn btn-primary">Remove Friend</button>
      </div>
    </div>
  </div>
</template>

<script>
import api from '../services/api'
export default {
  name: 'FriendCard',
  props: {
    friend: {
      type: Object,
      required: true
    }
  },
  methods: {
    viewProfile() {
      this.$router.push({ name: 'UnauthorizedProfile', params: { username: this.friend.username } })
    },
    async removeFriend() {
      const response = await api.removeFriend(this.friend.id)
      
      if (response.ok) {
        // Emit an event to notify the parent component that the friend has been removed
        this.$emit('friend-removed', this.friend.id)
      } else {
        // Handle error cases
        console.error('Failed to remove friend:', response.statusText)
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
* {
  font-family: 'Roboto', sans-serif;
}

.card {
  position: relative;
  align-self: center;
  margin: 10px;
  display: inline-block;
  width: 50rem;
  border: 1px solid #e0e0e0;
  border-radius: 5px;
}

.card-body {
  padding: 1rem;
}

.button-group {
  margin-top: 1rem;
}

.btn {
  margin-right: 0.5rem;
}
</style>