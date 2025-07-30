<template>
  <section class="min-h-screen bg-gray-900 text-white px-6 py-20">
    <div class="max-w-6xl mx-auto space-y-12">
      <h1 class="text-4xl font-bold border-b border-gray-700 pb-4">Projects</h1>

      <div class="grid gap-10 md:grid-cols-2">
        <!-- Project Card -->
        <div v-for="project in projects"
             :key="project.title"
             class="bg-gray-800 rounded-lg shadow-md overflow-hidden hover:scale-[1.01] transition">
          <!-- Thumbnail -->
          <img v-if="project.thumbnail"
               :src="project.thumbnail"
               :alt="project.title + ' preview'"
               class="w-full h-48 object-cover" />

          <!-- Details -->
          <div class="p-6">
            <h2 class="text-2xl font-semibold mb-2">{{ project.title }}</h2>
            <p class="text-gray-300 mb-4">{{ project.description }}</p>

            <!-- Tech Tags Grouped by Category -->
            <div v-for="(techs, category) in project.techStack" :key="category" class="mb-4">
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

            <!-- Links -->
            <div class="flex gap-4 mt-4">
              <a v-if="project.link"
                 :href="project.link"
                 class="text-indigo-400 hover:underline"
                 target="_blank">
                Live Site
              </a>
              <a v-if="project.github"
                 :href="project.github"
                 class="text-gray-400 hover:underline"
                 target="_blank">
                GitHub Repo
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
  const projects = [
    {
      title: "AI Storybook Generator",
      description:
        "A full-stack web app that lets users create personalized, illustrated children's stories using OpenAI. Built with React and Vite. It stores structured data in Azure SQL and images in Azure Blob Storage.",
      thumbnail: "/images/storybook.jpg",
      techStack: {
        Frontend: ["React", "Vite", "Tailwind CSS"],
        Backend: ["ASP.NET Core", "C#"],
        API: ["OpenAI API"],
        Cloud: ["Azure SQL", "Azure Blob Storage"]
      },
      link: "https://your-live-site.com",
      github: "https://github.com/twoody0/storybook-ai"
    },
    {
      title: "Workout Tracker App",
      description:
        "A cross-platform .NET MAUI mobile app for tracking workouts, logging weight/reps/sets, and syncing with a shared leaderboard across gym locations.",
      thumbnail: "/images/workout-tracker.jpg",
      techStack: {
        Frontend: [".NET MAUI"],
        Backend: ["C#", "SQLite"],
        Architecture: ["Dependency Injection"]
      },
      github: "https://github.com/twoody0/workout-tracker"
    },
    {
      title: "Portfolio Website",
      description:
        "This portfolio website! Built with Vue 3, Vite, and Tailwind CSS. Features smooth animations, multi-page routing, and responsive design.",
      thumbnail: "/images/portfolio.jpg",
      techStack: {
        Frontend: ["Vue", "Vite", "Tailwind CSS", "JavaScript"]
      },
      github: "https://github.com/twoody0/portfolio"
    }
  ]
  function techTagColor(category) {
    switch (category) {
      case "Frontend":
        return "bg-indigo-600";
      case "Backend":
        return "bg-green-600";
      case "API":
        return "bg-pink-600";
      case "Cloud":
        return "bg-blue-600";
      default:
        return "bg-gray-700";
    }
  }
</script>
