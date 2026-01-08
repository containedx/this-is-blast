public class InactiveState : IShooterState
{
    public void Enter(Shooter shooter)
    {
        shooter.ActivateInactiveUI();
    }

    public void Exit() { }

    public void Update() { }
}
