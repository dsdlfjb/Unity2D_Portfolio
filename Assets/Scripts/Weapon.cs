using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int _id;
    public int _prefId;
    public float _damage;
    public int _count;
    public float _speed;

    float _timer;
    PlayerCtrl _player;

    private void Awake()
    {
        _player = GameManager.Instance._player;
    }

    void Update()
    {
        if (!GameManager.Instance._isLive)
            return;

        switch (_id)
        {
            case 0:
                transform.Rotate(Vector3.back * _speed * Time.deltaTime);
                break;

            case 1:
                _timer += Time.deltaTime;

                if (_timer > _speed)
                {
                    _timer = 0;
                    Fire();
                }
                break;

            case 2:
                _timer += Time.deltaTime;

                if (_timer > _speed)
                    _timer = 0;
                break;

            default:
                break;
        }
    }

    public void Init(ItemData data)
    {
        name = "Weapon " + data._itemId;
        transform.parent = _player.transform;   //부모 player의 자식으로 지정 
        transform.localPosition = Vector3.zero;

        _id = data._itemId;
        _damage = data._baseDamage;
        _count = data._baseCount;

        for (int index = 0; index < GameManager.Instance._pool._pref.Length; index++)
        {
            if (data._projectile == GameManager.Instance._pool._pref[index])
            {
                _prefId = index;
                break;
            }
        }

        switch(_id)
        {
            case 0:
                _speed = 150;
                Batch();
                break;

            case 1:
                _speed = 0.3f;
                break;

            case 2:
                _speed = 50;
                Batch();
                break;

            default:
                break;
        }

        _player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);  // "ApplyGear" 함수를 모든 자식들에게 방송?하는 함수
                                                                                        // DontRequireReceiver : 꼭 리시버가 필요하진 않다
    }

    public void LevelUp(float damage, int count)
    {
        this._damage = damage;
        this._count += count;

        if (_id == 0)
            Batch();

        _player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); 
    }

    void Batch()
    {
        for (int index = 0; index < _count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.Instance._pool.Get(_prefId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / _count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(_damage, -100, Vector3.zero);    // -100 is Infinity Per.
        }
    }

    void Fire()
    {
        if (!_player._scanner._nearestTarget)
            return;

        Vector3 targetPos = _player._scanner._nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        Transform bullet = GameManager.Instance._pool.Get(_prefId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(_damage, _count, dir);
    }
}
