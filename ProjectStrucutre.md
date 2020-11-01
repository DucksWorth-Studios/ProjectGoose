## Directory Structure

* 3rd-Party - For other assets
* Animations
* Audio
	* Music
	* Narrative
	* SFX
* Materials
* Models
* Plugins - Unity Engine Plugins
* Prefabs
* Resources - Assets that need to be loaded from code
* Textures
* Sandbox - For experimental scripts/scenes
	* Aaron
	* Andy
	* Cameron
	* Tomas
* Scenes
	* Levels
	* Other
* Scripts
	* Editor - Additional script plugins
	* VR
	* Management - Game manager/event scripts
* Shaders
* StreamingAssets - Required for Steam VR & other librariers


1. **Do not store any asset files in the root directory.** Use subdirectories whenever possible.
2. **Do not create any additional directories in the root directory,** unless you really need to.
3. **Be consistent with naming.** If you decide to use camel case for directory names and low letters for assets, stick to that convention.
4. **Don’t try to move context-specific assets to the general directories.** For instance, if there are materials generated from the model, don’t move them to Materials directory because later you won’t know where these come from.
5. Use 3rd-Party to store assets imported from the Asset Store. They usually have their own structure that shouldn’t be altered.
6. Use Sandbox directory for any experiments you’re not entirely sure about. While working on this kind of things, the last thing that you want to care about is a proper organization. Do what you want, then remove it or organize when you’re certain that you want to include it in your project. When you’re working on a project with other people, create your personal Sandbox subdirectory like: Sandbox/JohnyC.


## Scene Hierarchy Strucutre

* Management - Empty Objects that contain scripts
* GUI
* Cameras
* Lights
* World
	* Terrain
	* Props
	* VR - Teleportation Plane & Teleportation Points
* Player
* _Dynamic


1. All empty objects should be located at 0,0,0 with default rotation and scale.
2. When you’re instantiating an object in runtime, make sure to put it in _Dynamic – do not pollute the root of your hierarchy or you will find it difficult to navigate through it.
3. For empty objects that are only containers for scripts, use “@” as prefix – e.g. @Cheats

## Resources
* https://blog.theknightsofunity.com/7-ways-keep-unity-project-organized/
* https://blog.theknightsofunity.com/unity-resources-folder-how-to-use-it/
* https://forum.unity.com/threads/best-practices-folder-structure.65381/
* http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/
