using UnityEngine;

public class ChestManager : MonoBehaviour
{

    private void Start() {
        
        for(int i = 0; i < transform.childCount; i++) {
            
            Transform currentChest = transform.GetChild(i);
            Transform chestToSwap = transform.GetChild( Random.Range(0, transform.childCount) );

            if (currentChest == chestToSwap) continue;

            Vector3 posTemp = currentChest.position;
            Quaternion rotTemp = currentChest.rotation;
            
            currentChest.position = chestToSwap.position;
            currentChest.rotation = chestToSwap.rotation;

            chestToSwap.position = posTemp;
            chestToSwap.rotation = rotTemp;

        }
        
    }

}
