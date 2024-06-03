<template>
  <div class="profile container">
    <h1 class="mb-4">Profile</h1>
    <div class="profile-info" v-if="user">
      <p><strong>Username:</strong> {{ user.username }}</p>
      <p><strong>Email:</strong> {{ user.email }}</p>
    </div>
    <div class="profile-posts mt-4" v-if="user">
      <h3>Posts</h3>
      <div class="post-card card mb-4 p-4" v-for="content in contents" :key="content.id">
        <h4>{{ content.title }}</h4>
        <p>{{ content.description }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import api from '../services/api'

export default {
  name: "ProfileView",
  data() {
    return {
      user: null,
      contents: null
    }
  },
  async created() {
    try {
      const userResponse = await api.getCurrentUser()
      this.user = userResponse.data

      const contentResponse = await api.getCurrentUserContent()
      this.contents = contentResponse.data
    } catch (error) {
      console.error(error)
    }
  }
}
</script>

<style scoped>
/* Add your scoped styles here */

* {
  font-family: 'Roboto', sans-serif;
}

.post-card {
  margin-top: 1rem;
  border: 1px solid #ccc;
  border-radius: 5px;
}
</style>
