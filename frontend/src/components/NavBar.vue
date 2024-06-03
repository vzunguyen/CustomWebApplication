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
      <li class="nav-item ms-auto" v-if="loggedIn">
        <router-link class="nav-link" to="/logout">Log Out</router-link>
      </li>
      <li class="nav-item ms-auto" v-if="!loggedIn">
        <router-link class="nav-link" to="/login">Log In</router-link>
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
  data() {
    return {
      loggedIn: localStorage.getItem('token') !== null
    };
  },
  created() {
    window.addEventListener('storage', this.syncLoginState);
  },
  beforeUnmount() {
    window.removeEventListener('storage', this.syncLoginState);
  },
  methods: {
    syncLoginState() {
      this.loggedIn = localStorage.getItem('token') !== null;
    }
  }
};
</script>

<style scoped>
* {
  box-sizing: border-box;
  font-family: 'Roboto', sans-serif;
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
  background-color: #ffffff;
  border-bottom: 2px solid #dee2e6;
  padding: 10px 15px;
  border-radius: 0.25rem;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.nav-item {
  margin-right: 10px;
}

.nav-link {
  color: #495057;
  font-weight: 500;
  padding: 10px 15px;
  border-radius: 0.25rem;
  transition: background-color 0.3s ease, color 0.3s ease;
}

.nav-link:hover,
.nav-link.active {
  background-color: #0d6efd;
  color: #ffffff;
}

.tab-content {
  background-color: #ffffff;
  padding: 20px;
  border-radius: 0.25rem;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}
</style>
