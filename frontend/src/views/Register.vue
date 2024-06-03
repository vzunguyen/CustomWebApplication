<template>
  <div class="register-container">
    <div class="row">
      <div class="col-md-6 offset-md-3">
        <div class="register-card card">
          <div class="card-body">
            <h2 class="card-title text-center mb-4">Register</h2>
            <form @submit.prevent="handleRegister">
              <div class="form-group">
                <label for="email" class="form-label">Email:</label>
                <input
                    type="email"
                    id="email"
                    v-model="email"
                    placeholder="Email"
                    required
                    class="form-control"
                />
              </div>
              <div class="form-group">
                <label for="username" class="form-label">Username:</label>
                <input
                    type="text"
                    id="username"
                    v-model="username"
                    placeholder="Username"
                    required
                    class="form-control"
                />
              </div>
              <div class="form-group">
                <label for="password" class="form-label">Password:</label>
                <input
                    type="password"
                    id="password"
                    v-model="password"
                    placeholder="Password"
                    required
                    class="form-control"
                />
              </div>
              <button type="submit" class="btn btn-primary btn-block">
                Register
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-md-6 offset-md-3 text-center mt-3">
        <router-link to="/login">Already have an account? Login</router-link>
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
        // Handle successful registration, e.g., store token, redirect to home page
        console.log(response.data);
        // Redirect to login page after successful registration
        router.push('/login');
      } catch (error) {
        // Handle registration error
        console.error(error);
      }
    }
  }
}
</script>

<style scoped>
.register-container {
  min-height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #f8f9fa;
}


.card-title {
  font-size: 24px;
  font-weight: bold;
  color: #333;
}

.form-group {
  margin-bottom: 20px;
}

.form-label {
  font-weight: 500;
  color: #555;
}

.form-control {
  border-radius: 5px;
  padding: 10px;
  border: 1px solid #ccc;
}

.btn-primary {
  background-color: #007bff;
  border-color: #007bff;
  font-weight: 500;
  padding: 10px;
  transition: background-color 0.3s;
}

.btn-primary:hover {
  background-color: #0069d9;
  border-color: #0062cc;
}

@media (max-width: 576px) {
  .register-card {
    max-width: 100%;
    border-radius: 0;
    box-shadow: none;
  }
}
</style>