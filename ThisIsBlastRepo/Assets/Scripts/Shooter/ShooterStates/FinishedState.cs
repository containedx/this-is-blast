using UnityEditor;
using UnityEngine;

public class FinishedState : IShooterState
{
    private Shooter shooter;
    private bool animate = false;

    private Vector3 targetPos;
    private float moveOffset = 5f;
    private float speed = 3f;

    public void Enter(Shooter shooter)
    {
        this.shooter = shooter;

        // check if its closer to left or right screen edge
        Vector3 v = Camera.main.WorldToViewportPoint(shooter.transform.position);
        bool goRight = v.x >= 0.5f;
        Debug.Log("right ? " + goRight);
        targetPos = shooter.transform.position;
        targetPos.x = goRight ? targetPos.x + moveOffset : targetPos.x - moveOffset;
        animate = true;
    }

    public void Exit() { }

    public void Update() 
    {
        if (!animate) return;

        MoveToTheSide();
    }

    private void MoveToTheSide()
    {
        shooter.transform.position = Vector3.Lerp(shooter.transform.position, targetPos, Time.deltaTime * speed);

        if (Mathf.Abs(shooter.transform.position.x - targetPos.x) < 0.05f)
        {
            FinishAnimate();
        }
    }

    private void FinishAnimate()
    {
        animate = false;
        shooter.ChangeState(new InactiveState());
        shooter.Destroy();
    }
}
