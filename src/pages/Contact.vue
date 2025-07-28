<template>
  <div class="min-h-screen bg-gray-900 text-white p-6">
    <h1 class="text-4xl font-bold mb-6">Contact Me</h1>
    <p class="mb-8 text-gray-300">Feel free to reach out via this form or email me directly.</p>

    <form class="max-w-xl space-y-6">
      <div>
        <label class="block mb-1 text-gray-400" for="name">Name</label>
        <input
          id="name"
          type="text"
          class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded focus:outline-none focus:ring focus:border-indigo-500"
        />
      </div>

      <div>
        <label class="block mb-1 text-gray-400" for="email">Email</label>
        <input
          id="email"
          type="email"
          class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded focus:outline-none focus:ring focus:border-indigo-500"
        />
      </div>

      <div>
        <label class="block mb-1 text-gray-400" for="message">Message</label>
        <textarea
          id="message"
          rows="5"
          class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded focus:outline-none focus:ring focus:border-indigo-500"
        ></textarea>
      </div>

      <button
        type="submit"
        class="bg-indigo-600 hover:bg-indigo-700 text-white font-semibold px-6 py-3 rounded transition"
      >
        Send Message
      </button>
    </form>
  </div>
</template>

<script setup>
// You can hook up this form to an email service like Formspree or use a backend API
</script>
