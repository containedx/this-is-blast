

using System.Collections.Generic;

public class PlayingState : IGameState
{
    GameManager game;

    public void Enter(GameManager game)
    {
        this.game = game;
    }

    public void Exit()
    {
       
    }

    public void Update()
    {
        if(game != null)
        {
            CheckIfWin();
            CheckIfFail();
        }
    }

    private void CheckIfWin()
    {
        if (game.GetBlocksCount() == 0)
        {
            game.ChangeState(new WinState());
        }
    }

    private void CheckIfFail()
    {
        // if there is empty active slot, then no worries
        if (game.ShooterManager.CheckIfAnyEmptyActiveSlots()) return;

        List<BlockColor> bottomBlocksColor = new List<BlockColor>();
        foreach(var column in game.currentLevelBlocks)
        {
            if(!column.IsEmpty())
            {
                BlockColor color = column.GetBottomColor();

                if (!bottomBlocksColor.Contains(color))
                {
                    bottomBlocksColor.Add(color);
                }
            }
        }

        List<BlockColor> shootersColor = game.ShooterManager.GetActiveShootersColor();

        foreach(var shooterColor in shootersColor)
        {
            if (bottomBlocksColor.Contains(shooterColor)) return;
        }

        game.ChangeState(new FailState());
    }
}
