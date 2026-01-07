using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Button activateButton;
    [SerializeField] private BlockMaterials materials;
    [SerializeField] private Image activeOutline;
    private MeshRenderer meshRenderer;

    [NonSerialized]
    public List<ColumnBlocks> levelBlocks;

    private IShooterState currentState;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        activateButton.onClick.AddListener(Activate);
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

        activeOutline.color = materials.GetMaterial(blockColor).color;
        meshRenderer.material = materials.GetMaterial(blockColor);
        countText.text = projectilesCount.ToString();
        ActivateOutline(false);
    }

    public void ChangeState(IShooterState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public void Activate()
    {
        this.levelBlocks = GameManager.Instance.currentLevelBlocks;
        OnActivate?.Invoke(this);
    }

    public void DecreaseCount()
    {
        projectilesCount -= 1;
        countText.text = projectilesCount.ToString();
    }

    public void ActivateOutline(bool value)
    {
        activeOutline.gameObject.SetActive(value);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
