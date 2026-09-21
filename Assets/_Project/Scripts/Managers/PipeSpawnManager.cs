using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject PrefabedPipe, player;
    [SerializeField] private float SpawnMaxDelay = 1.2f;

    private float _timer = 0.4f;

    private void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("PipeSpawnManager: Player referansı atanmadı!");
            return;
        }

        if (!player.TryGetComponent<BirdController>(out var bc))
        {
            Debug.LogWarning("PipeSpawnManager: Player üzerinde BirdController yok!");
            return;
        }
        else
        {
            if (!bc.Isdied() && bc.Isflying())
                PipeSpawner();
        }

        
    }

    private void PipeSpawner()
    {
        if (_timer <= 0f)
        {
            _timer = Random.Range(SpawnMaxDelay / 2, SpawnMaxDelay);
            PipeSpawn();
        }
        else
            _timer -= Time.deltaTime;
    }

    private void PipeSpawn()
    {
        float Ypos = Random.Range(-3.05f, -0.75f);
        GameObject obj = Instantiate(PrefabedPipe);
        obj.transform.position = new Vector3(transform.position.x, Ypos, 0);
    }
}