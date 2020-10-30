# VR Best Practices

## TL;DR
* If it looks interactable, it should be interactable
* Optimise and test the game from the beginning
* Aim for a minimum of 90 FPS and a maxium 20 milliseconds delay between input and reaction
	* Optimise based on the worst case not just the average case
* VR != Flat Screen so don't try to apply the same design techniques
* Alwasy keep the players comfort in mind. If you have an intense scene, follow it up with a relaxing one
* Have a gentle difficulty curve and remember to pace things out
* Use detail and lighting to guide the player down a specifc path
* Never take away the player's control
* Offer the player as many locomotion and comfort options as possible
* Scale everything to how you would expect to see it in real life
* Use positional audio to keep players immersed
* Aim to support as many headsets as possible
* Test your game as often as possible


"Throw everything you know about 2D games out of the window. Just throw it all out the window and start from scratch. What can you do? What can you build revolving around being able to to pick things up or move my hands and look around? Just start from there." -Nick Witsel, Vertigo Games (Arizona Sunshine)


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

## GamesIndustry.biz Academy

* Key development concepts in VR revolve around locomotion, comfort, interaction and optimisation. 
* "It's difficult to determine what is a should and a shouldn't in VR. The only rule is that there isn't any yet" -Nick Witsel, Vertigo Games (Arizona Sunshine)

### Be aware of the limitations
* "The blueprints and assumptions you have based on traditional game design do not work in the same way when it comes to VR, so iterating quickly in the engine, failing fast, and learning from what works is absolutely critical." -Steve Watt, nDream
* "Flat screen can learn from VR, move away from stuff that is too much film and too little game" -Erik Odeldahl, Fast Travel (Apex Construct)
* Players will take things much slower in VR than they do in a flat screen game.
* "People slow down so much [in Half-Life: Alyx], that's in contrast to how fast your character moves in Half-Life games traditionally. You're very, very, very fast in those games, and at the furthest end of the bell curve on the other extreme is how slow people go [in VR]." -Sean Vanaman, Valve (HL:A)

### If something looks interactive, it has to be interactive
* "If anything looks interactable, it has to be interactable. The player has to be able to touch it, lift it, throw it or press it, whatever the interaction may be. I think that's the most important [rule] of all." -Erik Odeldahl, Fast Travel (Apex Construct)
	* "A super detailed world is not going to save a world that [is not interactive]. Nail the interactions and let the rest follow."
* Take into account that people are not necessarily going to interact with the environment in the way you expect.
* "Players would lift up the box, they would look under the table, stick their heads in the vents. We watched player after player do this, and it gave us an opportunity to spend resources effectively on putting things under the box, and in the vent. It offered us the excuse to reward that type of exploration." -Corey Peters, Valve

### Always keep comfort in mind
* "It's a lot easier to overwhelm players in VR, [it's] inherently a physical experience" -Danny Bulla, Polyarc (Moss PSVR)
* The challenge is balancing immersion, freedom and comfort, as once players put the headset on, they really believe they're in this environment.
* "You're going through all the same emotions that you would if you were there. If you have a situation where there's a headcrab in that room, it's much scarier than when it's on your monitor. We really had to be cognisant of pacing those types of experiences out. There are parts of the game that will be intense, but then we try to have a moment afterwards where you can breath, take your time a little bit, explore -- just gather yourself back up." -Corey Peters, Valve

### Be gentle with your difficulty curve and pacing
* VR is still a new medium for a lot of players, so it's important you take the time to build up the difficulty, and give enough time for the player to familiarise themselves with the environment and the controls before throwing more complicated things at them. 
* "It's one thing to make a regular shooter and assume that the player knows how to move around and fire their weapon, but in VR it's such a physical experience that you have new skills to learn." -Corey Peters, Valve

### Work around having no camera control
* In VR, the player has complete control over the camera view at all times, which means that if you want them to follow a specific route, you'll have to guide them all the way.
* "Detail and lighting can play a big part in telling people where to go." -Steve Watt, nDream
* "If you need the player to look at something, make sure it is loud and probably blinks" -Erik Odeldahl, Fast Travel (Apex Construct)

### Focus on player movement
* The way you're going to allow the player to move is crucial to a good VR experience, especially in a game where the player embodies the main character
* "A big topic when it comes to VR and accessibility is the concept of VR legs. When I play a VR game, I'm standing still, my legs are standing still. So if I see my VR legs move around, there's this disconnect. For some people that's fine. Personally I don't like seeing my legs in VR because they never match with what my real legs are feeling so I'd rather not see them at all." -Nick Witsel, Vertigo Games (Arizona Sunshine)
* "What if you're playing a game where you've got this huge axe and it's really heavy. Well it doesn't weigh anything [in VR], because I can just move my hands around. You have games, like Saints and Sinners, where it actually slows your hand movement. So while I'm doing this in real life [he moves fast] my character does this [he moves slowly]. For some people that's absolutely fine. For other people... this is actually taking [them] out of the experience." -Nick Witsel, Vertigo Games (Arizona Sunshine)

### Choose your locomotion mechanics carefully
* For some people, smooth locomotion can be very nauseating, as your eyes see a movement that your body isn't physically following.
* "As every person is slightly different in their sensitivity to motion in VR, exposing as many options and adjustments to the user as possible is a good way to increase accessibility" -Grant Bolton, nDreams (Shooty Fruity)
* Limiting the field of view can be a good trick for avoiding motion sickness when using smooth locomotion -- developers call it 'tunnel vision', 'vignettes' or 'blinders', and Ubisoft's Eagle Flight was one of the first to use it.
* The other locomotion option in a first-person VR game is teleportation -- point at where you want to go, press a button, and you're transported there.
* "... we implemented a smooth locomotion control scheme just before launch, because lots of people really loudly declared online that they wanted it. And it turns out more than half of our players actually use smooth locomotion and very much prefer that. Lots of players don't want to play teleportation-driven games" -Erik Odeldahl, Fast Travel (Apex Construct)
* "Identifying the type of experience that you're trying to create early on is very important to focus on those things too." -Danny Bulla, Polyarc (Moss PSVR)

### Be consistent with level metrics
* In flat screen development, studios can cheat a lot with scale, making things look a lot bigger than they actually are. This is impossible in VR, so you need to build everything to scale and be consistent with it.
* You perceive scale a lot more accurately in VR
* "Anticipation plays a big factor into comfort. "So create consistency in your metrics: what is the most comfortable angle to jump at? What's the most comfortable size of a door for the character to move through? All these things are standard in traditional game design and level design, but it becomes so much more important in virtual reality as you're immersed, and so your brain can recognise things that are a little off." -Danny Bulla, Polyarc (Moss PSVR)

### Use positional audio to keep players immersed
* Having every sound coming from an actual source is crucial in VR, to improve the player's immersion.
* "[Positional audio] is not just a mechanic to get people to look or move in the right direction. It's equally important as your visual style and your modes of locomotion and interactions, just to get players to believe in the world they're in." -Erik Odeldahl, Fast Travel (Apex Construct)

### Think multiplatform from day one
* Because the VR market is still fairly small, you should build your game with multiple platforms in mind from the start.
* "We target all platforms as long as they have motion controllers. You have to build something that scales" -Erik Odeldahl, Fast Travel (Apex Construct)

### Always think about your performance targets
* "The main difference in hitting VR performance targets is that we have to render to a high-resolution pair of screens at a very high framerate -- somewhere between 60 and 144 frames per second depending on the platform. We also have to provide a consistent framerate without the kind of minor hitches that might be acceptable in a non-VR game -- this means we always have to optimise for the worst case rather than the average case." -Grant Bolton, nDreams (Shooty Fruity)
* A VR game not hitting performance targets will at best be less enjoyable, at worst unplayable and nauseating.
* "We've noticed that if you playtest a VR game below 45 frames per second, or even below 90, your enjoyment just goes down. People don't exactly know why [but] they're like: 'This isn't it.' And when you have them play something that's at a stable frame rate, they are like: 'Oh this is so much better, what's different?' It's literally only the frame rate." -Nick Witsel, Vertigo Games (Arizona Sunshine)

### Don't underestimate testing
* "The main differences are that testing your work is more involved as you need to put on your headset and pick up controllers. It's hard to automate testing as the inputs are so precise and analogue. Additionally, reviewing software as a group is challenging! The social screen -- where we mirror the VR view on a TV -- helps, but it's hard for an observer to get the same feeling from the software as the person playing." -Grant Bolton, nDreams (Shooty Fruity)
* Even the simplest tasks take different dimensions in VR, so the best thing is just to approach it like a blank canvas.
* "Throw everything you know about 2D games out of the window. Just throw it all out the window and start from scratch. What can you do? What can you build revolving around being able to to pick things up or move my hands and look around? Just start from there."-Nick Witsel, Vertigo Games (Arizona Sunshine)



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
- https://www.gamesindustry.biz/articles/2020-04-01-the-best-practices-and-design-principles-of-vr-development
- https://www.youtube.com/watch?v=DrPa0dJT6dQ&feature=youtu.be&t=1633

## Additional Resources
- https://developer.oculus.com/design/latest/concepts/book-bp/
- https://developers.google.com/vr/develop/best-practices/perf-best-practices
- https://docs.unrealengine.com/en-US/Platforms/VR/DevelopVR/ContentSetup/index.html
