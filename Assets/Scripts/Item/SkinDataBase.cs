using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDataBase : MonoBehaviour
{
    public static SkinDataBase Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Skin> skinDB = new List<Skin>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
