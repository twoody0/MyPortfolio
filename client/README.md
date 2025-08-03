# Tyler Woody's Developer Portfolio

Welcome to my personal developer portfolio — a modern, performant, and mobile-friendly site built with [Vue 3](https://vuejs.org/), [Vite](https://vitejs.dev/), and [Tailwind CSS](https://tailwindcss.com/). This site showcases my projects, technical skills, and development journey.

🔗 [Visit Live Site](https://www.tyler-woody.dev)

---

## 🚀 Features

- ⚡️ Fast loading with Vite
- 🎨 Clean and modern design with Tailwind CSS
- 🖼 Dynamic project gallery with flip animations
- 📱 Fully responsive design for all devices
- 🌐 Hosted with AWS S3 + CloudFront + Cloudflare
- 🔒 HTTPS with a custom SSL certificate via AWS ACM

---

## 🛠 Tech Stack

- **Frontend:** Vue 3, Vite, Tailwind CSS
- **Hosting:** AWS S3 (static site), AWS CloudFront (CDN)
- **Domain & DNS:** Cloudflare
- **SSL:** AWS ACM with HTTPS support

---

## 📁 Project Structure

```
my-portfolio/
├── public/            # Static assets (favicon, profile image)
├── src/
│   ├── assets/        # Component-specific assets
│   ├── components/    # Vue components like ProjectCard.vue
│   ├── pages/         # Main views (Home, Projects, About, etc.)
│   └── App.vue        # Root component
├── index.html         # HTML template
├── main.js            # App entry point
└── vite.config.js     # Vite dev/build config
```

---

## 🚀 Deployment (CI/CD with GitHub Actions + AWS)

This site is automatically deployed to AWS S3 + CloudFront using GitHub Actions whenever changes are pushed to the `master` branch.

### 🧠 How It Works

- GitHub Actions builds the project using `vite build`.
- The `dist/` folder is synced to the S3 bucket `tyler-woody-dev-site`.
- A CloudFront invalidation is optionally triggered to clear the cache and serve the latest version.

### 🛠 AWS Setup Required

Make sure you have these secrets configured in your GitHub repo:
- Settings -> Secrets and variables -> Actions -> New repository secret

| Secret Name         | Description                      |
|---------------------|----------------------------------|
| `AWS_ACCESS_KEY_ID` | IAM user access key              |
| `AWS_SECRET_ACCESS_KEY` | IAM user secret key         |
| `AWS_REGION`        | AWS region of your bucket        |
| `DISTRIBUTION_ID`   | (Optional) CloudFront distribution ID for cache invalidation |

### 🌍 CloudFront Cache Invalidation

```bash
aws cloudfront create-invalidation \
  --distribution-id <your-distribution-id> \
  --paths "/*"
```

---

## 🧩 DNS Notes

- `tyler-woody.dev` and `www.tyler-woody.dev` both point to the same CloudFront distribution.
- Cloudflare is used to manage DNS.
- SSL is managed by AWS ACM (covers both root and www domains).

---

## 🧠 Future Plans

- Add a blog with Markdown support
- Integrate GitHub Projects API

---

## 📫 Contact

Feel free to reach out or connect with me:

- GitHub: [github.com/tylerwoody](https://github.com/tylerwoody)
- LinkedIn: [linkedin.com/in/tylerwoody](https://www.linkedin.com/in/tylerwoody)
- Email: tyler@tyler-woody.dev

---

## 🔗 Related

- [Portfolio Backend](https://github.com/twoody0/MyPortfolio/tree/master/PortfolioBackend)

## 📄 License

This project is open source and available under the [MIT License](LICENSE).
