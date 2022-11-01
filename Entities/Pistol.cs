using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame.Entities;

public class Pistol : IWeapon
{
    public void Fire(GameState gameState)
    {
        throw new System.NotImplementedException();
    }
}

public class PistolProjectile : IEntity
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }
    public float Health { get; set; }
    
    
    
    public void LoadContent(Game game)
    {
        throw new System.NotImplementedException();
    }

    public void Update(GameState gameState, GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }

    public void Draw(GameState gameState, GameTime gameTime, SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }
}