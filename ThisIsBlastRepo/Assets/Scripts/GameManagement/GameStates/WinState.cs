

public class WinState : IGameState
{
    private GameManager game;
    public void Enter(GameManager game)
    {
        this.game = game;
        View.Get<WinScreen>().ShowDelayed(0.5f);
        View.Get<WinScreen>().confetti.Play();
        View.Get<WinScreen>().continueButton.onClick.AddListener(Continue);
        
        int levelNumber = GameManager.Instance.GetLevelIndex() + 1;
        View.Get<WinScreen>().levelText.text = "Level " + levelNumber;
    }

    public void Exit()
    {
        View.Get<WinScreen>().continueButton.onClick.RemoveListener(Continue);
        View.Get<WinScreen>().Hide();
    }

    public void Update()
    {
        
    }

    private void Continue()
    {
        game.NextLevel();
    }
}
