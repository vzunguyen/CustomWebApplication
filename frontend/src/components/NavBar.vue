<template>
  <div class="container">
    <ul class="nav nav-tabs mb-4">
      <li class="nav-item">
        <router-link class="nav-link" to="/">Home</router-link>
      </li>
      <li class="nav-item">
        <router-link class="nav-link" to="/search">Search</router-link>
      </li>
      <li class="nav-item">
        <router-link class="nav-link" to="/profile">Profile</router-link>      
      </li>
      <li class="nav-item">
        <router-link class="nav-link" to="/friends">Friends</router-link>
      </li>
      <li class="nav-item">
        <router-link class="nav-link" to="/create-content">Create Content</router-link>
      </li>
      <li v-if="loggedIn" class="nav-item">
        <router-link class="nav-link" to="/logout">Log Out</router-link>
      </li>
    </ul>
    <div class="tab-content">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
export default {
  name: "NavBar",
  computed: {
    loggedIn() {
      return localStorage.getItem('token') !== null
    }
  },
};
</script>

<style scoped>
* {
  box-sizing: border-box;
  font-family: 'Roboto', sans-serif;
  text-decoration: none;
}

.container {
  max-width: 960px;
  margin: 0 auto;
}

.nav-tabs {
  display: flex;
  list-style-type: none;
  margin: 0;
  padding: 0;
  background-color: #f8f9fa;
  border-bottom: none;
  padding: 10px;
  border-radius: 5px 5px 0 0;
}

.nav-item {
  margin-right: 10px;
}

.nav-link {
  color: #495057;
  font-weight: 500;
  padding: 10px 15px;
  border-radius: 5px;
  transition: background-color 0.3s ease;
}

.nav-link:hover {
  background-color: #e9ecef;
}

.tab-content {
  background-color: #fff;
  padding: 20px;
  border-radius: 0 0 5px 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}
</style>