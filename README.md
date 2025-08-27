# Bullet Hell Shooter

## 🎮 Project Overview  
This project was created for a **Computer Graphics Course Midterm Activity**, implementing a *Bullet Hell Shooter* in **Unity**. It features a single boss with **three unique bullet patterns**. The focus is on creating visually appealing attack sequences while optimizing performance.  

## 🏹 Key Features  
- **Three unique attack patterns**.
- **Time-based phase changes** using `TimeManager`.  
- **Object Pooling** for bullet optimization.  
- **Real-time bullet counter** and **in-game timer UI**.  

## 🔫 Patterns  
- **Shoot** → Linear bullets + corner movement.  
- **RadialShoot** → Rotating radial shots, from flower to spiral.  
- **StarShoot** → Rotating star pattern with speed variation.  

Each pattern is unique due to its movement, rotation, and bullet behavior.  

## 📺 Demo Video  
[▶ Watch on YouTube](https://www.youtube.com/watch?v=2nRMwH3QkoI)  

## 🛠 How to Run  
1. Clone the repo:  
   ```bash
   git clone https://github.com/jorgeGrVl/BulletHell-Shooter.git
2. Open the project in **Unity 2021.3 LTS or later**.
3. Run `MainScene` to start the game.

## 🧩 Files / Scripts of interest

- `SpaceshipBoss.cs` — orchestrates boss phases and movements.
- `Shoot.cs` — simple linear shooter with cooldown.
- `RadialShoot.cs` — radial bursts with alternating curve.
- `StarShoot.cs` — star pattern using angle-based speed variation.
- `BulletPool.cs` — object pool + visible bullet counter.
- `Bullet.cs` — bullet movement, curve and lifetime.
- `TimeManager.cs` — in-game clock and events.
- `TimeUI.cs` — updates on-screen timer.
