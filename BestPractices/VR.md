# VR Best Practices

## Unity VR Best Practices

### 1. Introduction to VR Best Practice
* Test game regulraly and ensure it is reaching the target FPS to create a nausea-free expericence
* Optimise early and often

### 2. Rendering in VR
* Optimise the rendering workflow as much as possible as rendering is the key bottleneck in VR
* Good technicues to use to optimise rendering are:
	- [Batching](https://docs.unity3d.com/Manual/DrawCallBatching.html): Batching uses shared Materials to reduce work on the GPU by minimising the cost of context switching 
	for each draw cell (i.e. chaninge shaders, textures)
	
  - [GPU Instancing](https://docs.unity3d.com/Manual/GPUInstancing.html): Useful for drawing multiple copies of the same mesh (i.e. beakers and test tubes)
  
  - [Lighting Strategy](https://unity3d.com/learn/tutorials/projects/creating-believable-visuals/lighting-strategy): **DON'T USE REALTIME LIGHTING OR REALTIME GLOABL ILLUMINATION.**
  Use non-directional lightmaps for static objects and light probes for dynamic objects
  
  - Cameras
    - Orientation and position should always respond to the player's movement (as long as the headset supports 6 degrees of movement)
    - Avoid using camera effects like camera bobing, zooming or shaking as this will cause motion sickness
    - Don't overide field of view (Unity obtains stero projection matrices  directly from the used VR SDK)
    - Don't use depth of field or motion blur
    - Avoid moving or rotating the horizon line or other large components of the environemnt
    - Set the near clip plane to the minimal acceptable value for correct rendering of objects. Set your far clip plane to a value that optimizes frustum culling.
    - When using Canvas, favor World Space render mode over Screen Space render modes, as it very difficult for a user to focus on Screen Space UI.
  
  - [Post-Processing](https://github.com/Unity-Technologies/PostProcessing): Avoid using were possible. Most post-processing effects are very expensive as they have to be rendered twice and 
  most effects like motion blur should never be used in VR
  
  - Anti-aliasing: Use anti-aliasing as often as possible as it helps to smooth the image, reduce jagged edges, and minimize specular aliasing.
    - Forward Rendering supplies [MSAA](https://en.wikipedia.org/wiki/Multisample_anti-aliasing) which can be enabled in the [Quality Settings](http://docs.unity3d.com/Manual/class-QualitySettings.html).
    - When using Deferred Rendering, consider using an anti-aliasing post-processing effect. [Unity’s Post-Processing Stack](https://github.com/Unity-Technologies/PostProcessing) offers the following:
      - [FXAA](https://en.wikipedia.org/wiki/Fast_approximate_anti-aliasing) - This is the cheapest solution for anti-aliasing and is recommended for low-end PC.
      - [TAA](https://en.wikipedia.org/wiki/Temporal_anti-aliasing) - This state-of-the-art technique will give better results than the other techniques at a higher GPU cost. Recommended on high-end PCs.
  
  - [Shaders](https://docs.unity3d.com/Manual/SL-ShaderPerformance.html): Optimising shaders is key to high FPS in a VR project
    - Graphics drivers do not actually prepare shaders until they are first needed
    - It is recommended to use a [Shader Variant Collection](https://docs.unity3d.com/ScriptReference/ShaderVariantCollection.html) and call its WarmUp function at 
    an opportune time (e.g., while displaying a loading screen for example) in order to prepare essential shaders and avoid frame rate drops when using a shader 
    for the first time.
    
  - Asynchronous Reprojection:  Whenever an application drops a frame, asynchronous reprojection will kick in, rendering a new frame by applying the 
  user’s latest orientation to the most recent rendered frame.
    - Developers should never rely on this technique in place of optimization as this technique does cause positional and animation judder.

### 3. Motion-to-Photon Latency
- Minimizing motion-to-photon latency is the key to giving the user the impression of presence
- It is recommended to target 20 milliseconds or less motion-to-photon latency for any input made via VR hardware to be reflected on the HMD’s screen. This includes the handling of HMD rotation and position as well as VR controller rotation and position.
- Avoid handling input in FixedUpdate as this callback does not necessarily get called once per frame and may incur increased input latency.
- Tracked poses may potentially be handled twice, once in Update() and again in onBeforeRender(). Any additional handling that occurs in onBeforeRender() should be very lightweight or can result in serious input latency.

### 4. Platform Specific Recommendations
- [RenderDoc](https://renderdoc.org/): RenderDoc is a stand-alone graphics debugging tool that allows you to do frame captures of your application running on PC or on Android.
- [SteamVR Frame Timings](https://developer.valvesoftware.com/wiki/SteamVR/Frame_Timing): This view shows CPU and GPU timings associated with SteamVR and is a great tool to monitor which frames are heavier to render, as well as the implications of reprojection in your VR project.
- [fpsVR](https://store.steampowered.com/app/908520/fpsVR/): fpsVR is a utility application for SteamVR that show VR session's performance counters in SteamVR Overlay window inside VR

## General Notes on creating a good VR Experience

* Make the player the key part in the story, not just an observer
* Make the world as physcially interactable as possible, stuff to pickup & examine
* Let the player drive the experience. Let them experience the game at their own pace
* Enagage as many senses as possible, good use of haptic feedback from controller and good use of audio
* Vary the tone of the game - if you want a player to be blown away by a big space, have them walk through a small corridor first
* Fulfill a fantasy
* Make the game memorable

## Resources
- https://learn.unity.com/tutorial/vr-best-practice
- https://www.youtube.com/watch?v=DrPa0dJT6dQ&feature=youtu.be&t=1633
