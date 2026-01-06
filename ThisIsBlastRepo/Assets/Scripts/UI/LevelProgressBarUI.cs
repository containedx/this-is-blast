using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private int initialBlocksCount = 0;

    private void Start()
    {
        initialBlocksCount = GameManager.Instance.GetInitialBlocksCount();
        slider.maxValue = initialBlocksCount;
        slider.minValue = 0;
        slider.value = 0;
    }

    private void Update()
    {
        slider.value = initialBlocksCount - GameManager.Instance.GetBlocksCount();
    }
}
