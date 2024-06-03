<template>
  <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card mt-5">
          <div class="card-header">
            <h1 class="text-center">Register</h1>
          </div>
          <div class="card-body">
            <form @submit.prevent="handleRegister">
              <div class="form-group">
                <input
                  type="email"
                  v-model="email"
                  class="form-control"
                  style="margin-bottom: 1rem;"
                  placeholder="Email"
                  required
                />
              </div>
              <div class="form-group">
                <input
                  type="text"
                  v-model="username"
                  class="form-control"
                  style="margin-bottom: 1rem;"
                  placeholder="Username"
                  required
                />
              </div>
              <div class="form-group">
                <input
                  type="password"
                  v-model="password"
                  class="form-control"
                  style="margin-bottom: 1rem;"
                  placeholder="Password"
                  required
                />
              </div>
              <button type="submit" class="btn btn-primary btn-block">Register</button>
            </form>
          </div>
          <div class="card-footer text-center">
            <router-link to="/login">Already have an account? Login</router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import api from '../services/api';
import router from "../router/index";

export default {
  name: 'RegisterView',
  data() {
    return {
      username: '',
      password: '',
      email: ''
    }
  },
  methods: {
    async handleRegister() {
      try {
        const response = await api.register({
          username: this.username,
          password: this.password,
          email: this.email
        });
        console.log(response.data);
        router.push('/login');
      } catch (error) {
        console.error(error);
      }
    }
  }
}
</script>
