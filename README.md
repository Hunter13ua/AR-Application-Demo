# AR Model Viewer

An Augmented Reality application for Unity that allows users to load, place, and interact with 3D GLB models in real-time AR environments.

## Features

- __Model Loading__: Download and load GLB models from URLs with validation

- __AR Placement__: Place models on detected planes in the real world

- __Interactive Manipulation__:

  - Select models by tapping
  - Rotate with single-finger drag
  - Scale with pinch gestures

- __UI State Management__: Guided user experience through loading, placement, and interaction phases

- __Cross-Platform__: Built for Android with AR Foundation

## Requirements

- __Unity__: 2021.3 or later

- __Packages__:

  - AR Foundation 4.2+
  - ARCore XR Plugin (for Android)
  - gltfast 5.0+
  - XR Interaction Toolkit 2.3+
  - TextMeshPro
  - Universal Render Pipeline (URP)

- __Android Device__: With ARCore support (Android 7.0+, ARCore 1.9+)

## Setup

1. __Clone the repository__:

   ```bash
   git clone https://github.com/yourusername/ar-model-viewer.git
   cd ar-model-viewer
   ```

2. __Open in Unity__:

   - Launch Unity Hub
   - Add project from the cloned directory
   - Ensure URP is set as the active render pipeline

3. __Install Dependencies__:

   - Open Package Manager (Window → Package Manager)
   - Install required packages listed above
   - Ensure ARCore XR Plugin is installed for Android builds

4. __Configure Project__:

   - Go to Project Settings → XR Plug-in Management
   - Enable ARCore for Android
   - Set Active Input Handling to "Input System Package"

5. __Scene Setup__:

   - Open `Assets/Scenes/TestTaskScene.unity`
   - Ensure all AR components are properly assigned in the ServiceLocator GameObject

## Building

1. __Switch Platform__:

   - File → Build Settings
   - Select Android platform
   - Click "Switch Platform"

2. __Configure Build__:

   - Set Minimum API Level to 24 (Android 7.0)
   - Enable "ARCore" in XR Plug-in Management
   - Add TestTaskScene to Scenes in Build

3. __Build APK__:

   - Build Settings → Build
   - Choose output location and filename
   - Wait for build completion

## Running

1. __Install APK__ on Android device with ARCore support
2. __Grant camera permissions__ when prompted
3. __Point camera at flat surfaces__ to detect planes
4. __Enter GLB URL__ (must end with .glb)
5. __Tap "Submit"__ to load model
6. __Tap screen__ to place model when planes are detected
7. __Interact__: Tap to select (yellow highlight), drag to rotate, pinch to scale

## Architecture

- __UIStateManager__: Manages application states (Initial, AR)
- __ServiceLocator__: Provides access to core services (AR components, ModelLoader)
- __ModelLoader__: Handles GLB downloading and instantiation using gltfast
- __ARPlacementManager__: Manages model placement and selection
- __ModelInteraction__: Handles touch gestures for selected models
- __Panel System__: Modular UI panels with state-based visibility

## Troubleshooting

- __Pink models__: Ensure URP shaders are included in build (Project Settings → Graphics → Always Included Shaders)
- __No AR planes__: Ensure good lighting and move camera slowly over surfaces
- __Touch not working__: Check Input System configuration and permissions
- __Model loading fails__: Verify URL is accessible and ends with .glb

## License

This project is for educational purposes as part of a test task.
