using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [Header("Shooter Settings")]
    public int projectilesCount = 20;
    public BlockColor blockColor = BlockColor.Red;
    public float cooldownTime = 0.2f;

    [Header("Projectile")]
    [SerializeField] private Projectile projectilePrefab;

    [Header("UI")]
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Button activateButton;

    [NonSerialized]
    public List<ColumnBlocks> levelBlocks;

    private IShooterState currentState;

    private void Awake()
    {
        countText.text = projectilesCount.ToString();
        activateButton.onClick.AddListener(Activate);
        SetState(new InactiveState());
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void SetState(IShooterState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public void Activate()
    {
        this.levelBlocks = GameManager.Instance.currentLevelBlocks;
        SetState(new ActiveState());
    }

    public void DecreaseCount()
    {
        projectilesCount -= 1;
        countText.text = projectilesCount.ToString();
    }

    

    
}
