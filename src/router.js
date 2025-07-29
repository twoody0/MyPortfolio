import { createRouter, createWebHistory } from 'vue-router'
import Home from './pages/Home.vue'
import Projects from './pages/Projects.vue'
import Certifications from './pages/Certifications.vue'
import Contact from './pages/Contact.vue'
import About from './pages/About.vue'
import Skills from './pages/Skills.vue'

const routes = [
  { path: '/', name: 'Home', component: Home },
  { path: '/projects', name: 'Projects', component: Projects },
  { path: '/certifications', name: 'Certifications', component: Certifications },
  { path: '/contact', name: 'Contact', component: Contact },
  { path: '/about', name: 'About', component: About },
  { path: '/skills', name: 'Skills', component: Skills },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
