using UnityEngine;

public interface IPickupable
{
    void OnPickedUp(Transform attachPoint);
    void OnDropped();
}
