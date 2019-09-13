# BattleshipTracker
This solution is built using:
**Visual Studio 2017 15.9.15 
**.NET Core 2.1 SDK**

## Projects
**1. Battleship.Core:**
   This Class Library project contains the logic of BattleshipGame / Tracker

**2. Battleship.Client:**
	This is a Console client project.

**3. Battleship.Services:**
	Console Client uses Services to avail Core logic


### Note: 
**Battleship.Core/Configuration drives the configuration as following:**
1. Rows: 
	Maximum number of Rows
2. Columns: 
	Maxium number of Columns
3. Difficulty: 
	Increasing difficulty results in sparser deployment of ships.

### Nice to have features(Sometimes in future):

1. Hit Location Search:

	Pick smaller set of squares to maximize AI success ratio:
	- i.e. starting with hitting in the middle square, for 10 x 10 grid it would be 5 x 5 square in centre
	- based on success in Step 1, pick quadrants or random locations
2. Game Modes:

  Two Player PLUS following
  - Assisted (Current mode)
  - Semi-assisted(Where AI can present potential hit locations and user makes choice)
  - Manual(Where user picks the attack location)
3. UI (Sky is the limit)
4. Data-Persistance

