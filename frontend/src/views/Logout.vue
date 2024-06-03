<template>
  <nav>
    <div class="container">
    <!-- Other navigation items -->
    <h1 class="mb-4">Log Out</h1>
    <button class="btn btn-primary" style="margin-top: 2em;" v-if="loggedIn" @click="handleLogout">Logout</button>
    <!-- Other navigation items -->
    </div>
  </nav>
</template>

<script>
import { logout } from '../services/api'

export default {
  name: "LogoutView",
  // Other component options
  computed: {
    loggedIn() {
      return localStorage.getItem('token') !== null
    }
  },
  methods: {
    handleLogout() {
      logout()
      // Redirect the user to the login page or any other appropriate page
      this.$router.push('/login')
    }
  }
}
</script>