using UnityEngine;

public class MergeTutorial : BaseTutorial
{
    protected override void OnEnable()
    {
        GameManager.Instance.ShooterManager.OnMergeFinished += Close;
    }

    protected override void OnDisable()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.ShooterManager.OnMergeFinished -= Close;
        }
    }
}
