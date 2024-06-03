<template>
  <div class="login">
    <h1>Login</h1>
    <div class="row">
      <form @submit.prevent="handleLogin">
        <input type="text" v-model="username" placeholder="Username" required>
        <input type="password" v-model="password" placeholder="Password" required>
        <button type="submit">Login</button>
      </form>
    </div>
    <div class="row">
      <router-link to="/register">Don't have an account? Register</router-link>
    </div>
  </div>
</template>

<script>
import api from '../services/api'
import router from "../router/index";

export default {
  name: 'LoginView',
  data() {
    return {
      username: '',
      password: '',
      loggedIn: false,
      usernameRules: [
        (v) => !!v || "Name is required",
        (v) => (v && v.length <= 10) || "Name must be less than 10 characters",
      ],
      //defining username rules for validation
      passwordRules: [
        (v) => !!v || "Password is required",
        (v) =>
            (v && v.length >= 8) || "Password must be more than 8 characters",
      ],
    }
  },
  methods: {
    async handleLogin() {
      try {
        const response = await api.login({
          username: this.username,
          password: this.password
        })
        console.log(response.data)
        localStorage.setItem('token', response.data.token);
        this.loggedIn = true;
        // redirect to home page
        router.push('/');
      } catch (error) {
        // Handle login error
        console.error(error);
        alert('Invalid username or password');
      }
    }
  }
}
</script>
