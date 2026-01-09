using UnityEngine;

public class InitialTutorial : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.ShooterManager.OnShooterActivated += Close;
    }

    private void OnDisable()
    {
        GameManager.Instance.ShooterManager.OnShooterActivated -= Close;
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    
}
