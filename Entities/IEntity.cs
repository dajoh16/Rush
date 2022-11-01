using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame.Entities;

public interface IEntity
{
    Texture2D Texture { get; set; }
    Vector2 Position { get; set; }
    float Speed { get; set; }
    float Health { get; set; }

    void LoadContent(Game game);
    void Update(GameState gameState, GameTime gameTime);

    void Draw(GameState gameState, GameTime gameTime, SpriteBatch spriteBatch);

}