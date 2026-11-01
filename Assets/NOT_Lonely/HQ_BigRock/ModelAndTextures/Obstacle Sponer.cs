using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("生成設定")]
    public GameObject obstaclePrefab; // 放入你的障礙物 Prefab
    public Transform player;          // 放入你的飛機

    public float spawnTime = 1.5f;    // 每隔幾秒生成一次
    public float spawnDistance = 150f;// 生成在飛機前方多遠的距離
    public float randomX = 30f;       // 左右隨機偏移的範圍
    public float randomY = 15f;       // 上下隨機偏移的範圍

    void Start()
    {
        // 遊戲開始時，啟動不斷生成的協程 (Coroutine)
        StartCoroutine(SpawnObstacleRoutine());
    }

    IEnumerator SpawnObstacleRoutine()
    {
        while (true) // 無限循環
        {
            if (player != null)
            {
                // 1. 計算基準生成位置：飛機當前位置 + 往飛機正前方推 spawnDistance 的距離
                Vector3 spawnPos = player.position + player.forward * spawnDistance;

                // 2. 加入隨機偏移量，讓障礙物散布在不同位置，避免排成一直線
                spawnPos += player.right * Random.Range(-randomX, randomX);
                spawnPos += player.up * Random.Range(-randomY, randomY);

                // 3. 生成障礙物 (Quaternion.identity 代表不旋轉，維持預設角度)
                GameObject newObstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

                // 4. 記憶體管理：設定 10 秒後自動銷毀已經生成的障礙物，避免飛機飛過後，後方殘留過多物件導致遊戲卡頓
                Destroy(newObstacle, 10f);
            }

            // 等待指定的秒數後，再執行下一次迴圈
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
