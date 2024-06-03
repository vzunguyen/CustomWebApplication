<template>
  <div class="unauthorized-profile">
    <h2>{{ username }}'s Profile</h2>
    <div class="profile-info">
      <p><strong>Username:</strong> {{ user.username }}</p>
      <p><strong>Email:</strong> {{ user.email }}</p>
    </div>
    <div class="profile-posts">
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
  name: 'UnauthorizedProfileView',
  data() {
    return {
      user: {},
      contents: []
    }
  },
  computed: {
    username() {
      return this.$route.params.username
    }
  },
  async created() {
    try {
      const response = await api.getUserByName(this.username)
      this.user = response.data

      const contentResponse = await api.getContentByUserId(this.user.id)
      this.contents = contentResponse.data
    } catch (error) {
      console.error(error)
    }
  }
}
</script>

<style scoped>
* {
  font-family: 'Roboto', sans-serif;
}

#content-card {
  margin-top: 1rem;
  border: 1px solid #ccc;
  border-radius: 5px;
}
</style>