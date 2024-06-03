<template>
  <div class="create-content">
    <h2>Create Content</h2>
    <form @submit.prevent="createContent">
      <div>
        <label for="title">Title:</label>
        <input type="text" id="title" v-model="content.title" required>
      </div>
      <div>
        <label for="description">Description:</label>
        <textarea id="description" v-model="content.description" required></textarea>
      </div>
      <button type="submit">Create</button>
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
        // Handle successful login, e.g., store token, redirect to home page
        console.log(response.data)
        // redirect to home page
        router.push('/');
      } catch (error) {
        // Handle login error
        console.error(error);
        alert('There is something wrong with the content creation. Please try again.');
      }
    }
  }
}
</script>

<style scoped>
* {
  box-sizing: border-box;
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