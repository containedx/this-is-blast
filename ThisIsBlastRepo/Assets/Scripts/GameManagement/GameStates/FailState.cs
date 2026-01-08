
public class FailState : IGameState
{
    private GameManager game;

    public void Enter(GameManager game)
    {
        this.game = game;
        View.Get<FailScreen>().ShowDelayed(1.5f);
        View.Get<FailScreen>().tryAgainButton.onClick.AddListener(TryAgain);

        int levelNumber = GameManager.Instance.GetLevelIndex() + 1;
        View.Get<FailScreen>().levelText.text = "Level " + levelNumber;
    }

    public void Exit()
    {
        View.Get<FailScreen>().tryAgainButton.onClick.RemoveListener(TryAgain);
        View.Get<FailScreen>().Hide();
    }

    public void Update()
    {
        
    }

    private void TryAgain()
    {
        game.RepeatLevel();
    }
}
