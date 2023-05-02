![Entitled Engine Logo](http://jiri.dscloud.me/Non-Active-Websites/GIT_README/EntitledEngine/EE.png) <br>

# Features
| Features | Entitled Engine |
| --- | --- |
| Pixel Renderer | ❌ |
| Entity Component System | ❌ |
| Physics | ✅ |
| Custom Sprites | ✅ |
| Sprite Slicer | ❌ |
| UI | ❌ |
| Rotations | ❌ |
| Pixel to Units| ✅ |
| Custom Logs | ✅ |
| Exit Codes | ✅ |

# Why did I make a game engine?
I wanted to know how game engines worked form the inside, and what knowledge you need for making an engine.<br>
As I've expierienced IT IS HARD to make a good workflow of the code in the engine itself, as in how to save objects for rendering, how to render them ect.<br>
But after some time things got flowing and the workflow of the engine came to mind with making list of each individual sprite type, in the end this wasn't the smartest decision I've made yet.<br>

<hr>

# Development Information

## Custom Logs
These are just my way of debugging things with using fancy colours and special error messages to display them highlighted or not, just a cooler and cleaner way of importants and information.

## Exit Codes
When you exit the program and it closed wrong then you will get a error message in the console or the next time you start up the program it will tell you in the console of the application.<br>
This way you can search up what is wrong and what went wrong and how to prevent this from happening another time.


## Units
the pixel to units system works just by multiplieing the floats given to the object and when rendering it multiplies the floats by a specific amount the person made in the setings or he had set it to default t 20pixel/unit.<br>


## Physics
I didn't understand anything from physics except from simple velocity and gravity wit hsome AABB(AxisAlignedBoundingBox).<br>
After throwing in for loops to check each object velocity and colision it applys gravity if it hasn't hit something, then it wil just stop applying gravity but there are some flaws because it isn't sorted so every rigidbody is also updated if you check the collision of it.<br>
this makes the physics broken and really slow with a low of bugs inside of it I have tried to fix this it took me 2months to try and fix it but in the end I gave up on trying to improve / optimize my old one.<br>
this was also the jumpthrough to start working on Entitled Engine 2 with improved collisions and als circle colliders, rotations, sounds sytem ect, A LOT mopre then this.
