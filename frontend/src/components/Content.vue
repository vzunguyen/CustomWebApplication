<template>
  <div class="card">
    <div class="card-body">
      <h5 class="card-title">{{ content.title }}</h5>
      <p class="card-text">{{ content.description }}</p>
      <div class="button-group">
        <button @click="editContent" class="btn btn-primary">Edit</button>
        <button @click="deleteContent" class="btn btn-danger">Delete</button>
      </div>
    </div>
  </div>
</template>

<script>
import api from '../services/api'
import router from "../router/index"
export default {
  name: 'ContentPost',
  props: {
    content: {
      type: Object,
      required: true,
    },
  },
  methods: {
    editContent() {
      router.push({name: 'EditContent', params: {id: this.content.id}});
    },
    async deleteContent() {
      try {
        const response = await api.deleteContent(this.content.id);

        if (response.ok) {
          // Emit an event to notify the parent component that the content has been deleted
          this.$emit('content-deleted', this.content.id);
        } else {
          // Handle error cases
          console.error('Failed to delete content:', response.statusText);
        }
      } catch (error) {
        console.error('Error deleting content:', error);
      }
    },
  },
};
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