using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyFirstGame.Entities;


namespace MyFirstGame;

public class GameProcessor : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameState _gameState;

    public GameProcessor()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        var entities = new List<IEntity>();
        var center = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
        var playerSpeed = 100f;
        var playerHp = 100f;

        var player = new Player(center, playerSpeed, playerHp);
        
        entities.Add(player);
        
      
        _gameState = new GameState()
        {
            Entities = entities,
            Player = player,
            GameHeight = _graphics.PreferredBackBufferHeight,
            GameWidth = _graphics.PreferredBackBufferWidth
        };
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _gameState.Entities.ForEach(e => e.LoadContent(this));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit(); 
        
        //Update ALL Entities
        _gameState.Entities.ForEach(e => e.Update(_gameState, gameTime));
        
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        

        _spriteBatch.Begin();
        _gameState.Entities.ForEach(e => e.Draw(_gameState, gameTime, _spriteBatch));
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
