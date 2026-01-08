using UnityEngine;

public class ReadyState : IShooterState
{
    private Shooter shooter;

    public void Enter(Shooter shooter)
    {
        this.shooter = shooter;
        shooter.ActivateReadyUI();
        shooter.activateButton.onClick.AddListener(Activate);
    }

    public void Exit() 
    {
        shooter.activateButton.onClick.RemoveListener(Activate);
    }

    public void Update() { }


    private void Activate()
    {
        shooter.levelBlocks = GameManager.Instance.currentLevelBlocks;
        shooter.OnActivate?.Invoke(shooter);
    }
}
