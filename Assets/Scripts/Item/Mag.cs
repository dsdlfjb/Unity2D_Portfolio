using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : MonoBehaviour, IFieldDropItem
{
    Vector2 _newPos;
    Transform _trns;

    public float _magnetStrength = 5f;  // 자석의 세기
    public float _distStretch = 10f;    // 거리에 따른 자석효과 적용
    public int _magnetDir = 1;      // 인력 1 척력 -1
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
            Vector2 directionToMagnet = _magnetTrnsf.position - _trnsf.position;        // 자석으로 향하는 벡터 설정
            float dist = Vector2.Distance(_magnetTrnsf.position, _trnsf.position);      // Distance로 거리 a, b를 구함
            float magnetDistStr = (_distStretch / dist) * _magnetStrength;              // 거리에 따른 힘이 달라져야하니까 거리로 나눔
            _rb.AddForce(magnetDistStr * (directionToMagnet * _magnetDir), ForceMode2D.Force);      // 힘의 크기와 방향이 있으니까 물리적 힘 구현
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
