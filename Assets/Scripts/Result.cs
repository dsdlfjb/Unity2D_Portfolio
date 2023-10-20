using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] _titles;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void Lose()
    {
        _titles[0].SetActive(true);
        _titles[1].SetActive(false);
    }

    public void Win()
    {
        _titles[0].SetActive(false);
        _titles[1].SetActive(true);
    }
}
