using Microsoft.Xna.Framework;

namespace MyFirstGame.Entities;

public interface IWeapon
{
    void Fire(GameState gameState, GameTime gameTime);
}