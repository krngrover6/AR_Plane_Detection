# AR Plane Detection – Unity 6 (iOS & Android)

Small augmented-reality demo built with **Unity 6** and **AR Foundation** that places animated chickens on detected real-world surfaces.

---

## Features

- AR plane detection using **AR Foundation 6** (**ARKit / ARCore**)
- Preview chicken follows the detected plane under the **screen center**
- Tap to place a chicken on the current plane
- UI button to spawn multiple chickens sequentially (**preview → tap → place loop**)

---

## Tech Stack

- **Unity 6** (Default Render Pipeline, Mobile Renderer)
- **AR Foundation 6**
- **ARKit** (iOS)
- **ARCore** (Android)
- **New Input System** (touch + mouse for editor testing)

---

## Project Structure

### `Scripts/Input/InputHandler.cs`
- Wraps the `UserInput` input-actions asset  
- Raises a static `OnTap` event whenever the user taps the screen  

### `Scripts/ExperienceManager.cs`
- Subscribes to `InputHandler.OnTap`
- Uses `ARRaycastManager` to raycast from the screen center onto detected planes
- Manages the chicken preview, placement logic, and the **Add Chicken** UI button state

### `Scripts/Chicken.cs`
- Handles parenting to the detected `ARTrackable`
- Enables the chicken animation when spawned

---

## How It Works

### Plane detection
- `ARPlaneManager` tracks planes in the environment
- `ARRaycastManager` raycasts from the screen center each frame to get position + rotation on the closest plane hit

### Preview & placement loop
- When `canAddChicken` is `true`, the preview chicken is visible and follows the raycast hit
- On tap, `ExperienceManager` instantiates a real chicken at the detected pose and hides the preview
- The **Add Chicken** button becomes visible; pressing it re-enables preview mode for the next chicken

---

## Setup & Build

### 1) Install packages (Package Manager)
Install these packages:

- **AR Foundation**
- **ARKit XR Plugin** (for iOS)
- **ARCore XR Plugin** (for Android)
- **Input System (New)**

---

### 2) Scene configuration

- Delete the default camera and add an **XR Origin (AR)**
- Add these components to the XR Origin:
  - `AR Session`
  - `ARPlaneManager`
  - `ARRaycastManager`
- Add an **InputHandler GameObject** with `InputHandler.cs` attached
- Add the **Canvas prefab** and wire:
  - `AddChickenButton` → `ExperienceManager.SetCanAddChicken(true)`

---

### 3) Platform settings (Player Settings → XR / Other Settings)

  **iOS**
- Enable **ARKit support**

  **Android**
- Enable **ARCore**
- Set Minimum API Level to **Android 7.0 (API 24)** or higher

---

### 4) Build & Run
Build & run on a real device.

  Tested on **iPhone 15**  
  Android is supported by AR Foundation as well

---

## Credits

- Chicken 3D model: **"Chicken – rigged"** by **Maf’j Alvarez**  
  Licensed under **Creative Commons Attribution 4.0**
