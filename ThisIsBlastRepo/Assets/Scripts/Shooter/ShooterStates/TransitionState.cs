using UnityEngine;

public class TransitionState : IShooterState
{
    private Vector3 targetLocalPosition = Vector3.zero;
    private float speed = 8f;
    private Shooter shooter;

    public void Enter(Shooter shooter)
    {
        this.shooter = shooter;
    }

    public void Exit() { }

    public void Update() 
    {
        if (shooter == null) return;

        shooter.transform.localPosition = Vector3.Lerp(
            shooter.transform.localPosition,
            targetLocalPosition,
            Time.deltaTime * speed
        );
        
        if (Vector3.Distance(shooter.transform.localPosition, targetLocalPosition) < 2f)
        {
            shooter.transform.localPosition = Vector3.zero;
            shooter.ChangeState(new ActiveState());
        }
    }
}
