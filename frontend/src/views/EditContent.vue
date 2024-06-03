<template>
  <div class="container">
    <h2 class="mb-4">Edit Content</h2>
    <div v-if="content">
      <form v-if="isAuthorizedUser" @submit.prevent="updateContent" class="needs-validation" novalidate>
        <div class="mb-3">
          <label for="title" class="form-label">Title:</label>
          <input type="text" id="title" v-model="content.title" class="form-control" required>
          <div class="invalid-feedback">
            Please provide a title.
          </div>
        </div>
        <div class="mb-3">
          <label for="description" class="form-label">Description:</label>
          <textarea id="description" v-model="content.description" class="form-control" rows="5" required></textarea>
          <div class="invalid-feedback">
            Please provide a description.
          </div>
        </div>
        <button type="submit" class="btn btn-primary">Update</button>
      </form>
      <div v-else class="alert alert-danger" role="alert">
        You are not authorized to edit this content.
      </div>
    </div>
    <div v-else class="spinner-border text-primary" role="status">
      <span class="visually-hidden">Loading...</span>
    </div>
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
.container {
  max-width: 600px;
}

h2 {
  font-family: 'Roboto', sans-serif;
}

.form-label {
  font-weight: bold;
}
</style>
