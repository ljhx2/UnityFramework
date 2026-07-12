using UnityEngine;

/// <summary>
/// WeaponPickUpInteraction의 결과를 시각화하기 위한 헬퍼 스크립트. 아바타의 등에 무기 오브젝트가 나타나도록 함.
/// </summary>
public class WeaponHelper : MonoBehaviour
{
    [SerializeField] private GameObject _weapon;

    public void ToggleWeapon(bool val)
    {
        _weapon.SetActive(val);
    }
}
