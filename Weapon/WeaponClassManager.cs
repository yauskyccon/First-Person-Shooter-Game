using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponClassManager : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint leftHandIK;
    public Transform recoilFollowPos;
    ActionStateManager actions;

    public WeaponManager[] weapons;
    int currentWeaponIndex;

    public delegate void WeaponChanged(WeaponManager newWeapon);
    public event WeaponChanged OnWeaponChanged;

    private void Awake()
    {
        currentWeaponIndex = 0;
        for(int i=0; i < weapons.Length; i++)
        {
            if (i == 0) weapons[i].gameObject.SetActive(true);
            else weapons[i].gameObject.SetActive(false);
        }

        if (weapons.Length > 0)
        {
            OnWeaponChanged?.Invoke(weapons[currentWeaponIndex]);
            FindObjectOfType<ScoreManager>().UpdateAmmo(weapons[currentWeaponIndex].GetComponent<WeaponAmmo>());
        }
    }

    public void SetCurrentWeapon(WeaponManager weapon)
    {
        if (actions == null) actions = GetComponent<ActionStateManager>();
        leftHandIK.data.target = weapon.leftHandTarget;
        leftHandIK.data.hint = weapon.leftHandHint;
        actions.SetWeapon(weapon);
        OnWeaponChanged?.Invoke(weapon);
        FindObjectOfType<ScoreManager>().UpdateAmmo(weapon.GetComponent<WeaponAmmo>());
    }

    public void ChangeWeapon(float direction)
    {
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        if (direction < 0)
        {
            if (currentWeaponIndex == 0) currentWeaponIndex = weapons.Length - 1;
            else currentWeaponIndex--;
        }
        else
        {
            if (currentWeaponIndex == weapons.Length - 1) currentWeaponIndex = 0;
            else currentWeaponIndex++;
        }
        weapons[currentWeaponIndex].gameObject.SetActive(true);
        OnWeaponChanged?.Invoke(weapons[currentWeaponIndex]);
        FindObjectOfType<ScoreManager>().UpdateAmmo(weapons[currentWeaponIndex].GetComponent<WeaponAmmo>());
    }

    public void WeaponPutAway()
    {
        ChangeWeapon(actions.Default.scrolDirection);
    }

    public void WeaponPulledOut()
    {
        actions.SwitchState(actions.Default);
    }
}
