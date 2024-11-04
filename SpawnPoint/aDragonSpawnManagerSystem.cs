using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aDragonSpawnManagerSystem : MonoBehaviour
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
        while (enemyCount < 4)
        {
            xPos = Random.Range(42, 52);
            zPos = Random.Range(35, 45);
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], new Vector3(xPos, 5, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }
}
