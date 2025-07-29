<template>
  <div class="min-h-screen bg-gray-900 text-white font-sans">
    <nav class="bg-gray-800 p-4 flex justify-center gap-6 text-lg font-medium">
      <router-link to="/" class="link-underline">Home</router-link>
      <router-link to="/projects" class="link-underline">Projects</router-link>
      <router-link to="/certifications" class="link-underline">Certifications</router-link>
      <router-link to="/contact" class="link-underline">Contact</router-link>
    </nav>

    <router-view />
  </div>
</template>

<script setup>
  // No script needed here
</script>
