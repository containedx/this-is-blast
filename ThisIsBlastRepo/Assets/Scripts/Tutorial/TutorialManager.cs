using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<Tutorial> tutorials = new List<Tutorial>();

    private int previousFeatureIndex = 0;

    #region Instance
    public static TutorialManager Instance { get; private set; }
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
    #endregion


    private void Start()
    {
        tutorials[0].tutorialView.Show();
        GameManager.Instance.OnLevelStarted += CheckTutorials;
    }

    public int GetPreviousFeatureIndex()
    {
        return previousFeatureIndex;
    }

    public int GetNextFeatureIndex()
    {
        if (tutorials.Count == 0) return 30;

        var next = tutorials[0].level;
        if (GameManager.Instance.GetLevelIndex() == next)
        {
            if (tutorials.Count == 0) return 30;
            next = tutorials[1].level;
        }

        return next;
    }

    private void CheckTutorials()
    {
        var currentLevel = GameManager.Instance.GetLevelIndex();

        tutorials.RemoveAll(t => t.level < currentLevel);

        foreach (var tutorial in tutorials)
        {
            if(tutorial.level == currentLevel)
            {
                previousFeatureIndex = currentLevel;
                tutorial.tutorialView.Show();
            }
        }
    }
}


[Serializable]
public class Tutorial
{
    public BaseTutorial tutorialView;
    public int level;
}
