#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$SCRIPT_DIR"
PROJECT_VERSION_FILE="$PROJECT_ROOT/ProjectSettings/ProjectVersion.txt"

TARGET="all"
UNITY_PATH="${UNITY_PATH:-}"
OUTPUT_ROOT="$PROJECT_ROOT/Builds"
ANDROID_FORMAT="apk"
DEVELOPMENT_BUILD="false"
SERVER_SELF_CONTAINED="true"
ZIP_DESKTOP_BUILDS="true"
LOG_PATH="$PROJECT_ROOT/Builds/unity-build.log"

usage() {
  cat <<'EOF'
Usage: ./build.sh [options]

Options:
  --target <all|windows|linux|android|windows,linux,android>
  --unity <path-to-unity>
  --output-root <dir>
  --android-format <apk|aab>
  --development
  --framework-dependent
  --no-zip
  --log <path>
  -h, --help

Android signing can be provided via environment variables:
  PCRCALC_ANDROID_KEYSTORE_NAME
  PCRCALC_ANDROID_KEYSTORE_PASS
  PCRCALC_ANDROID_KEYALIAS_NAME
  PCRCALC_ANDROID_KEYALIAS_PASS
EOF
}

read_project_version() {
  awk -F': ' '/m_EditorVersion:/ { print $2; exit }' "$PROJECT_VERSION_FILE"
}

detect_unity_path() {
  local version
  version="$(read_project_version)"
  local candidates=(
    "/Applications/Unity/Hub/Editor/$version/Unity.app/Contents/MacOS/Unity"
    "/opt/Unity/Hub/Editor/$version/Editor/Unity"
    "$HOME/Unity/Hub/Editor/$version/Editor/Unity"
  )

  for candidate in "${candidates[@]}"; do
    if [[ -x "$candidate" ]]; then
      printf '%s\n' "$candidate"
      return 0
    fi
  done

  return 1
}

while [[ $# -gt 0 ]]; do
  case "$1" in
    --target)
      TARGET="$2"
      shift 2
      ;;
    --unity)
      UNITY_PATH="$2"
      shift 2
      ;;
    --output-root)
      OUTPUT_ROOT="$2"
      shift 2
      ;;
    --android-format)
      ANDROID_FORMAT="$2"
      shift 2
      ;;
    --development)
      DEVELOPMENT_BUILD="true"
      shift
      ;;
    --framework-dependent)
      SERVER_SELF_CONTAINED="false"
      shift
      ;;
    --no-zip)
      ZIP_DESKTOP_BUILDS="false"
      shift
      ;;
    --log)
      LOG_PATH="$2"
      shift 2
      ;;
    -h|--help)
      usage
      exit 0
      ;;
    *)
      printf 'Unknown argument: %s\n' "$1" >&2
      usage >&2
      exit 1
      ;;
  esac
done

if [[ -z "$UNITY_PATH" ]]; then
  UNITY_PATH="$(detect_unity_path || true)"
fi

if [[ -z "$UNITY_PATH" ]]; then
  printf 'Unity executable not found. Pass --unity or set UNITY_PATH.\n' >&2
  exit 1
fi

mkdir -p "$(dirname "$LOG_PATH")" "$OUTPUT_ROOT"

export PCRCALC_BUILD_TARGET="$TARGET"
export PCRCALC_OUTPUT_ROOT="$OUTPUT_ROOT"
export PCRCALC_ANDROID_FORMAT="$ANDROID_FORMAT"
export PCRCALC_DEVELOPMENT_BUILD="$DEVELOPMENT_BUILD"
export PCRCALC_SERVER_SELF_CONTAINED="$SERVER_SELF_CONTAINED"
export PCRCALC_ZIP_DESKTOP_BUILDS="$ZIP_DESKTOP_BUILDS"

"$UNITY_PATH" \
  -batchmode \
  -nographics \
  -quit \
  -projectPath "$PROJECT_ROOT" \
  -logFile "$LOG_PATH" \
  -executeMethod BuildScript.BuildFromCommandLine
