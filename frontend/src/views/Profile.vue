<template>
  <div class="profile">
    <h1>Profile</h1>
    <div class="profile-info" v-if="user">
      <p><strong>Username:</strong> {{ user.username }}</p>
      <p><strong>Email:</strong> {{ user.email }}</p>
    </div>
    <div class="profile-posts" v-if="user">
      <h3>Posts</h3>
      <div id="content-card" class="post-card card p-4" v-for="content in contents" :key="content.id">
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

#content-card {
  margin-top: 1rem;
  border: 1px solid #ccc;
  border-radius: 5px;
}
</style>