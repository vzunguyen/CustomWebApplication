<template>
  <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card mt-5">
          <div class="card-header">
            <h1 class="text-center">Login</h1>
          </div>
          <div class="card-body">
            <form @submit.prevent="handleLogin">
              <div class="form-group">
                <input type="text" v-model="username" class="form-control" style="margin-bottom: 1rem;" placeholder="Username" required>
              </div>
              <div class="form-group">
                <input type="password" v-model="password" class="form-control" style="margin-bottom: 1rem;" placeholder="Password" required>
              </div>
              <button type="submit" class="btn btn-primary btn-block">Login</button>
            </form>
          </div>
          <div class="card-footer text-center">
            <router-link to="/register">Don't have an account? Register</router-link>
          </div>
        </div>
      </div>
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