# 📡 Portfolio Backend (Contact Form API)

This backend service handles the contact form submissions from the [Portfolio Frontend](https://www.tyler-woody.dev/). It is a serverless application built with C# and deployed using **AWS Lambda** via **API Gateway**.

---

## ⚙️ Features

- Accepts contact form POST requests
- Validates user input
- Sends emails using **Amazon SES**
- Secure, serverless, and scalable

---

## 🛠 Tech Stack

- **Language:** C# (.NET 8 Lambda)
- **Platform:** AWS Lambda
- **Email Service:** Amazon SES
- **API Gateway:** REST endpoint for contact route
- **Infrastructure:** AWS CloudFormation / SAM / Console
- **CI/CD:** GitHub Actions (optional)

---

## 📬 Endpoint

```
POST /contact
Content-Type: application/json

{
  "name": "Your Name",
  "email": "your@email.com",
  "message": "Your message"
}
```

Response:
```json
{
  "message": "Email sent successfully!"
}
```

---

## 🔐 Security

- Uses IAM roles with least privilege to restrict SES access
- Input validation on name, email, and message fields

---

## 🌐 Deployment (Manual)

1. Build the Lambda using .NET CLI:
   ```bash
   dotnet publish -c Release
   ```

2. Deploy via AWS Console or CLI (e.g. `zip` and upload)

3. Make sure the following environment variables are set:
   - `TO_ADDRESS`: Email to receive messages
   - `FROM_ADDRESS`: Verified SES sender

---

## 🔗 Related

- [Frontend Portfolio Website](https://github.com/twoody0/MyPortfolio)