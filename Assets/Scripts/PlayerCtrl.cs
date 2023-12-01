using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] VirtualJoystickCtrl _joystick;
    [SerializeField] Transform _player;

    public float _moveSpeed;

    Vector3 _moveDir;
    public Vector3 _nextPos;
    public Scanner _scanner;
    public GameObject blueThun;
    public float thunDuration = 5f;
    private float durationTime = 0f;
    private bool isThun = false;

    SpriteRenderer _renderer;
    Animator _anim;
    Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _moveDir = Vector3.zero;
        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _scanner = GetComponent<Scanner>();

        blueThun = GameObject.FindWithTag("BlueThun");
        blueThun.gameObject.SetActive(false);

        if (Managers.Save._saveData.GetUpgradeSkillData(4)._isPurchased)
        {
            thunDuration = 9;
        }

        else if (Managers.Save._saveData.GetUpgradeSkillData(3)._isPurchased)
        {
            thunDuration = 8;
        }

        else if (Managers.Save._saveData.GetUpgradeSkillData(2)._isPurchased)
        {
            thunDuration = 7;
        }

        else if (Managers.Save._saveData.GetUpgradeSkillData(1)._isPurchased)
        {
            thunDuration = 6;
        }

        else
        {
            thunDuration = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance._isLive)
            return;

        BodyMove();
        Show_HPBar();

        if(isThun)
        {
            UseSkill();
        }
    }

    public void BodyMove()
    {
        if (!(_joystick._InputDir.x == 0 && _joystick._InputDir.y == 0))
        {
            Vector2 moveDir = new Vector2(_joystick._InputDir.x, _joystick._InputDir.y);
            _moveDir = moveDir;

            _nextPos = new Vector3(_joystick._InputDir.x, _joystick._InputDir.y, -1);
        }

        if (_joystick._InputDir.x == 0 && _joystick._InputDir.y == 0)
            _rb.velocity = Vector2.zero;

        if (_joystick._InputDir.x < 0)
        {
            _renderer.flipX = true;
            _anim.SetBool("IsMove", true);
        }

        else if (_joystick._InputDir.x > 0)
        {
            _renderer.flipX = false;
            _anim.SetBool("IsMove", true);
        }

        else
            _anim.SetBool("IsMove", false);

        _moveDir = Vector3.Lerp(_moveDir, Vector3.zero, Time.deltaTime * 3f);
        _player.Translate(_moveDir * Time.deltaTime * _moveSpeed, Space.World);
    }

    public void Show_HPBar()
    {
        UIManager.Instance._hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.15f, 0.3f,0));
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            FieldDropItem item = other.GetComponent<FieldDropItem>();
            if (item != null)
            {
                switch (item._type)
                {
                    case "Coin":
                        DataManager.Instance.AddCoin(item._score);
                        break;

                    case "Mag":
                        // 자석 구현
                        List<GameObject> coinItem = GameManager.Instance._pool._pools[3];
                        for (int i = 0; i < coinItem.Count; i++)
                        {
                            FieldDropItem coinItemLogic = coinItem[i].GetComponent<FieldDropItem>();

                            if (coinItemLogic._type == "Coin")
                            {
                                coinItemLogic.isMagOn();
                            }
                        }
                        break;

                    case "Thunder":
                        isThun = true;
                        durationTime = thunDuration;
                        break;
                }
            }

            item.ActiveOff();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance._isLive)
            return;

        if (collision.collider.CompareTag("Enemy"))
        {
            GameManager.Instance._curHp -= Time.deltaTime * 10;

            if (GameManager.Instance._curHp < 0)
            {
                for (int index = 1; index < transform.childCount; index++)
                {
                    transform.GetChild(index).gameObject.SetActive(false);
                }

                _anim.SetTrigger("Die");
                GameManager.Instance.GameOver();
            }
        }
    }


    public void UseSkill()
    {
        blueThun.gameObject.SetActive(true);

        if (0<durationTime)
        {
            durationTime = durationTime - Time.deltaTime;
            //Debug.Log(durationTime);
        }

        else
        {
            blueThun.gameObject.SetActive(false);
            isThun = false;
        }
    }
}
