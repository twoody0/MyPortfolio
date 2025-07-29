<template>
  <section class="min-h-screen bg-gray-900 text-white flex items-center justify-center px-6">
    <div class="max-w-4xl text-center space-y-8 animate-fadeInUp">
      <!-- Profile Image -->
      <div class="flex justify-center">
        <img src="/profile.jpg"
             alt="Tyler Woody"
             class="w-80 h-80 rounded-full border-4 border-indigo-500 shadow-lg object-cover transition-transform duration-500 hover:scale-105" />
      </div>

      <!-- Name -->
      <h1 class="text-5xl font-bold opacity-0 animate-fadeInUp animation-delay-100">Hi, I'm Tyler Woody</h1>

      <!-- Value Prop -->
      <p class="text-xl text-gray-300 opacity-0 animate-fadeInUp animation-delay-300">
        I build performant web experiences with clean, modern code.
      </p>

      <!-- CTA -->
      <router-link to="/projects"
                   class="inline-block mt-4 bg-indigo-600 hover:bg-indigo-700 text-white font-semibold px-6 py-3 rounded transition opacity-0 animate-fadeIn animation-delay-500">
        View My Work
      </router-link>
    </div>
  </section>
</template>

<script setup>
  // No script logic needed yet
</script>
