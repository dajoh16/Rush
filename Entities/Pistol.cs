using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame.Entities;

public class Pistol : IWeapon
{
    public Pistol(TimeSpan baseFiringCooldown)
    {
        BaseFiringCooldown = baseFiringCooldown;
        BaseProjectileSpeed = 250f;
        ProjectileHealth = 100f;
        TimeSinceLastFire = TimeSpan.Zero;
    }

    public TimeSpan BaseFiringCooldown { get; set; }
    public TimeSpan TimeSinceLastFire { get; set; }
    public float BaseProjectileSpeed { get; set; }
    public float ProjectileHealth { get; set; }
    
    public void Fire(GameState gameState, GameTime gameTime)
    {
        TimeSinceLastFire += gameTime.ElapsedGameTime;

        if (TimeSinceLastFire > (BaseFiringCooldown * gameState.Player.HasteRating))
        {
            //Fire by creating a new projectile
            SpawnPistolProjectile(gameState);
            TimeSinceLastFire = TimeSpan.Zero;
        }
    }

    private void SpawnPistolProjectile(GameState gameState)
    {
        var mouseState = Mouse.GetState();
        var playerPosition = gameState.Player.Position;
        
        var x = mouseState.X - playerPosition.X;
        var y = mouseState.Y - playerPosition.Y;
        var direction = new Vector2(x, y);

        var speed = BaseProjectileSpeed + ((gameState.Player.ProjectileSpeedPercentage ) * BaseProjectileSpeed);

        direction.Normalize();
        var pistolProjectile = new PistolProjectile(playerPosition, direction, speed, ProjectileHealth);
        gameState.Entities.Add(pistolProjectile);
    }
}

public class PistolProjectile : IEntity
{
    public PistolProjectile(Vector2 position, Vector2 direction, float speed, float health)
    {
        Position = position;
        Direction = direction;
        Speed = speed;
        Health = health;
    }

    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }
    public float Health { get; set; }
    
    
    public void LoadContent(Game game)
    {
        Texture = game.Content.Load<Texture2D>("pistolBullet");
    }

    public void Update(GameState gameState, GameTime gameTime)
    {
        var newX = Direction.X * Speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
        var newY = Direction.Y * Speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

        var positionX = Position.X + newX;
        var positionY = Position.Y + newY;
        var newPosition = new Vector2(positionX, positionY);
        
        Position = newPosition;
    }

    public void Draw(GameState gameState, GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture,
            Position,
            null,
            Color.White,
            0f,
            new Vector2(Texture.Width/2, Texture.Height/2),
            Vector2.One, 
            SpriteEffects.None,
            0f);
    }
}