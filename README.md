# ChronoTactics

A 2D turn-based puzzle-tactics game engine and prototype built in C#. Players navigate a grid-based arena by manipulating time, creating timeline clones of past actions to press pressure plates, open sealed doors, and reach target goals.

---

## Overview

`ChronoTactics` demonstrates clean object-oriented architecture and system separation for time-manipulation game mechanics. Instead of real-time physics, it uses a discrete turn-based action system to track player inputs, maintain timeline state history, and project past actions into active time clones.

---

## Project Structure

```text
ChronoTactics/
├── ChronoTactics.csproj
├── Program.cs
├── Engine/
│   ├── GameLoop.cs
│   ├── GridMap.cs
│   └── InputManager.cs
├── Systems/
│   ├── StateMachine.cs
│   └── TimeRewindSystem.cs
├── Entities/
│   ├── Door.cs
│   ├── Player.cs
│   ├── PressurePlate.cs
│   └── TimeClone.cs
└── Scenes/
    └── GameScene.cs
