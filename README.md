# 🎯 Sharp Shooter

A **first-person shooter prototype built in Unity** focused on implementing a **modular combat system and clean gameplay architecture**.

This project explores **data-driven weapon systems, event-driven gameplay logic, and modular combat mechanics** to create a scalable FPS foundation.

---

# 🎮 Gameplay Overview

Sharp Shooter is a small FPS sandbox where the player fights enemy robots using multiple weapons with different behaviors.

Core gameplay loop:

1. Player aims and shoots enemies
2. Weapons consume magazine ammo
3. Player reloads using reserve ammo
4. Enemies chase the player using NavMesh
5. Enemies explode on death and update game state
6. Game ends when all enemies are eliminated or player dies

---

# 🧠 Key Systems Implemented

## 🔫 Data-Driven Weapon System

Weapons are configured using **ScriptableObjects**, allowing new weapons to be created without modifying code.

Each weapon defines:

* Damage
* Fire Rate
* Fire Mode (Semi / Full Auto)
* Magazine Size
* Reserve Ammo
* Zoom Settings
* Weapon Prefab
* Sound Effects

This allows designers to tune weapon behaviour directly in the editor.

Example weapon configuration:

```
WeaponSO
 ├── damage
 ├── fireRate
 ├── magazineSize
 ├── reserveAmmo
 ├── fireMode
 ├── zoomFOV
 └── weaponPrefab
```

---

## 🔫 Weapon Controller Architecture

Weapon responsibilities are separated cleanly:

```
ActiveWeapon
   ↓ handles input & UI
Weapon
   ↓ handles firing logic
WeaponSO
   ↓ contains weapon data
```

### ActiveWeapon

Responsible for:

* Player input
* Weapon switching
* UI updates
* Zoom behavior

### Weapon

Responsible for:

* Shooting
* Fire rate control
* Fire mode logic
* Ammo management
* Reload logic
* Damage application

This separation keeps gameplay logic **clean and maintainable**.

---

## 🔄 Magazine + Reserve Ammo System

Weapons use a **two-tier ammo system** similar to real FPS games.

```
Magazine Ammo → bullets currently loaded
Reserve Ammo → extra ammunition inventory
```

Example:

```
30 / 90
↑    ↑
mag  reserve
```

Flow:

```
Shoot → magazine decreases
Reload → transfers reserve → magazine
Pickup → increases reserve ammo
```

---

## 💥 Interface-Based Damage System

The project uses a **damage interface** to decouple weapons from specific enemy implementations.

```
IDamageable
   ↓
EnemyHealth implements IDamageable
```

Weapon logic simply applies damage to any object implementing `IDamageable`.

```
Weapon → Raycast → IDamageable.TakeDamage()
```

This allows future expansion for:

* destructible props
* bosses
* breakable environment objects

---

## 🤖 Enemy AI (NavMesh)

Enemies use Unity's **NavMeshAgent** for movement.

Behavior:

```
Robot
   ↓
Find Player
   ↓
Navigate toward player
   ↓
Deal damage on collision
```

The AI stops moving when:

* player dies
* target becomes invalid

---

## 💀 Enemy Health System

Enemies implement the `IDamageable` interface.

Responsibilities:

* Track health
* Apply damage
* Spawn death effects
* Notify game systems when killed

Death triggers an **event broadcast**.

---

## 📡 Event-Driven Enemy Death System

Enemy death does **not directly modify game state**.

Instead:

```
EnemyHealth
   ↓
EnemyEvents.OnEnemyKilled
   ↓
EnemyCounter listens
   ↓
GameManager.PlayerWon()
```

This **decouples systems**, allowing new systems (score, achievements, analytics) to subscribe without modifying enemy code.

---

## 🏁 Game State Manager

GameManager handles:

* Win condition
* Lose condition
* Game over UI
* Cursor state
* Restart logic

Game ends when:

```
All enemies destroyed → YOU WIN
Player health reaches 0 → YOU LOSE
```

---

# 🏗 Project Architecture

```
Player
 ├── ActiveWeapon
 ├── PlayerHealth
 └── FirstPersonController

Weapons
 ├── Weapon
 └── WeaponSO (ScriptableObject)

Enemies
 ├── Robot (AI)
 └── EnemyHealth

Systems
 ├── EnemyEvents
 ├── EnemyCounter
 └── GameManager

Interfaces
 └── IDamageable
```

---

# 🛠 Tech Stack

* **Unity**
* **C#**
* Unity NavMesh
* ScriptableObjects
* Event-driven gameplay architecture

---

# 🎯 What I Focused On

This project was built to practice **gameplay programming architecture**, including:

* Clean separation of responsibilities
* Data-driven weapon design
* Event-based gameplay systems
* Modular combat mechanics
* Scalable gameplay architecture

---

# 🚀 Future Improvements

Possible expansions:

* Weapon recoil system
* Object pooling for effects
* Enemy state machines
* UI ammo indicators
* Score system
* Enemy wave spawning
