using UnityEngine;

public class Guard : MonoBehaviour
{

    [SerializeField] private GameObject m_weaponContainer;
    [SerializeField] private GuardWeaponSO[] m_weapons;

    private void Start() {
        
        GuardWeaponSO weaponSO = m_weapons[Random.Range(0, m_weapons.Length)];
        
        GameObject guardWeapon = Instantiate(
            weaponSO.GetWeaponModel(),
            m_weaponContainer.transform.position,
            Quaternion.identity,
            m_weaponContainer.transform
        );

        guardWeapon.transform.localPosition = weaponSO.GetWeaponPosition();
        guardWeapon.transform.localEulerAngles = weaponSO.GetWeaponRotation();

    }

}
