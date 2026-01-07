using UnityEngine;
public class ShooterSlot
{
    public Transform slotTransform;

    public ShooterSlot(Transform slotTransform)
    {
        this.slotTransform = slotTransform;
    }
    public bool IsEmpty()
    {
        return slotTransform.childCount == 0;
    }

    public void PlaceShooter(Shooter shooter, bool changePosition=true)
    {
        shooter.transform.parent = slotTransform;
        if(changePosition) shooter.transform.localPosition = Vector3.zero;
    }
}
