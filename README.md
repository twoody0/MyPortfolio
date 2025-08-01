# Tyler Woody's Developer Portfolio

Welcome to my personal developer portfolio — a modern, performant, and mobile-friendly site built with [Vue 3](https://vuejs.org/), [Vite](https://vitejs.dev/), and [Tailwind CSS](https://tailwindcss.com/). This site showcases my projects, technical skills, and development journey.

?? [Visit Live Site](https://www.tyler-woody.dev)

---

## ?? Features

- ?? Fast loading with Vite
- ?? Clean and modern design with Tailwind CSS
- ?? Dynamic project gallery with flip animations
- ?? Fully responsive design for all devices
- ?? Hosted with AWS S3 + CloudFront + Route 53 + Cloudflare
- ?? HTTPS with a custom SSL certificate via AWS ACM

---

## ?? Tech Stack

- **Frontend:** Vue 3, Vite, Tailwind CSS
- **Hosting:** AWS S3 (static site), AWS CloudFront (CDN)
- **Domain & DNS:** Route 53 + Cloudflare
- **SSL:** AWS ACM with HTTPS support

---

## ?? Project Structure

```
my-portfolio/
??? public/            # Static assets (favicon, profile image)
??? src/
?   ??? assets/        # Component-specific assets
?   ??? components/    # Vue components like ProjectCard.vue
?   ??? pages/         # Main views (Home, Projects, About, etc.)
?   ??? App.vue        # Root component
??? index.html         # HTML template
??? main.js            # App entry point
??? tailwind.config.js # Tailwind custom config
??? vite.config.js     # Vite dev/build config
```

---

## ?? Deployment (AWS)

This site is built and deployed manually via AWS CLI:

### ?? Build the Site

```bash
npm run build
```

### ?? Upload to S3

```bash
aws s3 sync dist/ s3://tyler-woody-dev-site --delete
```

### ?? CloudFront Cache Invalidation

```bash
aws cloudfront create-invalidation \
  --distribution-id <your-distribution-id> \
  --paths "/*"
```

---

## ?? DNS Notes

- `tyler-woody.dev` and `www.tyler-woody.dev` both point to the same CloudFront distribution.
- Cloudflare is used to manage DNS.
- SSL is managed by AWS ACM (covers both root and www domains).

---

## ?? Future Plans

- Add a blog with Markdown support
- Integrate GitHub Projects API

---

## ?? Contact

Feel free to reach out or connect with me:

- GitHub: [github.com/tylerwoody](https://github.com/tylerwoody)
- LinkedIn: [linkedin.com/in/tylerwoody](https://www.linkedin.com/in/tylerwoody)
- Email: tyler@tyler-woody.dev

---

## ?? License

This project is open source and available under the [MIT License](LICENSE).
