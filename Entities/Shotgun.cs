using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame.Entities;

public class Shotgun : IWeapon
{
    public Shotgun(TimeSpan baseFiringCooldown)
    {
        BaseFiringCooldown = baseFiringCooldown;
        TimeSinceLastFire = TimeSpan.Zero;
        BaseProjectileSpeed = 250f;
        ProjectileHealth = 100f;
        Spread = 0.261799388f; // 15 Degrees in Radians
    }

    public float Spread { get; set; }
    public TimeSpan BaseFiringCooldown { get; set; }
    public TimeSpan TimeSinceLastFire { get; set; }
    public float BaseProjectileSpeed { get; set; }
    public float ProjectileHealth { get; set; }
    
    public void Fire(GameState gameState, GameTime gameTime)
    {
        TimeSinceLastFire += gameTime.ElapsedGameTime;

        if (TimeSinceLastFire > (BaseFiringCooldown * gameState.Player.HasteRating))
        {
            //Fire by creating 3 new projectiles
            SpawnShotgunProjectiles(gameState, gameTime);
            TimeSinceLastFire = TimeSpan.Zero;
        }
    }

    private void SpawnShotgunProjectiles(GameState gameState, GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        var playerPosition = gameState.Player.Position;
        
        var x = mouseState.X - playerPosition.X;
        var y = mouseState.Y - playerPosition.Y;
        var direction = new Vector2(x, y);

        var speed = BaseProjectileSpeed + ((gameState.Player.ProjectileSpeedPercentage ) * BaseProjectileSpeed);

        direction.Normalize();
        var centerProjectile = new PistolProjectile(playerPosition, direction, speed, ProjectileHealth);
        gameState.Entities.Add(centerProjectile);

        var angle = (double) Spread;
        var ccwDirectionX =(float) (direction.X * Math.Cos(angle) + direction.Y * Math.Sin(angle));
        var ccwDirectionY =(float) (-direction.X * Math.Sin(angle) + direction.Y * Math.Cos(angle));
        var ccwAngledProjectileDirection = new Vector2(ccwDirectionX, ccwDirectionY);
        
        var ccwAngledProjectile = new PistolProjectile(playerPosition, ccwAngledProjectileDirection, speed, ProjectileHealth);
        gameState.Entities.Add(ccwAngledProjectile);
        
        var cwDirectionX =(float) (direction.X * Math.Cos(angle) - direction.Y * Math.Sin(angle));
        var cwDirectionY =(float) (direction.X * Math.Sin(angle) + direction.Y * Math.Cos(angle));
        var cwAngledProjectileDirection = new Vector2(cwDirectionX, cwDirectionY);
        
        var cwAngledProjectile = new PistolProjectile(playerPosition, cwAngledProjectileDirection, speed, ProjectileHealth);
        gameState.Entities.Add(cwAngledProjectile);
    }
}

