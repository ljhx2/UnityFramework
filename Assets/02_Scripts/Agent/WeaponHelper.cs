using UnityEngine;

/// <summary>
/// WeaponPickUpInteraction의 결과를 시각화하기 위한 헬퍼 스크립트. 아바타의 등에 무기 오브젝트가 나타나도록 함.
/// </summary>
public class WeaponHelper : MonoBehaviour
{
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject _playerHandWeapn;

    //무기가 무기집에 들어 있는지 여부를 확인하는 플래그
    public bool IsWeaponHolstered { get; private set; } = false;
    //다른 무기를 획득할 수 없도록 하는 플래그
    public bool HasWeapon { get; set; }

    //무기를 캐릭터의 등 또는 손에 들지 전환하는 메서드
    public void ToggleWeapon(bool val)
    {
        _weapon.SetActive(val);
        _playerHandWeapn.SetActive(!val);
        IsWeaponHolstered = val;
    }
}
