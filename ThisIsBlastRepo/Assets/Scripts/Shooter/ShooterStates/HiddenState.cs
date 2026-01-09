using UnityEngine;

public class HiddenState : IShooterState
{
    private Shooter shooter;
    public void Enter(Shooter shooter)
    {
        this.shooter = shooter;
        shooter.ActivateHiddenUI();
        shooter.activateButton.interactable = false;
    }

    public void Exit()
    {
        shooter.DeactivateHiddenUI();
    }

    public void Update() { }
}
