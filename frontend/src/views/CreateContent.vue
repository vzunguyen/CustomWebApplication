<template>
  <div class="create-content container">
    <h1 class="mb-4">Create Content</h1>
    <form @submit.prevent="createContent" class="needs-validation" novalidate>
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
      <button type="submit" class="btn btn-primary">Create</button>
    </form>
  </div>
</template>

<script>
import api from '../services/api'
import router from "../router/index";

export default {
  name: 'CreateContent',
  data() {
    return {
      content: {
        title: '',
        description: ''
      }
    }
  },
  methods: {
    async createContent() {
      try {
        const response = await api.createContent({
          title: this.content.title,
          description: this.content.description,
        })
        console.log(response.data)
        router.push('/');
      } catch (error) {
        console.error(error);
        alert('There is something wrong with the content creation. Please try again.');
      }
    }
  }
}
</script>