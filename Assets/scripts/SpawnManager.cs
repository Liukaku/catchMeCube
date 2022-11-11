using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public Vector2 spawnRangeX;
    public Vector2 spawnRangeZ;
    public int spawnHeight;

    private int m_enemyCount;

    private int m_waveCount = 1;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        m_enemyCount = FindObjectsOfType<EnemyController>().Length;

        if (m_enemyCount == 0)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        for(var i = 0; i < m_waveCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
            Random.Range(spawnRangeX[0], spawnRangeX[1]),
            spawnHeight,
            Random.Range(spawnRangeZ[0], spawnRangeZ[1])
    );

            Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
        }


        m_waveCount++;

        Debug.Log(m_waveCount);
    }
}
