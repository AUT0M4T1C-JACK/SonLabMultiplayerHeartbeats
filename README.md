# SonLab Multiplayer Heartbeats

A Unity-based MR multiplayer application featuring heartbeat haptic feedback and synchronization across networked players in a Price Is Right-style game scenario.

## Getting Started

1. **Open the Project**: Load this project in Unity 2022.3.20f1
2. **Configure Photon**: Update your Photon credentials in the Photon settings
3. **Run the Application**: Start with the **PriceIsRightScene** in the Assets/Scenes directory

## Scene Setup & Script Configuration

### NetworkController
**What it does**: Manages Photon PUN multiplayer networking and receives remote control events

**Scene Setup**:
- Create an empty GameObject named `NetworkController`
- Attach the `NetworkController` script
- In the Inspector, assign:
  - **HeartBeatController** - Reference to the HeartBeatController object
  - **PriceManager** - Reference to the PriceManager object
  - **SceneLoader** - Reference to the SceneLoader object
- Automatically connects to Photon on startup and joins a shared room (max 4 players)

**Events it receives from network** (via Utility event codes):
- Start/Stop Heartbeat (event codes 29-30)
- Reveal individual prices 1-5 (event codes 31-35)
- Hide all prices (event code 36)
- Load Scene 1/2/3 (event codes 37-39)

### HeartBeatController
**What it does**: Sends haptic feedback pulses to both VR controllers and plays heartbeat audio in sync

**Scene Setup**:
- Create an empty GameObject named `HeartBeatController`
- Attach the `HeartBeatController` script
- In the Inspector, assign/configure:
  - **Timestamps File** (TextAsset) - A text file containing heartbeat timing data (one timestamp per line, in seconds)
  - **First Pulse Duration** - Duration of first haptic pulse (default: 0.1s)
  - **First Pulse Intensity** - Strength of first pulse (default: 1.0, range 0-1)
  - **Pulse Delay** - Wait time between first and second pulse (default: 0.1s)
  - **Second Pulse Duration** - Duration of second haptic pulse (default: 0.08s)
  - **Second Pulse Intensity** - Strength of second pulse (default: 0.6)
  - **Heartbeat Audio Source** - AudioSource component for audio playback
  - **First Heartbeat Sound** - Audio clip for systole (first beat sound)
  - **Second Heartbeat Sound** - Audio clip for diastole (second beat sound)
  - **Heartbeat Volume** - Volume level for audio (0-1, default: 1.0)

**How it works**: When triggered by NetworkController, plays haptic pulses and audio to both controllers at timestamps specified in the text file, creating a synchronized heartbeat sensation.

### PriceManager
**What it does**: Controls the visibility of 5 price display objects

**Scene Setup**:
- Create an empty GameObject named `PriceManager`
- Attach the `PriceManager` script
- In the Inspector, assign 5 GameObjects to the `Price Objects` array:
  - **Price 1-5**: Assign the 5 UI/Canvas objects that display prices
- All prices are hidden by default at startup

**Control**: NetworkController sends events to reveal individual prices (1-5) or hide all at once

### SceneLoader
**What it does**: Manages scene transitions for different game scenarios

**Scene Setup**:
- Create an empty GameObject named `SceneLoader`
- Attach the `SceneLoader` script
- In the Inspector, configure **Scene Names** array:
  - **Index 0 (Scene 1)**: `Game2Scene`
  - **Index 1 (Scene 2)**: `PriceIsRightScene`
  - **Index 2 (Scene 3)**: `DemoScene`
- Update these scene names to match your actual scene names in Build Settings

**How it works**: NetworkController sends event codes 37-39 to trigger loading of scenes at indices 0-2

### SpawnAnchorFromRaySelect
**What it does**: Spawns AR anchored objects where the player points with a ray interactor

**Scene Setup**:
- Attach this script to a GameObject with an XRRayInteractor component
- In the Inspector, assign:
  - **Prefab** - The GameObject to spawn at ray intersection points
  - **Ray Interactor** - Reference to the XRRayInteractor component
  - **Anchor Manager** - Reference to the ARAnchorManager for AR tracking
- When the ray hits an object and fires a select event, the prefab is spawned and anchored at that location

## Features

- **Multiplayer Networking**: Real-time event synchronization using Photon PUN (max 4 players per room)
- **Haptic Feedback**: Dual-pulse heartbeat haptic effects sent to both VR controllers
- **Synchronized Audio**: Heartbeat audio plays alongside haptic feedback
- **Price Reveal Game**: Controlled price reveal mechanics for game scenarios
- **Scene Management**: Dynamic scene loading for different game modes
