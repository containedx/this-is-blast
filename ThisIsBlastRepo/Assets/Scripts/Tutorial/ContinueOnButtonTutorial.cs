using UnityEngine;
using UnityEngine.UI;

public class ContinueOnButtonTutorial : BaseTutorial
{
    public Button continueButton;
    protected override void OnEnable()
    {
        continueButton.onClick.AddListener(Close);
    }

    protected override void OnDisable()
    {
        continueButton.onClick.RemoveListener(Close);
    }
}
