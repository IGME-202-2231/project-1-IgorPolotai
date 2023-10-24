# Project When Cows Strike Back

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Igor Polotai
-   Section: 03/04

## Game Design

-   Camera Orientation: Top Down
-   Camera Movement: Does not move
-   Player Health: 10
-   End Condition: When you lose all hitpoints, or beat the final boss
-   Scoring: Burgering cows

### Game Description

You are an alien, but the normally docile hostages, cows, have decided to finally stand up and not be abducted so easily. Problem is, you have a quota to fill! Get those cows!

### Controls

-   Movement
    -   Up: Up Arrow
    -   Down: Down Arrow
    -   Left: Left Arrow
    -   Right: Right Arrow
-   Fire: Left Mouse Click
-   Charge: Right Mouse Click

## Player

The player starts with 10 health. Colliding with an enemy bullet or enemy decreases your health. If you reach 0 health, you lose the game. The player moves with the arrow keys and fires with the right mouse click. Additionally, killing cows fills up your Charge meter. When the meter is full, you can activate a charge to get rid of all bullets and enemies on screen. For bullets, I made is so bullets only fire when the click is active. Each bullet does two damage

## Enemies

Cow: Basic cow. Slow fire speed. Has one health.
Soldier Cow: Stronger cow. Faster fire speed. Has three health.
Commando Cow: Stronger soldier cow. Even faster fire speed. Has five health.
Scout Cow: Shoots at your location, with a slow firing speed. Has one health.
Armor Cow: Slow movement and fire speed, but can take a lot of health. Has thirteen health.
Radioactive Cow: Fires green bullets, that if hit, will cause the player to be permenantly slowed by 5%. Does not decrease health.
The (Milk) Tank: Final Boss. Fires triple bullets, and will stay on one side of the screen. Has a massive one hundread health. If killed, game ends.

Enemies are spawned based on waves. There are six waves. Each wave starts every sixty seconds. The Boss spawns after five minutes.

## Make It My Own

All of the art assets were created by me. I created a total of eight enemies, but one of them (the Medic Cow), was cut for being too buggy. Additionally, I programed a robust spawn system that dynamically makes the game harder as the game goes on, and created the Charge mechanic in addition to basic movement and shooting to give you more options to defend against the onslaught, and to give you an incentive to actually kill the cows. I tested the game, and it can be beaten, though it is not easy. I am happy with the difficulty curve.

## Sources

All assets have been made by myself.

## Known Issues

All code that caused issues (like restarting the game, or the medic enemy, have been cut. Their code still exists in the source files in case I want to come back to this in the future).

### Requirements not completed

All requirements complete

