
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
            if(game.GetBlocksCount() == 0)
            {
                game.ChangeState(new WinState());
            }
        }
    }
}
