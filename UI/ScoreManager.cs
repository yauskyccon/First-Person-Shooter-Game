using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text currentAmmoText;
    public Text extraAmmoText;
    public static int scoreCount;

    public GameObject weapon;

    private WeaponAmmo currentAmmo;

    // Start is called before the first frame update
    /*void Start()
    {

        ammo = weapon.GetComponent<WeaponAmmo>();

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Diamond x " + Mathf.Round(scoreCount);
        currentAmmoText.text = "Current Ammo:  " + Mathf.Round(ammo.currentAmmo);
        extraAmmoText.text = "Extra Ammo:  " + Mathf.Round(ammo.extraAmmo);
    }*/

    public void UpdateAmmo(WeaponAmmo ammo)
    {
        currentAmmo = ammo;
        UpdateUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (currentAmmo != null)
        {
            UpdateUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentAmmo != null)
        {
            scoreText.text = "Diamond x " + Mathf.Round(scoreCount);
            currentAmmoText.text = "Current Ammo:  " + Mathf.Round(currentAmmo.currentAmmo);
            extraAmmoText.text = "Extra Ammo:  " + Mathf.Round(currentAmmo.extraAmmo);
        }
    }
}
