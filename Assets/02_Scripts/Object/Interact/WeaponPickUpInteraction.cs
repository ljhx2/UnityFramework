using UnityEngine;

public class WeaponPickUpInteraction : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interactor)
    {
        WeaponHelper weaponHelper;
        if (weaponHelper = interactor.GetComponent<WeaponHelper>())
        {
            if (weaponHelper.HasWeapon)
                return;

            weaponHelper.ToggleWeapon(true);
            weaponHelper.HasWeapon = true;
        }
        Destroy(gameObject);
    }
}
