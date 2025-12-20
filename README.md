# Pulse

A work-in-progress Windows overlay assistant built around an **ASR -> LLM** pipeline.

The end goal: press a hotkey, speak naturally, and get a lightweight on-screen response (and eventually actions) without tabbing away from what you are doing.

## What is in this repo today

- `OverlayDemo/`: a WPF overlay prototype (transparent, topmost) with a global hotkey to show/hide.
- `LLM/`: Python scripts to download a local model from Hugging Face and run a small Transformers generation test.

## Current status

- Overlay UI: prototype works (toggle with `Ctrl+Shift+Space`).
- ASR: not implemented yet.
- LLM: download + local inference script exists; full assistant logic/integration is next.

## Repo layout

- `Pulse.sln`: Visual Studio solution
- `OverlayDemo/`: WPF overlay app
- `LLM/`: model download + experimentation scripts
  - `LLM/download_model.py`
  - `LLM/main.py`
  - `LLM/models/` (gitignored)

## Quickstart

### 1) Run the overlay prototype (Windows)

Prereqs:
- Windows 10/11
- .NET SDK that supports `net10.0-windows` (this project currently targets .NET 10 preview)

Run:
- From the repo root: `dotnet run --project OverlayDemo\OverlayDemo.csproj`
- Or: `OverlayDemo\run.bat`

Usage:
- Toggle overlay: `Ctrl+Shift+Space`

### 2) Download a local LLM (Python)

Prereqs:
- Python 3.10+
- A Hugging Face account/token (for gated/rate-limited downloads)

Install deps (example):
- `pip install huggingface_hub transformers accelerate torch`

Authenticate (pick one):
- `huggingface-cli login`
- Or set an env var: `HF_TOKEN=...`

Download the model:
- `python LLM\download_model.py`

Notes:
- Models download into `LLM/models/` (ignored by git).
- `LLM/main.py` may need its `model_id` path adjusted to match the download folder (the downloader writes to `LLM/models/Mistral-7B-Instruct-v0.3`).

## Intended pipeline (roadmap)

1. Hotkey / push-to-talk in the overlay
2. ASR (speech -> text) with streaming partials
3. Prompt builder + conversation state
4. LLM (local-first) to produce answers and/or tool calls
5. Overlay response UI (and later: actions)

## Roadmap

- ASR capture and transcription (likely Whisper/faster-whisper)
- LLM integration as a service (process/server) the overlay can call
- Streaming responses into the overlay UI
- Tool calling + small set of safe local tools (clipboard, notes, etc.)
- Configuration (hotkeys, model choice, latency/quality tradeoffs)

## Privacy

The goal is local-first: speech and text stay on your machine. The only network access required is optional model downloads from Hugging Face.

## Contributing

PRs/issues are welcome, especially around:
- low-latency ASR on Windows
- WPF overlay UX (positioning, focus behavior, animations)
- bridging the overlay <-> Python inference cleanly
