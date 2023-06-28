using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _damage;
    public int _per;

    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this._damage = damage;
        this._per = per;

        if (per >= 0)
        {
            _rb.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || _per == -100) return;
            
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Enemy>().TakeDamage(_damage);

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
