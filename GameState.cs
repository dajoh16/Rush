using System.Collections.Generic;
using MyFirstGame.Entities;

namespace MyFirstGame;

public class GameState
{
    public Player Player { get; set; }
    public List<IEntity> Entities { get; set; }
    public int GameHeight { get; set; }
    public int GameWidth { get; set; }
}