using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float _moveSpeed;
    public float _alphaSpeed;
    public float _destroyTime;
    public float _damage;

    TextMeshPro _text;
    Color _alpha;

    // Start is called before the first frame update
    void Start()
    {
        _damage = Random.Range(1, 11);
        _text = GetComponent<TextMeshPro>();
        _text.text = _damage.ToString();
        _alpha = _text.color;
        Invoke("DestroyObject", _destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, _moveSpeed * Time.deltaTime, 0));
        _alpha.a = Mathf.Lerp(_alpha.a, 0, Time.deltaTime * _alphaSpeed);
        _text.color = _alpha;
    }

    void DestroyObject() { Destroy(gameObject); }
}
