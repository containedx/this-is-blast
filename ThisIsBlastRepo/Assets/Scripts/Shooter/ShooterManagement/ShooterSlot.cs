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

    public void PlaceShooter(Shooter shooter)
    {
        shooter.transform.parent = slotTransform;
        shooter.transform.localPosition = Vector3.zero;
    }
}
