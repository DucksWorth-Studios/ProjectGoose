# Unity Best Practices

## Programming
* Try to separate and organise the various responsibilities and actions 
  - the various entities within your game don't have to concern themselves with the inner workings of any other entity.
* Apply the ideas of [Separation of Concerns](http://en.wikipedia.org/wiki/Separation_of_concerns) and [Encapsulation](http://en.wikipedia.org/wiki/Information_hiding) where possible
* Maintain "Separation of concerns". Avoid duping and bloating component's code. 
  - If some big code chunk has nothing to do with component's name, or could be reused by multiple components - you should probably move it to a separate script.
* TBC

## Lighting
* Enable **Auto Generate** Lighting only with single Scenes
* Don't mix scenes that have **Auto Generate** enabled and disabled
* Ensure that alL Scenes that are used together have identical Ligthing Settings, ideally using Global Settings
* You should enable **Auto Generate** only when you are quickly iterating on the lighting while working with a single Scene. 
  - In all other cases, you should disable **Auto Generate** and generate the lighting data manually.
* When you use **Auto Generate**, Unity does not create a Lighting Data Asset in the Project. Instead, Unity stores the GI data and other lighting data for that Scene in memory.

## Resources
* https://answers.unity.com/questions/15713/what-are-some-good-scripting-habits.html
* https://trello.com/b/Z6cDRyis/good-coding-practices-in-unity-unofficial

* https://learn.unity.com/tutorial/lighting-best-practices

## Additional Resources
* http://en.wikipedia.org/wiki/Separation_of_concerns
* http://en.wikipedia.org/wiki/Information_hiding
* https://www.jacksondunstan.com/
* https://unity.com/how-to/best-practices-performance-optimization-unity
