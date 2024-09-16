using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float RangoX1 = 0;
    public float RangoX2 = 0;
    public float RangoY1 = 0;
    public float RangoY2 = 0;

    public GameObject EnemyPrefab;

    public float Onigiri = 15;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(Onigiri, EnemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector2(Random.Range(RangoX1, RangoX2), Random.Range(RangoY1, RangoY2)), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}