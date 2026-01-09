public class InactiveState : IShooterState
{
    public void Enter(Shooter shooter)
    {
        shooter.ActivateInactiveUI();
        shooter.activateButton.interactable = false;
    }

    public void Exit() { }

    public void Update() { }
}
