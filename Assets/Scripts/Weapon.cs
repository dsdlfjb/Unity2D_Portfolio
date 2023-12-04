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
    public Sprite _swordSprite; // 검 스킨
    public Sprite _bulletSprite; // 총알 스킨

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

            default:
                break;
        }
    }

    public void Init(ItemData data)
    {
        // 리소스 폴더에서 현재 스킨 이름에 맞는 스프라이트를 찾아와서 적용
        //_swordSprite = Resources.Load<Sprite>(DataManager.Instance._swordSkinName);
        _swordSprite = Managers.Resource.GetSprite(Enum.ESpriteKey.Sword_1 + Managers.Save._saveData._equippedSwordIndex);
        _bulletSprite = Resources.Load<Sprite>(DataManager.Instance._bulletSkinName);

        name = "Weapon " + data._itemId;
        transform.parent = _player.transform;   //부모 player의 자식으로 지정 
        transform.localPosition = Vector3.zero;

        _id = data._itemId;

        if(_swordSprite.name == "Basic Sword")
        {
            _damage = data._baseDamage;           
        }

        else if(_swordSprite.name == "Sword00")
        {
            _damage = 5;
        }

        else if (_swordSprite.name == "Sword01")
        {
            _damage = 7;
        }

        else if (_swordSprite.name == "Sword02")
        {
            _damage = 10;
        }

        else if (_swordSprite.name == "Sword03")
        {
            _damage = 13;
        }

        else if (_swordSprite.name == "Sword04")
        {
            _damage = 15;
        }

        else if (_swordSprite.name == "Sword05")
        {
            _damage = 18;
        }

        else if (_swordSprite.name == "Sword06")
        {
            _damage = 20;
        }

        else if (_swordSprite.name == "Sword07")
        {
            _damage = 22;
        }

        else if (_swordSprite.name == "Sword08")
        {
            _damage = 25;
        }

        else if (_swordSprite.name == "Sword09")
        {
            _damage = 27;
        }

        else if (_swordSprite.name == "Sword10")
        {
            _damage = 30;
        }

        else if (_swordSprite.name == "Sword11")
        {
            _damage = 35;
        }   

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

            // 생성될 무기에 스킨 전달
            bullet.GetComponent<Bullet>().Init(_damage, -100, Vector3.zero, _swordSprite);    // -100 is Infinity Per.
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

        // 발사하려는 무기에도 같은 방식으로 스킨 전달해야 함
        bullet.GetComponent<Bullet>().Init(_damage, _count, dir, _bulletSprite);

        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Range);
    }
}
