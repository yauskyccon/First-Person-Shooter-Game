using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    private float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    public float damage = 20;
    AimStateManager aim;

    [SerializeField] AudioClip gunShot;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public WeaponAmmo ammo;
    WeaponBloom bloom;
    ActionStateManager actions;
    WeaponRecoil recoil;

    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticles;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20;

    public Transform leftHandTarget, leftHandHint;
    WeaponClassManager weaponClass;

    // Start is called before the first frame update
    void Start()
    {
        aim = GetComponentInParent<AimStateManager>();
        bloom = GetComponent<WeaponBloom>();
        actions = GetComponentInParent<ActionStateManager>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        muzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
        fireRateTimer = fireRate;
    }

    private void OnEnable()
    {
        if(weaponClass == null)
        {
            weaponClass = GetComponentInParent<WeaponClassManager>();
            ammo = GetComponent<WeaponAmmo>();
            audioSource = GetComponent<AudioSource>();
            recoil = GetComponent<WeaponRecoil>();
            recoil.recoilFollowPos = weaponClass.recoilFollowPos;
        }

        weaponClass.SetCurrentWeapon(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire()) Fire();

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            muzzleFlashParticles.Stop();
        }

        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.currentAmmo == 0) return false;
        if (actions.currentState == actions.Reload) return false;
        if (actions.currentState == actions.Swap) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse1)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse1)) return true;
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        ammo.currentAmmo--;

        barrelPos.LookAt(aim.aimPos);
        barrelPos.localEulerAngles = bloom.BloomAngle(barrelPos);

        audioSource.PlayOneShot(gunShot);
        recoil.TriggerRecoil();
        TriggerMuzzleFlash();
        
        for (int i = 0; i < bulletPerShot; i++) 
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);

            Bullet bulletScript = currentBullet.GetComponent<Bullet>();
            bulletScript.weapon = this;

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    void TriggerMuzzleFlash()
    {
        muzzleFlashParticles.Play();
        muzzleFlashLight.intensity = lightIntensity;
    }
}
