using System;
using System.Collections.Generic;
using UnityEngine;

public class ShooterContainer 
{
    public List<ShooterSlot> slots = new List<ShooterSlot>();
    public int columnCount = 3;

    public bool PlaceOntoFirstEmptySlot(Shooter shooter, bool changePosition = true)
    {
        ShooterSlot firstEmptySlot = FindFirstEmptySlot();
        if (firstEmptySlot == null)
        {
            Debug.Log("NO EMPTY SLOTS");
            return false;
        }

        firstEmptySlot.PlaceShooter(shooter, changePosition);
        return true;
    }

    public ShooterSlot FindFirstEmptySlot()
    {
        foreach(var slot in slots)
        {
            if (slot.IsEmpty()) return slot;
        }

        return null;
    }

    public ShooterSlot FindSlot(Shooter shooter)
    {
        foreach (var slot in slots)
        {
            if (slot.shooter == shooter) return slot;
        }

        return null;
    }

    public void MoveShooterBelowEmptySpace(Shooter shooter)
    {
        int emptySlotIndex = slots.IndexOf(FindSlot(shooter));
        int slotBelowIndex = emptySlotIndex + columnCount;
        //move the whole column up
        while(slotBelowIndex < slots.Count )
        {
            Debug.Log("trying to move " + slotBelowIndex + " to" + emptySlotIndex);
            if (slots[slotBelowIndex].IsEmpty()) return;

            ShooterSlot emptySlot = slots[emptySlotIndex];
            emptySlot.PlaceShooter(slots[slotBelowIndex].shooter);
            Debug.Log("SUCCESS " + slotBelowIndex + " to" + emptySlotIndex);
            emptySlotIndex = slotBelowIndex;
            slotBelowIndex = emptySlotIndex + columnCount;
        }
    }
}
