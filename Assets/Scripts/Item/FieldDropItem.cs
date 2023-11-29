using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDropItem : MonoBehaviour
{
    public string _type;
    public int _score;
    public Rigidbody2D _rb;
    public bool _isMagnetOn;

    void OnEnable()
    {
        _isMagnetOn = false;
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MagOn();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_isMagnetOn)
            return;

        if (other.gameObject.tag == "Mag" && !_isMagnetOn)
        {
            isMagOn();
        }

        
    }

    void MagOn()
    {
        if (!_isMagnetOn)
            return;

        Vector3 nextPos = GameManager.Instance._player.transform.position - transform.position;
        _rb.velocity = nextPos * 10;
    }

    public void isMagOn()
    {
        _isMagnetOn = true;
    }

    public void ActiveOff()
    {
        gameObject.SetActive(false);
    }
}
