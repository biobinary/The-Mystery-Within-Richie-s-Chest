using UnityEngine;

[CreateAssetMenu(fileName="New Guard Weapon", menuName="Scriptable Objects/Guard Weapon")]
public class GuardWeaponSO : ScriptableObject
{
    
    [SerializeField] private GameObject m_weaponModel;
    [SerializeField] private Vector3 m_weaponPosition;
    [SerializeField] private Vector3 m_weaponRotation;

    public GameObject GetWeaponModel() {
        return m_weaponModel;
    }

    public Vector3 GetWeaponPosition() { 
        return m_weaponPosition;
    }

    public Vector3 GetWeaponRotation() {
        return m_weaponRotation;
    }

}
