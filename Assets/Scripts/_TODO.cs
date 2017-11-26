// EMO TODO
// enemy sprites that shoot projectiles
// crumbling sprite that reappears
// start thinking about an opening animation
// Fix nozapper jump frames
// Spike damage not working
// Unable to collect Tetris piece
// Add iFrames to hit sprite



// BRAD TODO
// Melee bug: if you get hit during the first frame of melee, you take damage and still destory the enemy even if the melee FX never show up
// Swivel contraUpper in prefab based on angle of shot
// There is a difference with the enemies and their Wall collisions. The shyguy works, the goomba does not.
// - It looks like OnCollisionEnter vs OnTriggerEnter. I didn't want to mess with it.
// - On that note, can we get goomba to turnaround on actual walls, and not the ones I add via Tiled? I can make multiple object layers if you need both.
// - Also shyguy turns around on collision with Wall, where Platform turns around at center point. Can you set platform up to do this? It would help level layout I could use the edge of the start/end boxes for alignement.



// PAULISH
// Dim screen with collectibles
// - Don't clear until the relevant button is pressed
// - Can use MessageBG.png sprite behind text to darken everything else


// DONE =================
// Swap Contra's bullet with contrabullet
// Add death state to Contra animator
// Fix ContraLower sprite
// Constrain Contra firing to within some reasonable range (currently entire level)
// Make contra bullets respect walls
// die on pit death
// -- reset health, counters, reset level
// Create level management
// - START GAME
// - GAME OVER
// damage from hazards
// Enemies can harm you while they are dying
// Fix sliding down slopes
// - LEVEL ADVANCE
// -- "collect" rocket
// -- rocket moves up to edge of screen
// -- Collect Tetris piece
// -- send "fall" trigger to Cage
// Tell me best way to add 'no glove or gun' and 'no gun' sprites (animator state, or separate player obj)
// Add small explosions to enemy kill
// Wire Melee animations - should destroy only one block in front of player (see Mario bricks for testing)
// melee sprites
// visually distinct platform sprites
// Animate tetris sprites as pickups
// end of level rocket
// update player sprite for stand and run without glove
// powerglove sprite
// explosion animation
// finish melee sprite
// Veritcal moving platform
// make pickup colliders trigger
