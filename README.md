# 🎮 AI-game-generator-agent
AI game generator agent
A portfolio-ready, interview-focused microservices project built in **.NET Core**, designed to:

* Generate **funny, simple, addictive video games** using an **AI agent**.
* Pull inspiration from **Google Trends**.
* Use **clean architecture (DDD, MVC, Repository Pattern)**.
* Use **secure coding**, zero-trust principles, env-based secrets.
* Use **Supabase** for state storage.
* Integrate LLMs via **Groq**, abstracted so you can swap to OpenAI later.
* Include **LangChain + LangGraph + LangSmith** (optional but structured).
* Communicate between services **only via RabbitMQ**.
* Provide **Swagger documentation** for every microservice.
* Provide an **API Gateway**.
* Be simple, clean, and perfect for senior .NET interview discussions.

---

# 🚀 Project Goals

This is **not production-ready**. It’s structured to:

* Demonstrate architectural knowledge.
* Show understanding of microservices, event-driven systems, DDD.
* Use secure patterns (no shared models, message-driven communication).
* Impress interviewers.

---

# 🏗 Architecture Overview

```
+----------------------+          +-----------------------+
|  Google Trends Svc   | --msg--> |  Game Generator Svc   |
+----------------------+          +-----------------------+
             |                                 |
             |                                 | --msg--> Game Template Svc
             |                                 v
        RabbitMQ <------------------------- Game Storage Svc
             ^                                   |
             |                                   v
          API Gateway ----------------------> Supabase
```

### Microservices

Each microservice:

* Uses **.NET Core + MVC**
* Implements **DDD layers**:

  * Controller (API)
  * BL / Services
  * DAL / Repository
  * Domain Models
* Has its own:

  * **Swagger UI**
  * **README.md**
  * **Dockerfile**
* Does NOT share models.
* Communicates **only via RabbitMQ message contracts**.

### Services Included

* **GoogleTrendsService** — Pulls trends from API.
* **GameGeneratorService** — Uses LLM agent to create simple game descriptions.
* **GameTemplateService** — Converts description → actual game template code.
* **GameStorageService** — Stores game artifacts in Supabase.
* **APIGateway** — Routes user input and hides internal services.

### Technologies Used

| Topic          | Tech                                           |
| -------------- | ---------------------------------------------- |
| Message broker | RabbitMQ                                       |
| Database       | Supabase Postgres                              |
| AI Integration | Groq LLM, LangChain, LangGraph, LangSmith      |
| Runtime        | .NET 8                                         |
| Architecture   | DDD + MVC + Repository Pattern + Microservices |
| Security       | Zero-Trust (no secrets in code)                |

---

# 🔐 Security Principles

* **Zero Trust:** No hard-coded keys, tokens, URLs, or credentials.
* Uses **Environment Variables** everywhere.
* GitHub Secrets for CI/CD.
* DAL layer validates all input.
* All queues use separate credentials.
* Supabase service role key is **never** stored in code.

---

# 📦 Repository Structure

```
TrendGames/
 ├── api-gateway/
 ├── services/
 │    ├── GoogleTrendsService/
 │    ├── GameGeneratorService/
 │    ├── GameTemplateService/
 │    └── GameStorageService/
 ├── shared/
 │    └── messaging-contracts/ (only queue DTOs)
 ├── infra/
 │    ├── docker/
 │    ├── rabbitmq/
 │    └── supabase/
 ├── install.sh / install.ps1 / install.bat
 └── README.md (this file)
```

---

# 🧩 Example: GameGeneratorService (Simplified)

This is included in full in your project. It contains:

* Controller
* BL
* DAL
* Domain
* Repository
* Messaging producer
* LLM client

The microservice listens for trends and produces **game ideas sent to RabbitMQ**.

---

# 🛠 Installation

This repo includes a cross-platform installer.

### 1️⃣ Clone the repo

```bash
git clone https://github.com/YOUR_USERNAME/trendgames.git
cd trendgames
```

### 2️⃣ Run the installer

| OS      | Command        |
| ------- | -------------- |
| Windows | `install.bat`  |
| macOS   | `./install.sh` |
| Linux   | `./install.sh` |

The install script:

* Installs .NET 8 if missing
* Installs RabbitMQ via Docker
* Installs Supabase CLI
* Restores all NuGet dependencies

---

# ⚙️ Running the project

### Start infrastructure

```bash
docker compose up -d
```

### Start all microservices

```bash
dotnet build
./run-all.sh
```

(or run each service individually)

---

# 🌐 API Gateway

* Acts as a single entry point.
* Exposes:

  * Start new trend → game generation
  * Get generated games

Swagger available at:

```
http://localhost:5000/swagger
```

---

# 📚 LangChain / LangGraph / LangSmith Integration

The LLM agent pipeline:

```
GoogleTrend → LangChain (format) → LangGraph (state machine) → Groq → LangSmith Observability
```

* LangChain: prompt templates, output parsing.
* LangGraph: agent workflow.
* LangSmith: tracing and debugging.

The LLM client is injected via interface so you can swap Groq → OpenAI.

---

# 📝 Each Microservice Has Its Own README

Inside each folder:

* **Architecture overview**
* **API endpoints + Swagger URL**
* **RabbitMQ events this service publishes/consumes**
* **Setup instructions**

---

# 📦 Message Contracts (Shared)

Only message types are shared between services.
Everything else is isolated.

Example event:

```csharp
public class TrendFoundEvent
{
    public String TrendName { get; set; }
    public DateTime RetrievedAt { get; set; }
}
```

---

# 📄 Environment Variables

Create a `.env` file (never commit it!)

```
SUPABASE_URL=...
SUPABASE_KEY=...
RABBITMQ_USER=...
RABBITMQ_PASS=...
GROQ_API_KEY=...
```

---

# 🎤 How to Explain in Interviews

Use this:

### "This project shows that I can design a scalable microservices ecosystem…"

* Fully event-driven.
* Strict separation (no shared models, only messages).
* Clean architecture per service.
* The game creation pipeline uses AI agents, LangChain, LangGraph, LangSmith.
* Supabase stores cross-service state securely.
* RabbitMQ ensures loose coupling.
* API Gateway abstracts internals.
* Zero-trust security.
* Everything containerized, everything documented.

Interviewers love seeing:

* Event-driven design
* DDD separation
* LLM integration
* API Gateway
* Secure coding

---

# ✅ Status

**Phase 1 (completed):** Project skeleton + GameGeneratorService + README
**Phase 2 (next):** Generate microservices
