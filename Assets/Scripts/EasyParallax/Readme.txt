Easy Parallax

This package allows you to easily add a parallax effect to your 2d game. It is most suitable for infinite runners, 
where your backgrounds move side to side infinitely.
Not a single line of code is needed to implement this asset!

How to implement:

1. Add the SpriteMovement component to all of your sprites that will move
2. Optionally add the SpriteDuplicator component to allow seamless sprite duplication using a pool, so that you never run out of objects
3. Create different MovementSpeedType Scriptable objects for your sprites. E.g. BackgroundFar, BackgroundClose, Foreground, etc.
4. Apply the MovementSpeedType to your objects (set the parameter in the SpriteMovement component)
5. Adjust the actual speed in the MovementSpeedType scriptable object. This allows for a centralized change in game speed without going over all objects.