Implement Asteroids with one additional feature

Unity version: 2021.3.20f1
        
Used packages:
* DOTween

Core gameplay:
Player controls a spaceship and has to destroy asteroids and UFO's to try and survive
  
* Player health
  * Player has 3 lives, once all lives are lost it is game over and you need to restart
        
* Player movement
  * Mimicks movement in space by using acceleration and deceleration
  * Turning left and right around axis
  * Can only move forwards
        
* Player shooting
  * Triggered via spacebar
        
* Projectiles
  * Self destroy after x seconds

* Asteroids
  * 3 sizes, small, medium, large
  * On hit, asteroids split into smaller sizes until smallest and then get destroyed
  * Smaller asteroids are faster making them harder to hit

* UFO's
  * Shoots projectiles
  * Takes 1 hit to destroy

* General
  * Objects wrap around the screen, eg. when going off the screen at the top, the object reappears at the bottom
  * All objects have 1 HP

* Levels
  * Once all asteroids and UFO's are destroyed, the next level spawns
  * Levels should increase in difficulty by spawning more asteroids

Day 1:
* Spent ~4 hours
* Basic project setup
* Noting down core gameplay of Asteroids
* Ideation for additional feature
* Ship controls
* Asteroid and level spawning + increasing spawns
* Ship lives
* Level boundaries based on screen
* UI animation for lives
* Ship animation on taking damage
        
Day 2:
* Spent ~3 hours
* Added asteroid sizes and splitting into 2 of the smaller size
* Quick particle effect on destroying asteroids and ship
* Added UFO and shoots in random direction
* Clean up and removal of hardcoded values
* Added basic UI for controls
        
Day 3:
* Spent ~4 hours
* Changed player movement from velocity to force
* Bug fixing
* Added pickup system
* Added shock wave projectile
* Cleanup