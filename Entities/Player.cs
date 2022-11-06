using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame.Entities;

public class Player : IEntity
{
    private static float MAX_HASTE = 1000f;
    private float _projectileSpeedPercentage;
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public float Speed { get; set; }
    public float Health { get; set; }
    public IWeapon EquippedWeapon { get; set; }
    public float HasteStat { get; set; }
    public float HasteRating => 1 - (HasteStat / MAX_HASTE);

    public float ProjectileSpeedPercentage
    {
        get => _projectileSpeedPercentage / 100;
        set => _projectileSpeedPercentage = value;
    }

    public Player(Vector2 position, float speed, float health)
    {
        Position = position;
        Speed = speed;
        Health = health;
        //EquippedWeapon = new Pistol(TimeSpan.FromSeconds(2));
        EquippedWeapon = new Shotgun(TimeSpan.FromSeconds(2));
        HasteStat = 0f; //0 Haste Stat
        ProjectileSpeedPercentage = 0f; //0% increase
    }
    
    public void LoadContent(Game game)
    {
        Texture = game.Content.Load<Texture2D>("player");
    }

    public void Update(GameState gameState, GameTime gameTime)
    {
        var keyState = Keyboard.GetState();
        UpdatePlayerPosition(gameState, gameTime, keyState);
        
        EquippedWeapon.Fire(gameState, gameTime);
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

    private void UpdatePlayerPosition(GameState gameState, GameTime gameTime, KeyboardState keyState)
    {
        Position = GetNewPlayerPosition(gameState, gameTime, keyState);
        
    }

    private Vector2 GetNewPlayerPosition(GameState gameState, GameTime gameTime, KeyboardState keyState)
    {
        var newPosition = Position;
        if (keyState.IsKeyDown(Keys.Up))
        {
            newPosition.Y -= Speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (keyState.IsKeyDown(Keys.Down))
        {
            newPosition.Y += Speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (keyState.IsKeyDown(Keys.Left))
        {
            newPosition.X -= Speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (keyState.IsKeyDown(Keys.Right))
        {
            newPosition.X += Speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        newPosition = KeepPlayerWithinBounds(gameState, newPosition);
        
        return newPosition;
    }

    private Vector2 KeepPlayerWithinBounds(GameState gameState, Vector2 position)
    {
        if(position.X > gameState.GameWidth - Texture.Width / 2)
        {
            position.X = gameState.GameWidth - Texture.Width / 2;
        }
        else if(position.X < Texture.Width / 2)
        {
            position.X = Texture.Width / 2;
        }

        if(position.Y > gameState.GameHeight - Texture.Height / 2)
        {
            position.Y = gameState.GameHeight - Texture.Height / 2;
        }
        else if(position.Y < Texture.Height / 2)
        {
            position.Y = Texture.Height / 2;
        }
        return position;
    }
}