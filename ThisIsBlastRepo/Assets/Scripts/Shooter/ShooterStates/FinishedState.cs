public class FinishedState : IShooterState
{
    public void Enter(Shooter shooter)
    {
        //TODO: animate, shooter move to the side
        shooter.gameObject.SetActive(false);
    }

    public void Exit() { }

    public void Update() { }
}
