using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text levelText;

    private int initialBlocksCount = 0;

    private void Start()
    {
        initialBlocksCount = GameManager.Instance.GetInitialBlocksCount();
        slider.maxValue = initialBlocksCount;
        slider.minValue = 0;
        slider.value = 0;

        GameManager.Instance.OnLevelStarted += UpdateLevel;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelStarted -= UpdateLevel;
    }

    private void Update()
    {
        slider.value = initialBlocksCount - GameManager.Instance.GetBlocksCount();
    }

    private void UpdateLevel()
    {
        int levelNumber = GameManager.Instance.GetLevelIndex() + 1;
        levelText.text = "Level " + levelNumber;
    }
}
