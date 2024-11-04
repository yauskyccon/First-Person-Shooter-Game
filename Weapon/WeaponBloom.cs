using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBloom : MonoBehaviour
{
    [SerializeField] float defaultBloomAngle = 3;
    [SerializeField] float walkBloomMultiplayer = 1.5f;
    [SerializeField] float crouchBloomMultiplayer = 0.5f;
    [SerializeField] float sprintBloomMultiplayer = 2f;
    [SerializeField] float adsBloomMultiplayer = 0.5f;

    MovementStateManager movement;
    AimStateManager aiming;

    float currentBloom;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponentInParent<MovementStateManager>();
        aiming = GetComponentInParent<AimStateManager>();
    }

    public Vector3 BloomAngle (Transform barrelPos)
    {
        if (movement.currentState == movement.Idle) currentBloom = defaultBloomAngle;
        else if (movement.currentState == movement.Walk) currentBloom = defaultBloomAngle * walkBloomMultiplayer;
        else if (movement.currentState == movement.Run) currentBloom = defaultBloomAngle * sprintBloomMultiplayer;
        else if (movement.currentState == movement.Crouch)
        {
            if (movement.dir.magnitude == 0) currentBloom = defaultBloomAngle * crouchBloomMultiplayer;
            else currentBloom = defaultBloomAngle * crouchBloomMultiplayer * walkBloomMultiplayer;
        }

        if (aiming.currentState == aiming.Aim) currentBloom *= adsBloomMultiplayer;

        float randX = Random.Range(-currentBloom, currentBloom);
        float randY = Random.Range(-currentBloom, currentBloom);
        float randZ = Random.Range(-currentBloom, currentBloom);

        Vector3 randomRotation = new Vector3(randX, randY, randZ);
        return barrelPos.localEulerAngles + randomRotation;
    }
}
