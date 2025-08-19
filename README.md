# 🌐 Tyler Woody — Developer Portfolio

This is the main monorepo for my personal developer portfolio. It includes both the **frontend** (client-facing Vue 3 site) and the **backend** (C# Web API for contact forms and future extensions).

> 🚀 Live site: [www.tyler-woody.dev](https://www.tyler-woody.dev)

---

## 📁 Project Structure

```
/
├── PortfolioFrontend/       # Vue 3 + Vite + Tailwind frontend
├── PortfolioBackend/        # ASP.NET Core Web API backend
├── PortfolioBackend.Tests/  # Backend unit tests
└── .github/workflows/       # CI/CD GitHub Actions
```

---

## 🛠 Tech Stack

- **Frontend:** Vue 3, Vite, Tailwind CSS
- **Backend:** ASP.NET Core Web API
- **Deployment:** AWS S3 + CloudFront (CI/CD via GitHub Actions)
- **Domain & DNS:** Cloudflare
- **Email:** AWS SES (contact form support)
- **CI/CD:** GitHub Actions

---

## 🔧 Setup Instructions

> Clone this repo and navigate to each subdirectory for detailed setup instructions in their respective READMEs:

- 📦 [Frontend README](./client/README.md)
- ⚙️ [Backend README](./PortfolioBackend/README.md)

---

## 🧪 Testing

Backend tests are included in the `PortfolioBackend.Tests` project and can be run with:

```bash
dotnet test
```

---

## 📄 License

This project is licensed under the MIT License. See [LICENSE.txt](./LICENSE.txt) for details.

---

## 🖼 Architecture Diagram

![Portfolio Architecture Diagram]((https://github.com/user-attachments/assets/7c8103a5-6dcb-4334-925a-6261a53d3e37))
