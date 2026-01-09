using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    public UnityEvent<Shooter> OnActivate = new UnityEvent<Shooter>();

    [Header("Shooter Settings")]
    public int projectilesCount = 20;
    public BlockColor blockColor = BlockColor.Red;
    public float cooldownTime = 0.1f;

    [Header("Projectile")]
    [SerializeField] private Projectile projectilePrefab;

    [Header("UI")]
    public Button activateButton;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Color activeTextColor;
    [SerializeField] private Color inactiveTextColor;
    [SerializeField] private BlockMaterials materials;
    [SerializeField] private Image readyOutline;
    [SerializeField] private MeshRenderer meshRenderer;

    [NonSerialized]
    public List<ColumnBlocks> levelBlocks;

    public IShooterState currentState { private set; get; }

    private void Awake()
    {
        ChangeState(new InactiveState());
    }
    private void OnDestroy()
    {
        currentState?.Exit();
        currentState = null;
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void Setup(ShooterData shooterData)
    {
        blockColor = shooterData.blockColor;
        projectilesCount = shooterData.projectiles;

        readyOutline.color = materials.GetMaterial(blockColor).color;
        meshRenderer.material = materials.GetMaterial(blockColor);
        countText.text = projectilesCount.ToString();
    }

    public void ChangeState(IShooterState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public void DecreaseCount()
    {
        projectilesCount -= 1;
        countText.text = projectilesCount.ToString();
    }


    public void ActivateReadyUI()
    {
        ActivateOutline(true);
        countText.color = activeTextColor;
    }

    public void ActivateInactiveUI()
    {
        ActivateOutline(false);
        countText.color = inactiveTextColor;
    }
    public void ActivateActiveUI()
    {
        ActivateOutline(false);
        countText.color = activeTextColor;
    }

    private void ActivateOutline(bool value)
    {
        readyOutline.gameObject.SetActive(value);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
