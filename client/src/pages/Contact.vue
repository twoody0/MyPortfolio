<template>
  <section class="min-h-screen bg-gray-900 text-white px-4 py-20 flex justify-center items-center">
    <div class="w-full max-w-2xl">
      <h1 class="text-4xl font-bold mb-6 text-center">Contact Me</h1>
      <p class="mb-8 text-gray-300 text-center">Feel free to reach out via this form or email me directly.</p>

      <form @submit.prevent="sendContact" class="max-w-xl mx-auto space-y-6">
        <div>
          <label class="block mb-1 text-gray-400" for="name">Name</label>
          <input id="name" type="text" v-model="name"
                 class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded" />
          <p v-if="errors.name" class="text-red-500 text-sm mt-1">{{ errors.name }}</p>
        </div>

        <div>
          <label class="block mb-1 text-gray-400" for="email">Email</label>
          <input id="email" type="email" v-model="email"
                 class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded" />
          <p v-if="errors.email" class="text-red-500 text-sm mt-1">{{ errors.email }}</p>
        </div>

        <div>
          <label class="block mb-1 text-gray-400" for="message">Message</label>
          <textarea id="message" v-model="message" rows="5"
                    class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded"></textarea>
          <p v-if="errors.message" class="text-red-500 text-sm mt-1">{{ errors.message }}</p>
        </div>

        <button type="submit"
                class="bg-indigo-600 hover:bg-indigo-700 text-white font-semibold px-6 py-3 rounded transition">
          Send Message
        </button>
      </form>

      <!-- Success message -->
      <p v-if="successMessage" class="text-green-500 text-center font-semibold mt-4">
        {{ successMessage }}
      </p>

      <p v-if="errorMessage" class="text-red-500 text-center font-semibold mt-4">
        {{ errorMessage }}
      </p>
    </div>
  </section>
</template>

<script setup>
  import { ref } from 'vue'
  import { API_BASE } from '../constants'

  const name = ref('')
  const email = ref('')
  const message = ref('')
  const errors = ref({})
  const successMessage = ref('')
  const errorMessage = ref('')

  const MAX_NAME = 80
  const MAX_EMAIL = 254
  const MAX_MESSAGE = 4000
  const EMAIL_RE = /^[^\s@]+@[^\s@]+\.[^\s@]+$/

  function trimAndCollapse(str) {
    return str.replace(/\s+/g, ' ').trim()
  }

  const validateForm = () => {
    errors.value = {}

    const n = trimAndCollapse(name.value)
    const e = email.value.trim()
    const m = message.value

    if (!n) errors.value.name = 'Name is required'
    else if (n.length > MAX_NAME) errors.value.name = `Name must be ≤ ${MAX_NAME} chars`

    if (!e) errors.value.email = 'Email is required'
    else if (e.length > MAX_EMAIL || !EMAIL_RE.test(e)) errors.value.email = 'Email is invalid'

    if (!m.trim()) errors.value.message = 'Message is required'
    else if (m.length > MAX_MESSAGE) errors.value.message = `Message must be ≤ ${MAX_MESSAGE} chars`

    // write back normalized values so I don’t send weird whitespace
    name.value = n
    email.value = e
    message.value = m

    return Object.keys(errors.value).length === 0
  }

  const sendContact = async () => {
    successMessage.value = ''
    errorMessage.value = ''

    if (!validateForm()) return

    try {
      const response = await fetch(`${API_BASE}/Contact`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          name: name.value,
          email: email.value,
          message: message.value
        })
      })

      const result = await response.json().catch(() => ({}))
      if (response.ok) {
        successMessage.value = 'Message sent!'
        name.value = ''
        email.value = ''
        message.value = ''
      } else {
        errorMessage.value = 'Something went wrong. Please try again.'
        console.error('Server error:', result)
      }
    } catch (err) {
      errorMessage.value = 'Network error. Please try again later.'
      console.error('Network error:', err)
    }
  }
</script>
