<template>
  <section class="min-h-screen bg-gray-900 text-white px-6 py-20">
    <div class="max-w-6xl mx-auto space-y-12">
      <h1 class="text-4xl font-bold border-b border-gray-700 pb-4">Projects</h1>

      <div class="grid gap-10 md:grid-cols-2">
        <div v-for="(project, i) in projects"
             :key="project.title"
             class="perspective">
          <div class="bg-gray-800 rounded-lg shadow-md overflow-hidden transform transition duration-700"
               data-card
               :style="{ opacity: '0' }"
               :class="[
            i === 0 ? 'animation-delay-100' :
            i === 1 ? 'animation-delay-300' :
            i === 2 ? 'animation-delay-500' : ''
          ]">
            <img v-if="project.thumbnail"
                 :src="project.thumbnail"
                 :alt="project.title + ' preview'"
                 class="w-full h-48 object-cover" />

            <div class="p-6">
              <h2 class="text-2xl font-semibold mb-2">{{ project.title }}</h2>
              <p class="text-gray-300 mb-4">{{ project.description }}</p>

              <div v-for="(techs, category) in project.techStack"
                   :key="category"
                   class="mb-4">
                <h3 class="text-sm text-gray-400 mb-1 uppercase tracking-widest">{{ category }}</h3>
                <div class="flex flex-wrap gap-2">
                  <span v-for="tech in techs"
                        :key="tech"
                        class="px-3 py-1 text-sm rounded"
                        :class="techTagColor(category)">
                    {{ tech }}
                  </span>
                </div>
              </div>

              <div class="flex gap-4 mt-4">
                <a v-if="project.link"
                   :href="project.link"
                   class="text-indigo-400 hover:underline"
                   target="_blank">Live Site</a>
                <a v-if="project.github"
                   :href="project.github"
                   class="text-gray-400 hover:underline"
                   target="_blank">GitHub Repo</a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
  import { onMounted, nextTick } from 'vue'

  const projects = [
    {
      title: "AI Storybook Generator",
      description:
        "A full-stack web app that lets users create personalized, illustrated children's stories using OpenAI.",
      thumbnail: "/storybook.png",
      techStack: {
        Frontend: ["React", "Vite", "Tailwind CSS"],
        Backend: ["ASP.NET Core", "C#"],
        API: ["OpenAI API"],
        Cloud: ["Azure SQL", "Azure Blob Storage"]
      },
      link: "https://your-live-site.com",
      github: "https://github.com/AustinH5544/Hackathon-2025"
    },
    {
      title: "Workout Tracker App",
      description:
        "A cross-platform .NET MAUI mobile app for tracking workouts and syncing with gym leaderboards.",
      thumbnail: "/workout-tracker.png",
      techStack: {
        Frontend: [".NET MAUI"],
        Backend: ["C#", "SQLite"],
        Architecture: ["Dependency Injection"]
      },
      github: "https://github.com/twoody0/WorkoutTracker"
    },
    {
      title: "Portfolio Website",
      description:
        "This is my live portfolio website! Built with Vue 3, Vite, and Tailwind CSS for the frontend. The backend is powered by AWS Lambda and API Gateway, with email notifications sent via Amazon SES. Static hosting is served through an S3 bucket and distributed globally with CloudFront.",
      thumbnail: "/portfolio.png",
      techStack: {
        Frontend: ["Vue", "Vite", "Tailwind CSS", "JavaScript"],
        Backend: ["C#", "AWS Lambda"],
        API: ["API Gateway", "AWS SES"],
        Cloud: ["S3", "CloudFront", "IAM", "ACM"],
        Architecture: ["Serverless", "Environment Variables"]
      },
      github: "https://github.com/twoody0/MyPortfolio"
    }
  ]

  function techTagColor(category) {
    switch (category) {
      case "Frontend": return "bg-indigo-600"
      case "Backend": return "bg-green-600"
      case "API": return "bg-pink-600"
      case "Cloud": return "bg-blue-600"
      case "Architecture": return "bg-yellow-600"
      default: return "bg-gray-700"
    }
  }

  onMounted(async () => {
    await nextTick()

    const cardElements = document.querySelectorAll('[data-card]')

    const observer = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        if (entry.isIntersecting) {
          entry.target.classList.remove('opacity-0')
          entry.target.classList.add('animate-flipInUp')
          observer.unobserve(entry.target)
        }
      })
    }, { threshold: 0.1 })

    cardElements.forEach(el => observer.observe(el))
  })
</script>
