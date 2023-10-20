using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _damage;
    public int _per;

    Rigidbody2D _rb;
    SpriteRenderer _sr;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    public void Init(float damage, int per, Vector3 dir, Sprite sprite)
    {
        this._damage = damage;
        this._per = per;
        this._sr.sprite = sprite; // 스킨 적용

        if (per >= 0)
        {
            _rb.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || _per == -100) return; 

        _per--;

        if (_per < 0)
        {
            _rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || _per == -100)
            return;

        gameObject.SetActive(false);
    }
}
