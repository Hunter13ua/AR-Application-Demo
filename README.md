# AR Model Viewer

A simple AR application that loads GLB models from URLs and allows placement and interaction in augmented reality.

## Requirements

- Unity 6000.0.43f1
- Android device with ARCore support
- Packages: AR Foundation, gltfast, XR Interaction Toolkit

## Setup

1. Clone repository
2. Open in Unity
3. Install required packages via Package Manager
4. Enable ARCore in XR Plug-in Management
5. Set Active Input Handling to "Input System Package"

## Building

1. Switch to Android platform in Build Settings
2. Add TestTaskScene to build scenes
3. Build APK

## Running

1. Install APK on Android device
2. Grant camera permissions
3. Enter GLB URL and tap Submit
4. Scan surfaces, then tap to place model
5. Interact: Tap to select, drag to rotate, pinch to scale

## Features

- URL-based GLB model loading with validation
- AR placement on detected planes
- Touch-based model interaction (select, rotate, scale)
- State-based UI management

---

*Test task submission: Unity AR project with build/run instructions*
