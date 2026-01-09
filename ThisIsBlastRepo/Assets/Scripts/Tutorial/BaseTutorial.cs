using UnityEngine;

public class BaseTutorial : MonoBehaviour
{
    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.ShooterManager.OnShooterActivated += Close;
    }

    private void OnDisable()
    {
        if(GameManager.Instance)
        {
            GameManager.Instance.ShooterManager.OnShooterActivated -= Close;
        }
    }

    private void Close()
    {
        Hide();
    }
}
