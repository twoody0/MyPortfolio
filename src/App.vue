<template>
  <div class="min-h-screen bg-gray-900 text-white font-sans">
    <nav :class="[
        'fixed top-0 left-0 right-0 z-50 bg-gray-800 py-4 px-8 text-lg font-medium flex justify-center gap-6 transition-transform duration-300',
        atTop ? 'translate-y-0' : '-translate-y-full'
      ]">
      <router-link to="/" class="link-underline">Home</router-link>
      <router-link to="/about" class="link-underline">About</router-link>
      <router-link to="/skills" class="link-underline">Skills</router-link>
      <router-link to="/projects" class="link-underline">Projects</router-link>
      <!--<router-link to="/certifications" class="link-underline">Certifications</router-link>-->
      <router-link to="/contact" class="link-underline">Contact</router-link>
    </nav>

    <!-- Padding to offset fixed nav height -->
    <div class="pt-5">
      <router-view />
    </div>
  </div>
</template>

<script setup>
  import { ref, onMounted, onBeforeUnmount } from 'vue'

  const atTop = ref(true)

  function handleScroll() {
    atTop.value = window.scrollY <= 10
  }

  onMounted(() => {
    handleScroll() // trigger once on mount
    window.addEventListener('scroll', handleScroll)
  })

  onBeforeUnmount(() => {
    window.removeEventListener('scroll', handleScroll)
  })
</script>
