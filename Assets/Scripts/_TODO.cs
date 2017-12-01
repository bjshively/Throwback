// EMO TODO
// fix reappearing block sprite reappear
// look into zapper collision for small enemies (shouldn't get hit)
// spikes on lvl2 not spikey


// BRAD TODO
// knockback animation feels fucked up
// seesaw platform with physics
// Hook up door
// - Trigger for "complete"
// - Trigger for "open"
// Setup other tetris pieces
// Sound effects for door
// Set music volume for all stages



// EITHER TODO
// lvl2 needs music
// lvl3 needs music
// lvl99 needs music



// CANNOT REPRODUCE
// lvl1 bullets not destorying on ground





// PAULISH
// 
// ground pound/air melee



// DONE =================
// lvl2 turnarounds are colliding
// goombas don't bounce of breaking blocks
// bullets stick to lvl2 left edge
// sorting layer for UI and bullets, rockets
// Setup door animations
// Add some through platforms that force player to use
// Add some meaningful melee blocks in Level1
// Make level manager track # of pieces collected
// Player death sound effect
// Fix enemy distance to player (check prologue level)
// Platform that stays flat, but moves/rotates around a point (ciruclar moving platform)
// Fix player iframes/
// update player sprites on the UI layers
// Check on point filter for UI sprites
// PlayerNoitems has no Knockback frames.
// death sprite - use knockback sprite, mouth closed, less arched
// Send player animators "die" trigger.
// Implement no move death for 3 seconds.
// Add lower boundary object collision, "abyss"
// object colliders at the edge of the map
// start thinking about an opening animation
// Add scope cooldown frames
// - Also shyguy turns around on collision with Wall, where Platform turns around at center point. Can you set platform up to do this? It would help level layout I could use the edge of the start/end boxes for alignement.
// - Don't clear until the relevant button is pressed
// Add attribution for graphics (at least list games) and music/sound fx
// --SoundFX: https://opengameart.org/content/512-sound-effects-8-bit-style
// --Music: ericskiff.com/music/
// Brainstorm name
// Splash screen -- Maybe just player running in an open black space? Or maybe some ~static art of the pg/zapper/scope?
// enemy sprites that shoot projectiles
// crumbling sprite that reappears
// Fix nozapper jump frames
// Spike damage not working
// Unable to collect Tetris piece
// Add iFrames to hit sprite
// Dim screen with collectibles
// Send "die" trigger to BlockReappearing animator to trigger crumbly animation
// Send "die" trigger to trackplatform animator to turn on iframes until you kill it
// Something up with the scope bullet trajectory & speed, not quite 45 deg and maybe too fast?
// Melee bug: if you get hit during the first frame of melee, you take damage and still destory the enemy even if the melee FX never show up
// There is a difference with the enemies and their Wall collisions. The shyguy works, the goomba does not.
// - It looks like OnCollisionEnter vs OnTriggerEnter. I didn't want to mess with it.
// - On that note, can we get goomba to turnaround on actual walls, and not the ones I add via Tiled? I can make multiple object layers if you need both.
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
