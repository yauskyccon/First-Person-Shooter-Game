using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public GameObject playerWeapon;
    private WeaponAmmo ammo;

    // Start is called before the first frame update
    void Start()
    {
        ammo = playerWeapon.GetComponent<WeaponAmmo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.Play("Gain");
            Destroy(gameObject);
            ammo.extraAmmo += 30;
        }
    }
}
