using System;
using UnityEngine;

public class MergeState : IShooterState
{
    private Vector3 targetPosition = Vector3.zero;
    private float speed = 4f;
    private Shooter shooter;

    private bool middle = false;
    private int projectilesToAdd = 0;
    private float delayTimer = 0f;

    private Shooter leftShooter;
    private Shooter rightShooter;

    private Action onMergeFinished;

    public MergeState(Shooter left, Shooter right, Action onMergeFinished) 
    {
        // this is middle shooter
        middle = true;
        leftShooter = left;
        rightShooter = right;
        projectilesToAdd = left.projectilesCount + right.projectilesCount;
        this.onMergeFinished = onMergeFinished;
    }

    public MergeState(Vector3 targetShooterPosition)
    {
        targetPosition = targetShooterPosition;
    }

    public void Enter(Shooter shooter)
    {
        this.shooter = shooter;
    }

    public void Exit() { }

    public void Update() 
    {
        if (shooter == null) return;

        if (middle)
        {
            delayTimer += Time.deltaTime;
            Debug.Log(delayTimer);
            if (delayTimer > 0.5f)
            {
                Debug.Log("middle shooter end merge ");
                middle = false;
                shooter.projectilesCount += projectilesToAdd;
                shooter.UpdateProjectilesCountText();
                leftShooter.Destroy();
                rightShooter.Destroy();
                onMergeFinished?.Invoke();
                shooter.ChangeState(new ActiveState());
            }
            return;
        }

        shooter.transform.position = Vector3.Lerp(
            shooter.transform.position,
            targetPosition,
            Time.deltaTime * speed
        );
        
        if (Vector3.Distance(shooter.transform.position, targetPosition) < 0.01f)
        {
            shooter.transform.position = targetPosition;
        }
    }
}
