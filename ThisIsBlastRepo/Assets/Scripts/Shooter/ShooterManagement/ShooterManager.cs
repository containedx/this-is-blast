using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterManager : MonoBehaviour
{
    public Action OnShooterActivated;

    [SerializeField] private Shooter shooterPrefab;
    [SerializeField] private Transform activeShooterSlotPrefab;
    [SerializeField] private Transform readyShooterSlotPrefab;

    [SerializeField] private Transform activeShooterSlotsParent;
    [SerializeField] private Transform readyShooterSlotsParent;

    private List<Shooter> shooters = new List<Shooter>();

    private ShooterContainer readySlots;
    private ShooterContainer activeSlots;

    public void SpawnShooters(LevelData levelData)
    {
        CleanUp();

        readySlots = new ShooterContainer();
        activeSlots = new ShooterContainer();

        activeSlots.columnCount = levelData.activeShootersCount;
        for (int i = 0; i < levelData.activeShootersCount; i++)
        {
            Transform activeSlot = Instantiate(
                activeShooterSlotPrefab,
                activeShooterSlotsParent
            );

            activeSlots.slots.Add(new ShooterSlot(activeSlot));
        }

        readySlots.columnCount = levelData.readyShootersColumnsCount;
        var grid = readyShooterSlotsParent.GetComponent<GridLayoutGroup>();
        grid.constraintCount = readySlots.columnCount;
        
        for (int i = 0; i < levelData.shooters.Count; i++)
        {
            Transform readySlot = Instantiate(
                readyShooterSlotPrefab,
                readyShooterSlotsParent
            );
            readySlots.slots.Add(new ShooterSlot(readySlot));
        }

        int index = 0;
        foreach (var shooterData in levelData.shooters)
        {
            Shooter shooter = Instantiate(shooterPrefab, transform);

            readySlots.PlaceOntoFirstEmptySlot(shooter);
            shooter.OnActivate.AddListener(OnShooterActivate);

            index++;
            shooter.Setup(shooterData);
            shooters.Add(shooter);
        }

        for (int i = 0; i < readySlots.columnCount; i++)
        {
            readySlots.slots[i].shooter.ChangeState(new ReadyState());
        }

        if(levelData.hiddenShootersCount > 0)
        {
            var columns = readySlots.columnCount;
            var hiddenCount = levelData.hiddenShootersCount;
            index = columns;
            var shootersLeft = readySlots.slots.Count - columns;
            bool random = true;
            if (shootersLeft == hiddenCount) random = false;
            for (int i = 0; i < hiddenCount; i++)
            {
                readySlots.slots[index].shooter.ChangeState(new HiddenState());
                
                if(random) index = UnityEngine.Random.Range(columns, readySlots.slots.Count);
                else index++;
            }
        }
    }

    public void OnShooterActivate(Shooter shooter)
    {
        bool success = activeSlots.PlaceOntoFirstEmptySlot(shooter, false);
        if (success)
        {
            shooter.ChangeState(new TransitionState());
            readySlots.MoveShooterBelowEmptySpace(shooter);
            OnShooterActivated?.Invoke();
        }
    }

    public bool CheckIfAnyEmptyActiveSlots()
    {
        foreach(var slot in activeSlots.slots)
        {
            if (slot.IsEmpty()) return true;
        }
        return false;
    }

    public List<BlockColor> GetActiveShootersColor()
    {
        List<BlockColor> colors = new List<BlockColor>();

        foreach (var slot in activeSlots.slots)
        {
            if (slot.IsEmpty()) continue;
            BlockColor color = slot.shooter.blockColor;

            if (!colors.Contains(color))
            {
                colors.Add(color);
            }
        }

        return colors;
    }

    private void CleanUp()
    {
        //TODO: object pool slots
        if (readySlots != null)
        {
            foreach (var slot in readySlots.slots)
            {
                Destroy(slot.slotTransform.gameObject);
            }
        }
        
        if( activeSlots != null)
        {
            foreach (var slot in activeSlots.slots)
            {
                Destroy(slot.slotTransform.gameObject);
            }
        }
    }
}
