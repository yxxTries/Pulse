import os
from huggingface_hub import snapshot_download

MODEL_ID = "mistralai/Mistral-7B-Instruct-v0.3"

# Download into LLM/models/Mistral-7B-Instruct-v0.3
TARGET_DIR = os.path.join(os.path.dirname(__file__), "models", "Mistral-7B-Instruct-v0.3")

def main():
    os.makedirs(TARGET_DIR, exist_ok=True)
    # Uses your stored HF token (huggingface-cli login) or HF_TOKEN env var.
    # local_dir_use_symlinks=False ensures a real copy instead of symlinks.
    snapshot_download(
        repo_id=MODEL_ID,
        local_dir=TARGET_DIR,
        local_dir_use_symlinks=False,
        # If you prefer, you can set token via env var HF_TOKEN; otherwise, CLI login is used.
        token=os.getenv("HF_TOKEN"),
        revision="main",
    )
    print(f"Model files downloaded to: {TARGET_DIR}")


if __name__ == "__main__":
    main()
