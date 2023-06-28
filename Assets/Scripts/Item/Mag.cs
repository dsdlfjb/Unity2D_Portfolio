using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : MonoBehaviour, IFieldDropItem
{
    Vector2 _newPos;
    Transform _trns;

    public float _magnetStrength = 5f;  // �ڼ��� ����
    public float _distStretch = 10f;    // �Ÿ��� ���� �ڼ�ȿ�� ����
    public int _magnetDir = 1;      // �η� 1 ô�� -1
    public bool _looseMagnet = true;

    Transform _trnsf;
    Transform _magnetTrnsf;
    Rigidbody2D _rb;
    bool _magnetInZone;

    private void Awake()
    {
        _trnsf = transform;
        _rb = _trnsf.GetComponent<Rigidbody2D>();
        _trns = transform;
    }

    private void FixedUpdate()
    {
        if (_magnetInZone)
        {
            Vector2 directionToMagnet = _magnetTrnsf.position - _trnsf.position;        // �ڼ����� ���ϴ� ���� ����
            float dist = Vector2.Distance(_magnetTrnsf.position, _trnsf.position);      // Distance�� �Ÿ� a, b�� ����
            float magnetDistStr = (_distStretch / dist) * _magnetStrength;              // �Ÿ��� ���� ���� �޶������ϴϱ� �Ÿ��� ����
            _rb.AddForce(magnetDistStr * (directionToMagnet * _magnetDir), ForceMode2D.Force);      // ���� ũ��� ������ �����ϱ� ������ �� ����
        }
    }

    public void Use()
    {
        _trns.position = Vector2.Lerp(_trns.position, _newPos, Time.deltaTime * 1.5f);

        if (Mathf.Abs(_newPos.x - _trns.position.x) < 0.05)
            _trns.position = _newPos;

    }

    public void DestroyGameObject()
    {
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Use();
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Magnet"))
        {
            _magnetTrnsf = collision.gameObject.transform;
            _magnetInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Magnet") && _looseMagnet)
        {
            _magnetInZone = false;
        }
    }
}
