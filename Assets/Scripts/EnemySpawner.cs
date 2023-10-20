using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int SPAWN_MAX = 30;       // 최소 몬스터 제한 수
    private const int SPAWN_INCREASE = 10;  // 스테이지 올라갈 때 마다 증가하는 몬스터 수     ex) 2스테이지는 30 + 10 = 40마리

    public Transform[] _spawnPoint;
    public SpawnData[] _spawnData;
    public float _levelTime;
    int _level;
    float _timer;

    int _spawnCounter = 0;
    int _enemyDieCounter = 0;

    private void Awake()
    {
        _spawnPoint = GetComponentsInChildren<Transform>();
        _levelTime = GameManager.Instance._maxGameTime / _spawnData.Length;

        if (Managers.Instance != null)
        {
            Managers.Event.RegistEvent(Enum.EEventKey.OnEnemyDie, OnEnemyDie);
        }
    }

    private void OnDestroy()
    {
        if (Managers.Instance != null)
        {
            Managers.Event.RemoveEvent(Enum.EEventKey.OnEnemyDie, OnEnemyDie);
        }
    }

    private void Update()
    {
        if (!GameManager.Instance._isLive) return;

        _timer += Time.deltaTime;
        _level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance._gameTime / 10f), _spawnData.Length - 1);
        _level = Mathf.Min(_level, Managers.Instance.CurrentStageLevel - 1);

        if (SPAWN_MAX + ((Managers.Instance.CurrentStageLevel - 1) * SPAWN_INCREASE) > _spawnCounter && _timer > _spawnData[_level].spawnTime)
        {
            _timer = 0;
            Spawn(false);
        }
    }

    void Spawn(bool isBoss)
    {
        GameObject enemy = GameManager.Instance._pool.Get(0);

        if (isBoss)
        {
            enemy.GetComponent<Enemy>().Init(_spawnData[_spawnData.Length - 1]);
        }
        else
        {
            _spawnCounter++;
            enemy.GetComponent<Enemy>().Init(_spawnData[_level]);
        }

        enemy.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;
    }


    private void OnEnemyDie(object enemyType)
    {
        EEnemyType type = (EEnemyType)enemyType;

        if (type == EEnemyType.Normal)
        {
            _enemyDieCounter++;

            if (_enemyDieCounter >= SPAWN_MAX + ((Managers.Instance.CurrentStageLevel - 1) * SPAWN_INCREASE))
            {
                this.Spawn(true);
            }
        }
    }
}

[System.Serializable]
public class SpawnData
{
    public EEnemyType enemyType;
    public int spriteType;
    public float spawnTime;
    public int hp;
    public float speed;
}