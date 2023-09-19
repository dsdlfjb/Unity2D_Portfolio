// 적 캐릭터를 정의
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyState
{
    Chase,
    Attack,
    Die,
}
public class Enemy : MonoBehaviour
{
    // 추격할 대상의 transform
    public Transform _playerTrnsf;
    public RuntimeAnimatorController[] _animCon;
    Rigidbody2D _rb;
    Collider2D _coll;

    public float _hp;
    public float _maxHp;
    public float _speed;
    public float _attackDelay = 1;

    public GameObject _itemCoin;
    //public GameObject _itemCooper;
    public GameObject _itemMag;
    public GameObject _damageText;
    public Transform _hudPos;

    Dictionary<EEnemyState, EnemyState> _states = new Dictionary<EEnemyState, EnemyState>();

    Animator _anim;
    SpriteRenderer _spriter;
    WaitForFixedUpdate _wait;      // 다음 fixedUpdate까지 기다림
    // 현재 스테이트를 나타냄
    EEnemyState _eState;

    public bool _isLive;

    private void Awake()
    {
        _playerTrnsf = FindObjectOfType<PlayerCtrl>().transform;
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
        _anim = GetComponent<Animator>();
        _spriter = GetComponent<SpriteRenderer>();
        _wait = new WaitForFixedUpdate();

        // State들을 Dictionary 에 추가.
        _states.Add(EEnemyState.Chase, new EnemyChaseState());
        _states.Add(EEnemyState.Attack, new EnemyAttackState());
        _states.Add(EEnemyState.Die, new EnemyDieState());

        // 기본값을 추적으로 변경
        ChangeState(EEnemyState.Chase);
    }
    private void Update()
    {
        if (!GameManager.Instance._isLive)
            return;

        // 사망 스테이트로 변경
        if (_hp <= 0) ChangeState(EEnemyState.Die);

        _states[_eState].Execute(this);
    }

    private void OnEnable()
    {
        _playerTrnsf = GameManager.Instance._player.GetComponent<Transform>();
        _isLive = true;
        _coll.enabled = true;
        _rb.simulated = true;
        _spriter.sortingOrder = 1;
        _anim.SetBool("IsDie", false);
        _hp = _maxHp;
    }

    public void Init(SpawnData data)
    {
        _anim.runtimeAnimatorController = _animCon[data.spriteType];
        _speed = data.speed;
        _hp = data.hp;
        _maxHp = data.hp;
    }

    // 플레이어로 이동하는 함수
    public void MoveToPlayer(Vector3 dir)
    {
        if (!_isLive || _anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))      // 현재 상태 정보를 가져오는 함수
            return;

        _anim.SetBool("IsWalk", true);
        _rb.velocity = dir * _speed;
        _spriter.flipX = _playerTrnsf.position.x < _rb.position.x;
    }

    public void AttackToPlayer()
    {
        _anim.SetTrigger("Attack");

    }

    public void ChangeState(EEnemyState stateType)
    {
        // state에서 탈출
        _states[_eState].Exit(this);

        // state 변경
        _eState = stateType;

        // state 입장
        _states[_eState].Enter(this);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !_isLive)
            return;

        _hp -= collision.GetComponent<Bullet>()._damage;
        //StartCoroutine(Coroutine_KnockBack());

        if (_hp > 0)
        {
            _anim.SetTrigger("Hit");
            AudioManager.Instance.PlaySFX(AudioManager.ESfx.Hit);
        }

        if (_hp <= 0)
        {
            _isLive = false;
            _coll.enabled = false;
            _rb.simulated = false;
            _spriter.sortingOrder = 0;
            _anim.SetBool("IsDie", true);

            // 랜덤 확률로 아이템 드랍
            int rnd = Random.Range(0, 10);
            if (rnd < 4)        // No 아이템 40%
            {
                Debug.Log("No Item");
            }

            else if (rnd < 8)       // 코인 아이템 40%
            {
                Instantiate(_itemCoin, transform.position, _itemCoin.transform.rotation);
            }

            /*
            else if (rnd < 8)       // 구리코인 아이템 30%
            {
                Instantiate(_itemCooper, transform.position, _itemCooper.transform.rotation);
            }
            */

            else if (rnd < 10)      // 자석 아이템 20%
            {
                Instantiate(_itemMag, transform.position, _itemMag.transform.rotation);
            }

            GameManager.Instance._killCount++;
            GameManager.Instance.GetExp();
            UIManager.Instance.EXP_UP();

            if (GameManager.Instance._isLive)
                AudioManager.Instance.PlaySFX(AudioManager.ESfx.Dead);
        }
    }

    /*
    IEnumerator Coroutine_KnockBack()
    {
        yield return _wait;     // 다음 하나의 물리 프레임까지 딜레이

        Vector3 playerPos = GameManager.Instance._player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        _rb.AddForce(dirVec.normalized * 3f, ForceMode2D.Impulse);
    }
    */

    public void TakeDamage(float damage)
    {
        GameObject hudText = Instantiate(_damageText);
        hudText.transform.position = _hudPos.position;
        hudText.GetComponent<DamageText>()._damage = damage;
    }

    public void Dead()
    {
        gameObject.SetActive(false);

    }
}