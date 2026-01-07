using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MonoBehaviour
{
    [SerializeField] private Shooter shooterPrefab;
    [SerializeField] private Transform activeShooterSlotPrefab;
    [SerializeField] private Transform readyShooterSlotPrefab;

    [SerializeField] private Transform activeShooterSlotsParent;
    [SerializeField] private Transform readyShooterSlotsParent;

    private ShooterContainer readySlots;
    private ShooterContainer activeSlots;

    public void SpawnShooters(LevelData levelData)
    {
        readySlots = new ShooterContainer();
        activeSlots = new ShooterContainer();

        for (int i = 0; i < levelData.activeShootersCount; i++)
        {
            Transform activeSlot = Instantiate(
                activeShooterSlotPrefab,
                activeShooterSlotsParent
            );

            activeSlots.slots.Add(new ShooterSlot(activeSlot));
        }

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

            index++;
            shooter.Setup(shooterData);
        }
    }
}
