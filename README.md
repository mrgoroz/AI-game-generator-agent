# 🎮 AI-Generated Trend-Based Mini Game Maker

This project demonstrates how a **.NET developer** can integrate **AI agentic workflows** to automatically generate **simple, funny video games** based on **Google Trends data**.

It is designed as a **portfolio project** showing mastery of:

* .NET backend architecture
* LangChain.NET (or Python LangChain via API)
* Agentic workflows (Chains & Graphs)
* LLM orchestration
* External API integration (Google Trends)
* Automatic asset & code generation

---

# 🚀 Project Overview

The system works like this:

1. **Google Trends Agent** fetches trending topics.
2. **Game Idea Agent** converts a trending topic into a funny, playable game design.
3. **Game Asset Agent** generates assets (sprites, text, ideas).
4. **Game Code Agent** generates game code (Unity, Godot, or web-based).
5. **Build Agent** packages the game.
6. **Deployment Agent** uploads it to GitHub Pages or itch.io.

Everything runs through a **LangGraph-style workflow** with nodes and transitions.

---

# 🧩 Tech Stack

### Backend

* **.NET 8 Web API** — main backend
* **LangChain.NET** — orchestrates LLM chains
* Optional: **Python microservice** for LangGraph logic

### AI

* OpenAI (or free LLM: **Gemma**, **Llama 3.1**, **Mistral**) via API

### Frontend (if making browser games)

* HTML5 Canvas / PhaserJS / Godot Web Export

### Storage

* GitHub repo for final games
* Local or cloud storage for generated assets

---

# 🧠 Architecture (Simple)

```
+-------------------+
| Google Trends API |
+---------+---------+
          |
          v
+-----------------------+
| Google Trends Agent   |
+-----------------------+
          |
          v
+-----------------------+
| Game Idea Agent       |
+-----------------------+
          |
          v
+-----------------------+
| Asset Generator Agent |
+-----------------------+
          |
          v
+-----------------------+
| Game Code Agent       |
+-----------------------+
          |
          v
+-----------------------+
| Build & Deploy Agent  |
+-----------------------+
```

---

# 🏗️ Microservices Overview

The project is built using a microservices architecture, where each service has a specific responsibility.

### [GoogleTrendsService](services/GoogleTrendsService)
*   **What it does**: Identifies current trending topics on Google.
*   **How it does it**: Simulates fetching data (to be replaced with real scraping/API). Publishes `TrendDetected` events via MassTransit/RabbitMQ.

### [GameIdeaService](services/GameIdeaService)
*   **What it does**: Generates unique game concepts based on trending topics.
*   **How it does it**: Consumes `TrendDetected` events. Uses Groq (LLM) to brainstorm ideas. Stores ideas in Supabase. Publishes `GameIdeaGenerated` events.

### [GameAssetService](services/GameAssetService)
*   **What it does**: Generates visual and audio assets for the game.
*   **How it does it**: (Planned) Will consume `GameIdeaGenerated` events and use AI models to create assets.

### [GameCodeService](services/GameCodeService)
*   **What it does**: Generates the actual source code for the game.
*   **How it does it**: (Planned) Will consume `GameIdeaGenerated` events and use LLMs to write code.

### [BuildService](services/BuildService)
*   **What it does**: Compiles and bundles the game assets and code.
*   **How it does it**: (Planned) Will take generated code and assets and produce a deployable build.

### [DeploymentService](services/DeploymentService)
*   **What it does**: Deploys the game to a hosting platform (e.g., GitHub Pages).
*   **How it does it**: (Planned) Will take the build artifact and publish it.

### [SharedKernel](services/SharedKernel)
*   **What it does**: Contains shared code, interfaces, and event definitions used across all microservices to ensure consistency.

---

# 📁 Project Structure

```
AI-game-generator-agent/
│
├── services/
│   ├── GoogleTrendsService/   # Fetches trends
│   ├── GameIdeaService/       # Generates game ideas
│   ├── GameAssetService/      # Generates assets
│   ├── GameCodeService/       # Generates code
│   ├── BuildService/          # Builds the game
│   ├── DeploymentService/     # Deploys the game
│   └── SharedKernel/          # Shared code & events
│
├── README.md
└── LICENSE
```

---

# 🔧 How It Works

### 1. Fetch Trending Topic

Requests Google Trends top 10.
Returns the funniest or most gameable trend.

### 2. Convert Trend → Game Design

Example:

**Trend:** "Taylor Swift Concert"

**Game Idea:** "Swiftie Dodger — avoid flying friendship bracelets!"

### 3. Generate Game Assets

* Sprite descriptions
* Background ideas
* Sound suggestions

### 4. Generate Game Code

Templates available:

* Unity C# (simple scenes)
* Godot GDScript
* PhaserJS

### 5. Build & Upload

Creates a directory in the `games/` folder.
Pushes automatically to GitHub.

---

# 🧪 Example API Workflow

```
POST /generate-game
```

Response:

```
{
  "trend": "Bitcoin price drop",
  "gameName": "Crypto Panic Run",
  "outputFolder": "games/crypto-panic-run"
}
```

---

# 🎯 Goals of This Project

✔ Show senior-level .NET AI integration
✔ Demonstrate event-driven agent workflows
✔ Demonstrate real-world LLM orchestration
✔ Showcase creativity & engineering
✔ Perfect for your GitHub portfolio

---

# 📦 Future Improvements

* Multi-agent competition for best game idea
* Automatic playable HTML preview
* Vector database of previous game ideas
* Continuous game improvement loop

---

# 🧑‍💻 Author

**Golan** — .NET Senior Software Developer<br>
This project was created as part of an AI portfolio.
