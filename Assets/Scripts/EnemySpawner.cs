using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] _spawnPoint;
    public SpawnData[] _spawnData;
    public float _levelTime;
    int _level;
    float _timer;

    private void Awake()
    {
        _spawnPoint = GetComponentsInChildren<Transform>();
        _levelTime = GameManager.Instance._maxGameTime / _spawnData.Length;
    }

    private void Update()
    {
        if (!GameManager.Instance._isLive) return;

        _timer += Time.deltaTime;
        _level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance._gameTime / 10f), _spawnData.Length - 1);

        if (_timer > _spawnData[_level].spawnTime)
        {
            _timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Instance._pool.Get(0);
        enemy.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(_spawnData[_level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int hp;
    public float speed;
}