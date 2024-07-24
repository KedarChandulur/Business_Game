# Business_Game
This is a University project. Course number - GAM 440(Games with a purpose)

This project is a reference/built from one of the old board based business game(specifically played india).

Game engine used: Unity.
Programming language used: C#

Game video demo can be found here: [Business Game](https://youtu.be/OFX-oXPxE58)

Game demo can be downloaded from here: [Game_Demos](https://drive.google.com/drive/folders/1R_cf1DxQs3nRPIJxcY0E4LptXCEE-De5?usp=sharing)


# Additional Info
For the design of Business Game, I utilized the Triadic Game Design framework where needed. For example, to decide the final winner among players, I added an "assets owned" element. This allows players to sum their assets with their current liquid money, and the player with the highest total wins. Another implementation is the Tax square: if a player lands on the income tax square, they pay a 6% tax on their current money.

The programming was done in Unity using C#, adhering to SOLID principles and incorporating various software design patterns.

I employed the Singleton design pattern for the managers and used events to manage most of the game's functionality.

For Account and Money, I chose composition over inheritance. This is because transactions between players and the banker involve the same functionalities for both account types.

Inheritance was used for every square, as each square in the game can have completely different functionalities, but basic functionality is necessary. For example, highlighting the player's icon on the square is common for all squares.
