using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AudioManager.instance.Play("Gain");
            Destroy(gameObject);
            ScoreManager.scoreCount += 1;
        }
    }
}
