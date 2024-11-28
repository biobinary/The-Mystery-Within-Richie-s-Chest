using UnityEngine;

public class WorldLimit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        StatusLabelManager.instance.ShowText("Nothing there.");
    }

}
