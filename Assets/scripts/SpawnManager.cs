using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    public Vector2 spawnRangeX;
    public Vector2 spawnRangeZ;
    public int spawnHeight;

    private int m_enemyCount;

    private int m_powerUpCount;

    private int m_waveCount = 1;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        m_enemyCount = FindObjectsOfType<EnemyController>().Length;
        m_powerUpCount = FindObjectsOfType<PowerUpManager>().Length;

        if (m_enemyCount == 0)
        {
            SpawnEnemy();
        }

        if (m_powerUpCount == 0)
        {
            StartCoroutine(SpeedCountDown());
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

    private IEnumerator SpeedCountDown()
    {

        yield return new WaitForSeconds(10);

        if (m_powerUpCount == 0)
        {
            SpawnPowerUp();
        }
    }
    void SpawnPowerUp()
    {
        Vector3 spawnPosition = new Vector3(
        Random.Range(spawnRangeX[0], spawnRangeX[1]),
        0.8f,
        Random.Range(spawnRangeZ[0], spawnRangeZ[1])
        );
        Instantiate(powerUpPrefab, spawnPosition, enemyPrefab.transform.rotation);
    }


}
