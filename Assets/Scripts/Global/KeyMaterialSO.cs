using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key Materials", menuName = "Scriptable Objects/Key Materials")]
public class KeyMaterialSO : ScriptableObject
{
    
    [SerializeField] private List<Material> m_materials;

    public Material GetMaterialByKeyType(ChestKey.TYPE type) {
        int index = ((int)type);
        return m_materials[index];
    }

}
