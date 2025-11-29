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
* Communicate between services **only via Supabase (as the communication layer)**.
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
        Supabase (as the communication layer) <------------------------- Game Storage Svc
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
* Communicates **only via Supabase (as the communication layer) message contracts**.

### Services Included

* **GoogleTrendsService** — Pulls trends from API.
* **GameGeneratorService** — Uses LLM agent to create simple game descriptions.
* **GameTemplateService** — Converts description → actual game template code.
* **GameStorageService** — Stores game artifacts in Supabase.
* **APIGateway** — Routes user input and hides internal services.

### Technologies Used

| Topic          | Tech                                           |
| -------------- | ---------------------------------------------- |
| Message broker | Supabase (as the communication layer)          |
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

The microservice listens for trends and produces **game ideas sent to Supabase (as the communication layer)**.

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
* Installs Supabase (as the communication layer) via Docker
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
* **Supabase (as the communication layer) events this service publishes/consumes**
* **Setup instructions**

---

# 📦 Data Flow (Simple Supabase-Based Communication)

To keep this project **as simple as possible**, there are **no message brokers, no schemas, and no formal contracts**.

All microservices communicate **only through Supabase tables** using simple JSON fields.

### How Communication Works

1. A service **writes a row** to a Supabase table.
2. Another service **polls for rows** where `status = 'pending'`.
3. That service processes the row and **updates it** with `status = 'done'` and output data.
4. All services remain independent and loosely coupled.

### Example Tables

```
trend_requests
- id (uuid)
- query (text)
- status (text)
- result (jsonb)

game_generation_requests
- id (uuid)
- trend_data (jsonb)
- status (text)
- generated_game (jsonb)

analytics_events
- id (uuid)
- event_type (text)
- payload (jsonb)
- created_at (timestamp)
```

### Benefits

* Extremely simple architecture.
* Easy to explain in interviews.
* Polyglot (.NET + Python) without extra layers.
* Supabase handles all persistence and auth.
* No message brokers, no schemas, no shared models.

### Why Data Contracts?

* Polyglot-safe: works with **.NET, Python, Node, Go**, etc.
* No shared DLLs or code dependencies.
* Each service owns its internal models.
* JSON Schemas define the structure of each table row.
* Easier to version (`v1`, `v2`).
* Perfect for interview-friendly microservice demos.

### How It Works

1. Each table has a corresponding JSON Schema in `/contracts`.
2. Services read and write rows that match the schema.
3. Incoming data is validated using JSON schema libraries.
4. Services communicate only through Supabase tables — not via queues.

### Example Contracts Folder

```
/contracts
   trend-request.schema.json
   trend-result.schema.json
   game-gen-request.schema.json
   game-gen-result.schema.json
   audit-log.schema.json
```

### Example JSON Schema Snippet

```
{
  "$schema": "https://json-schema.org/draft/2020-12/schema",
  "title": "TrendRequest",
  "type": "object",
  "properties": {
    "id": { "type": "string" },
    "query": { "type": "string" },
    "createdAt": { "type": "string", "format": "date-time" },
    "status": { "type": "string", "enum": ["pending", "processing", "done"] }
  },
  "required": ["id", "query", "createdAt", "status"]
}
```

### Example Validation

#### .NET (C#)

```
JsonSchema schema = JsonSchema.FromFileAsync("contracts/trend-request.schema.json").Result;
ValidationResult result = schema.Validate(jsonString);
```

#### Python

```
import jsonschema
jsonschema.validate(instance=data, schema=loaded_schema)
```

### "This project shows that I can design a scalable microservices ecosystem…"

* Fully event-driven.
* Strict separation (no shared models, only messages).
* Clean architecture per service.
* The game creation pipeline uses AI agents, LangChain, LangGraph, LangSmith.
* Supabase stores cross-service state securely.
* Supabase (as the communication layer) ensures loose coupling.
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
---
