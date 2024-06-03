<template>
  <div class="edit-content">
    <h2>Edit Content</h2>
    <div v-if="content">
      <form v-if="isAuthorizedUser" @submit.prevent="updateContent">
        <div>
          <label for="title">Title:</label>
          <input type="text" id="title" v-model="content.title" required>
        </div>
        <div>
          <label for="description">Description:</label>
          <textarea id="description" v-model="content.description" required></textarea>
        </div>
        <button type="submit">Update</button>
      </form>
      <div v-else>
        <p>You are not authorized to edit this content.</p>
      </div>
    </div>
    <p v-else>Loading...</p>
  </div>
</template>

<script>
import api from '../services/api'

export default {
  name: 'EditContent',
  data() {
    return {
      content: null,
      isAuthorizedUser: false
    }
  },
  created() {
    this.fetchContent()
  },
  methods: {
    async fetchContent() {
      try {
        const contentId = this.$route.params.id
        const response = await api.getContentById(contentId)
        this.content = response.data

        // Check if the current user is the author of the content
        const userResponse = await api.getCurrentUser()
        const currentUser = userResponse.data
        this.isAuthorizedUser =  currentUser.id === this.content.userId
      } catch (error) {
        console.error(error)
      }
    },
    async updateContent() {
      try {
        const contentId = this.$route.params.id
        const response = await api.updateContent(contentId, this.content)
        // Handle successful content update
        console.log(response.data)
        // Optionally, redirect to content details page or display success message
      } catch (error) {
        // Handle error
        console.error(error)
      }
    }
  }
}
</script>

<style scoped>

* {
  font-family: 'Roboto', sans-serif;
}

form {
  max-width: 400px;
  margin: 0 auto;
}

div {
  margin-bottom: 10px;
}

label {
  display: block;
  font-weight: bold;
}

input[type="text"],
textarea {
  width: 100%;
  padding: 5px;
}

button {
  padding: 10px 20px;
  background-color: #007bff;
  color: #fff;
  border: none;
  cursor: pointer;
}
</style>