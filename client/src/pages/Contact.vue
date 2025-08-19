<template>
  <section class="min-h-screen bg-gray-900 text-white px-4 py-20 flex justify-center items-center">
    <div class="w-full max-w-2xl">
      <h1 class="text-4xl font-bold mb-6 text-center">Contact Me</h1>
      <p class="mb-8 text-gray-300 text-center">Feel free to reach out via this form or email me directly.</p>

      <form @submit.prevent="sendContact" class="max-w-xl mx-auto space-y-6">
        <!-- Name -->
        <div>
          <label class="block mb-1 text-gray-400" for="name">Name</label>
          <input id="name" type="text" v-model="name"
                 class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded" />
          <p v-if="errors.name" class="text-red-500 text-sm mt-1">{{ errors.name }}</p>
        </div>

        <!-- Email -->
        <div>
          <label class="block mb-1 text-gray-400" for="email">Email</label>
          <input id="email" type="email" v-model="email"
                 class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded" />
          <p v-if="errors.email" class="text-red-500 text-sm mt-1">{{ errors.email }}</p>
        </div>

        <!-- Message -->
        <div>
          <label class="block mb-1 text-gray-400" for="message">Message</label>
          <textarea id="message" v-model="message" rows="5"
                    class="w-full px-4 py-2 bg-gray-800 text-white border border-gray-700 rounded"></textarea>
          <p v-if="errors.message" class="text-red-500 text-sm mt-1">{{ errors.message }}</p>
        </div>

        <!-- Turnstile widget (managed/visible) -->
        <div ref="turnstileEl"
             class="cf-turnstile"
             data-sitekey="YOUR_TURNSTILE_SITE_KEY"
             data-callback="onTurnstileSuccess"
             data-expired-callback="onTurnstileExpired"
             data-error-callback="onTurnstileError"
             data-action="contact_submit"
             data-theme="dark">
        </div>

        <!-- Disable until token present or while submitting -->
        <button type="submit"
                class="bg-indigo-600 hover:bg-indigo-700 text-white font-semibold px-6 py-3 rounded transition"
                :disabled="submitting || !turnstileToken">
          <span v-if="!submitting">Send Message</span>
          <span v-else>Sending…</span>
        </button>
      </form>

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
  import { ref, onMounted, onBeforeUnmount } from 'vue'
  import { API_BASE } from '../constants'

  const name = ref('')
  const email = ref('')
  const message = ref('')
  const errors = ref({})
  const successMessage = ref('')
  const errorMessage = ref('')
  const submitting = ref(false)

  const turnstileToken = ref('')
  const turnstileEl = ref(null)
  let widgetId = null

  // Callbacks used by the managed widget
  window.onTurnstileSuccess = (token) => { turnstileToken.value = token }
  window.onTurnstileExpired = () => { turnstileToken.value = '' }
  window.onTurnstileError = () => { turnstileToken.value = '' }

  // Simple client-side validation referenced
  const validateForm = () => {
    errors.value = {}
    if (!name.value.trim()) errors.value.name = 'Name is required'
    if (!email.value.trim()) errors.value.email = 'Email is required'
    else if (!/\S+@\S+\.\S+/.test(email.value)) errors.value.email = 'Email is invalid'
    if (!message.value.trim()) errors.value.message = 'Message is required'
    return Object.keys(errors.value).length === 0
  }

  const resetTurnstile = () => {
    if (window.turnstile && widgetId !== null) window.turnstile.reset(widgetId)
    turnstileToken.value = ''
  }

  onMounted(() => {
    const tryRender = () => {
      if (window.turnstile && turnstileEl.value && widgetId === null) {
        widgetId = window.turnstile.render(turnstileEl.value, {
          sitekey: '0x4AAAAAABtD0JIaZdxLK5Jd',
          callback: 'onTurnstileSuccess',
          'expired-callback': 'onTurnstileExpired',
          'error-callback': 'onTurnstileError',
          action: 'contact_submit',
          theme: 'dark'
        })
      }
    }
    const id = setInterval(() => {
      if (window.turnstile) { tryRender(); clearInterval(id) }
    }, 100)
  })

  onBeforeUnmount(() => {
    if (window.turnstile && widgetId !== null) {
      try { window.turnstile.remove(widgetId) } catch { }
    }
  })

  const sendContact = async () => {
    successMessage.value = ''
    errorMessage.value = ''

    if (!validateForm()) return
    if (!turnstileToken.value) {
      errorMessage.value = 'Please complete the verification first.'
      return
    }

    submitting.value = true
    try {
      const response = await fetch(`${API_BASE}/contact`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          name: name.value,
          email: email.value,
          message: message.value,
          cf_turnstile_token: turnstileToken.value
        })
      })
      const result = await response.json().catch(() => ({}))

      if (response.ok) {
        successMessage.value = result.message || 'Message sent!'
        name.value = ''; email.value = ''; message.value = ''
      } else {
        errorMessage.value = result.message || 'Something went wrong.'
        console.error('Server error:', result)
      }
    } catch (err) {
      errorMessage.value = 'Network or verification error. Please try again.'
      console.error(err)
    } finally {
      resetTurnstile()   // get a new token for the next submit
      submitting.value = false
    }
  }
</script>
