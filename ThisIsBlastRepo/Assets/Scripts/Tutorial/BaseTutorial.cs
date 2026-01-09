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

    protected virtual void OnEnable()
    {
        GameManager.Instance.ShooterManager.OnShooterActivated += Close;
    }

    protected virtual void OnDisable()
    {
        if(GameManager.Instance)
        {
            GameManager.Instance.ShooterManager.OnShooterActivated -= Close;
        }
    }

    protected void Close()
    {
        Hide();
    }
}
