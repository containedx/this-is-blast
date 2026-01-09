using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadingState : IGameState
{
    private GameManager game;

    private float min = 0f;
    private float max = 100f;
    float speed = 5f;
    private Slider slider;

    public void Enter(GameManager game)
    {
        this.game = game;
        View.Get<LoadingScreen>().Show();
        slider = View.Get<LoadingScreen>().slider;
        min = slider.minValue;
        max = slider.maxValue;
        slider.value = min;
    }

    public void Exit()
    {
        View.Get<LoadingScreen>().Hide();
    }

    public void Update()
    {
        // Fake loading :) 
        slider.value = Mathf.Lerp(slider.value, max, Time.deltaTime * speed);
        if (Mathf.Abs(slider.value - max) < 5f)
        {
            slider.value = max;
            Continue();
        }
    }

    private void Continue()
    {
        game.ChangeState(new PlayingState());
    }
}
