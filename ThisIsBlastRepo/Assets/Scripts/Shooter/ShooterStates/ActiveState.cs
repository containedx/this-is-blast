

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState : IShooterState
{
    private Shooter shooter;
    private float shootTimer;
    private Transform shootTarget;

    public void Enter(Shooter shooter)
    {
        this.shooter = shooter;
        shooter.ActivateActiveUI();
        shootTimer = shooter.cooldownTime;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (shooter.levelBlocks == null)
            return;

        if (shooter.projectilesCount == 0) ShooterFinished();

        LerpTowardTarget();

        shootTimer -= Time.deltaTime;
        if (shootTimer > 0f) return;

        shootTimer = shooter.cooldownTime;
        FindNextTarget();
    }

    private void LerpTowardTarget()
    {
        // if shoot target is null go back to neutral
        Quaternion targetRotation = Quaternion.identity;
        if (shootTarget != null )
        {
            var direction = shootTarget.position - shooter.transform.position;
            direction.y = 0;
            targetRotation = Quaternion.LookRotation(direction);
        }

        shooter.transform.rotation = Quaternion.Slerp(shooter.transform.rotation, targetRotation, 5f * Time.deltaTime);
    }

    private void FindNextTarget()
    {
        shootTarget = null;

        shooter.levelBlocks.RemoveAll(col => col.IsEmpty());

        foreach (ColumnBlocks column in shooter.levelBlocks)
        {
            var target = column.TryToFindTarget(shooter.blockColor);
            if (target != null)
            {
                Shoot(target);
                break;
            }
        }
    }

    private void Shoot(Block target)
    {
        //Debug.Log("shooting " + target.name);
        shootTarget = target.transform;

        shooter.DecreaseCount();

        var projectile = ObjectPooler.Instance.SpawnFromPool(PoolObjectType.Projectile, shooter.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Shoot(target);
    }

    private void ShooterFinished()
    {
        Debug.Log("shooter finished");
        shooter.ChangeState(new FinishedState());
    }

    
}