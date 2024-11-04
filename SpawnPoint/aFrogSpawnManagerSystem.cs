using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aFrogSpawnManagerSystem : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int xPos;
    public int zPos;
    public int enemyCount;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 10)
        {
            xPos = Random.Range(35, 40);
            zPos = Random.Range(35, 40);
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], new Vector3(xPos, 10, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
}
