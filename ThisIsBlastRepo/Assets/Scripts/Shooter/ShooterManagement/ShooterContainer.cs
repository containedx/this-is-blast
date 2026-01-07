using System;
using System.Collections.Generic;
using UnityEngine;

public class ShooterContainer 
{
    public List<ShooterSlot> slots = new List<ShooterSlot>();

    public void PlaceOntoFirstEmptySlot(Shooter shooter, bool changePosition = true)
    {
        ShooterSlot firstEmptySlot = FindFirstEmptySlot();
        if (firstEmptySlot == null)
        {
            Debug.Log("NO EMPTY SLOTS");
            return;
        }

        firstEmptySlot.PlaceShooter(shooter, changePosition);
    }

    public ShooterSlot FindFirstEmptySlot()
    {
        foreach(var slot in slots)
        {
            if (slot.IsEmpty()) return slot;
        }

        return null;
    }
}
