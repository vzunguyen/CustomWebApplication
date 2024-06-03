<template>
  <div class="unauthorized container">
    <h1 class="mb-4">Unauthorized</h1>
    <div class="alert alert-danger" role="alert">
      You are not authorized to delete this content.
    </div>
    <button class="btn btn-primary" @click="goBack">Go Back</button>
  </div>
</template>

<script>
export default {
  name: 'UnAuthorized',
  methods: {
    goBack() {
      this.$router.go(-1); // Go back to the previous page
    }
  }
}
</script>

<style scoped>
.container {
  max-width: 600px;
  margin: 0 auto;
  text-align: center;
}

.alert {
  margin-top: 20px;
}

.btn {
  margin-top: 20px;
}
</style>
