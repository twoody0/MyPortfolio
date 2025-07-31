<template>
  <section class="min-h-screen bg-gray-900 text-white px-4 py-20 flex justify-center items-center">
    <div class="w-full max-w-2xl">
      <h1 class="text-4xl font-bold mb-6 text-center">Contact Me</h1>
      <p class="mb-8 text-gray-300 text-center">Feel free to reach out via this form or email me directly.</p>

      <form @submit.prevent="handleSubmit" class="space-y-6">
        <div>
          <label class="block mb-1 text-gray-400" for="name">Name</label>
          <input id="name"
                 name="name"
                 v-model="form.name"
                 required
                 type="text"
                 class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded focus:outline-none focus:ring focus:border-indigo-500" />
        </div>

        <div>
          <label class="block mb-1 text-gray-400" for="email">Email</label>
          <input id="email"
                 name="email"
                 v-model="form.email"
                 required
                 type="email"
                 class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded focus:outline-none focus:ring focus:border-indigo-500" />
        </div>

        <div>
          <label class="block mb-1 text-gray-400" for="message">Message</label>
          <textarea id="message"
                    name="message"
                    v-model="form.message"
                    required
                    rows="5"
                    class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded focus:outline-none focus:ring focus:border-indigo-500"></textarea>
        </div>

        <button type="submit"
                class="bg-indigo-600 hover:bg-indigo-700 text-white font-semibold px-6 py-3 rounded transition w-full">
          Send Message
        </button>
      </form>

      <!-- Success message -->
      <p v-if="success" class="mt-6 text-green-400 text-center">
        ✅ Thanks! Your message has been sent.
      </p>

      <!-- Error message -->
      <p v-if="error" class="mt-6 text-red-400 text-center">
        ❌ Something went wrong. Please try again.
      </p>
    </div>
  </section>
</template>

<script setup>
  import { ref } from 'vue'

  const form = ref({
    name: '',
    email: '',
    message: ''
  })

  const success = ref(false)
  const error = ref(false)

  async function handleSubmit() {
    success.value = false
    error.value = false

    try {
      const response = await fetch('https://formspree.io/f/xovllpgg', {
        method: 'POST',
        headers: {
          Accept: 'application/json'
        },
        body: new FormData(document.querySelector('form'))
      })

      if (response.ok) {
        success.value = true
        form.value = { name: '', email: '', message: '' }
      } else {
        error.value = true
      }
    } catch (err) {
      error.value = true
    }
  }
</script>
