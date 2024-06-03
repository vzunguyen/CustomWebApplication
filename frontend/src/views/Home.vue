<template>
  <div class="home container">
    <h1 class="mb-4">Home</h1>
    <div v-if="paginatedContents.length > 0" class="row">
      <div v-for="content in paginatedContents" :key="content.id">
        <ContentPost :content="content" @content-deleted="handleContentDeleted" />
      </div>
    </div>
    <div v-else class="text-center">No content available.</div>

    <div v-if="errorMessage" class="alert alert-danger" role="alert">
      {{ errorMessage }}
    </div>

    <div class="pagination d-flex justify-content-center mt-4">
      <button class="btn btn-primary me-2" :disabled="currentPage === 1" @click="prevPage">Prev</button>
      <span class="page-info">{{ currentPage }} / {{ Math.ceil(contents.length / itemsPerPage) }}</span>
      <button class="btn btn-primary ms-2" :disabled="currentPage === Math.ceil(contents.length / itemsPerPage)" @click="nextPage">Next</button>
    </div>
  </div>
</template>

<script>
import ContentPost from '../components/Content.vue';
import api from '../services/api'; // Import your API service

export default {
  name: 'HomeView',
  components: {
    ContentPost,
  },
  data() {
    return {
      contents: [],
      currentPage: 1,
      itemsPerPage: 3,
      errorMessage: '', // To hold error message
    };
  },
  created() {
    this.fetchContents();
  },
  methods: {
    async fetchContents() {
      try {
        const response = await api.getAllContent();
        this.contents = response.data;
      } catch (error) {
        console.error(error);
      }
    },
    paginate(contents) {
      const indexOfLastItem = this.currentPage * this.itemsPerPage;
      const indexOfFirstItem = indexOfLastItem - this.itemsPerPage;
      return contents.slice(indexOfFirstItem, indexOfLastItem);
    },
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    nextPage() {
      if (this.currentPage < Math.ceil(this.contents.length / this.itemsPerPage)) {
        this.currentPage++;
      }
    },
    handleContentDeleted(contentId) {
      // Remove the deleted content from the contents array
      this.contents = this.contents.filter(content => content.id !== contentId);
    },
  },
  computed: {
    paginatedContents() {
      return this.paginate(this.contents);
    },
  },
};
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700&display=swap');

.page-info {
  font-family: 'Roboto', sans-serif;
  box-sizing: border-box;
  margin: 0;
  padding: 0;
  font-size: 1.3em;
}

.btn-primary:disabled {
  background-color: #6c757d;
  border-color: #6c757d;
}

@media (max-width: 575px) {
  .col-md-4 {
    width: 100%;
  }
}

@media (min-width: 576px) and (max-width: 767px) {
  .col-md-4 {
    width: 100%;
  }
}

@media (min-width: 768px) and (max-width: 991px) {
  .col-md-4 {
    width: 50%;
  }
}

@media (min-width: 992px) {
  .col-md-4 {
    width: 33.33%;
  }
}
</style>
