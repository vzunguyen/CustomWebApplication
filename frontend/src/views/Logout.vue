<template>
  <nav>
    <!-- Other navigation items -->
    <h1>Log Out</h1>
    <button v-if="loggedIn" @click="handleLogout">Logout</button>
    <!-- Other navigation items -->
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