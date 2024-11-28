using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject other);
    void OnBeginFacing();
    void OnEndFacing();
}
