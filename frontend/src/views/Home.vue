<template>
  <div class="about">
    <h1 class="text-center my-4">Home</h1>
    <div v-if="paginatedContents.length > 0" class="row">
      <div v-for="content in paginatedContents" :key="content.id" class="col-md-4 mb-4">
        <ContentPost :content="content" />
      </div>
    </div>
    <div v-else class="text-center">No content available.</div>

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

* {
  font-family: 'Roboto', sans-serif;
}

.about {
  padding: 20px;
}

.pagination {
  margin-top: 20px;
}

.page-info {
  font-size: 18px;
  font-weight: 500;
  margin: 0 10px;
}

@media (max-width: 767px) {
  .col-md-4 {
    width: 100%;
  }
}

@media (min-width: 768px) and (max-width: 991px) {
  .col-md-4 {
    width: 50%;
  }
}
</style>