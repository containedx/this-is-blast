

using UnityEngine;
using UnityEngine.UI;

public class WinState : IGameState
{
    private GameManager game;
    private Slider slider;
    private float min = 0f;
    private float target = 0f;
    private float max = 100f;
    float speed = 5f;

    public void Enter(GameManager game)
    {
        this.game = game;
        View.Get<WinScreen>().ShowDelayed(0.5f);
        View.Get<WinScreen>().confetti.Play();
        View.Get<WinScreen>().continueButton.onClick.AddListener(Continue);
        
        int levelNumber = GameManager.Instance.GetLevelIndex() + 1;
        View.Get<WinScreen>().levelText.text = "Level " + levelNumber;

        slider = View.Get<WinScreen>().newFeatureSlider;
        min = TutorialManager.Instance.GetPreviousFeatureIndex();
        slider.minValue = min;
        max = TutorialManager.Instance.GetNextFeatureIndex() - 1;
        slider.maxValue = max;
        target = game.GetLevelIndex();
        slider.value = min;

        View.Get<WinScreen>().newFeatureText.text = "New Feature";
    }

    public void Exit()
    {
        View.Get<WinScreen>().continueButton.onClick.RemoveListener(Continue);
        View.Get<WinScreen>().Hide();
    }

    public void Update()
    {
        if(slider != null)
        {
            slider.value = Mathf.Lerp(slider.value, target, Time.deltaTime * speed);
            if (Mathf.Abs(slider.value - max) < max * 0.1f)
            {
                slider.value = max;
                View.Get<WinScreen>().newFeatureText.text = "Unlocked New Feature!";
            }
        }
    }

    private void Continue()
    {
        game.NextLevel();
    }
}
