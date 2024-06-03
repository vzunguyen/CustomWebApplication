import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import('../views/Home.vue')
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('../views/Login.vue')
  },
  {
    path: '/register',
    name: 'Register',
    component: () => import('../views/Register.vue')
  },
  {
    path: '/search',
    name: 'Search',
    component: () => import('../views/Search.vue')
  },
  {
    path: '/profile',
    name: 'Profile',
    component: () => import('../views/Profile.vue')
  },
  {
    path: '/friends',
    name: 'Friends',
    component: () => import('../views/Friend.vue')
  },
  // {
  //   path: '/content/:id',
  //   name: 'Content',
  //   component: Content
  // },
  {
    path: '/create-content',
    name: 'CreateContent',
    component: () => import('../views/CreateContent.vue')
  },
  {
    path: '/edit-content/:id',
    name: 'EditContent',
    component: () => import('../views/EditContent.vue')
  },
  {
    path: '/unauthorized',
    name: 'UnAuthorized',
    component: () => import('../components/Unauthorized.vue')
  },
  {
    path: '/logout',
    name: 'Logout',
    component: () => import('../views/Logout.vue')
  },
  {
    path: '/:username',
    name: 'UnauthorizedProfile',
    component: () => import('../views/UnauthorizedProfile.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router;