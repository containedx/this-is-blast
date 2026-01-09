using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<Tutorial> tutorials = new List<Tutorial>();

    private void Start()
    {
        tutorials[0].tutorialView.Show();
        GameManager.Instance.OnLevelStarted += CheckTutorials;
    }

    private void CheckTutorials()
    {
        var currentLevel = GameManager.Instance.GetLevelIndex();

        tutorials.RemoveAll(t => t.level < currentLevel);

        foreach (var tutorial in tutorials)
        {
            if(tutorial.level == currentLevel)
            {
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
