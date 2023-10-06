using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDatabase : MonoBehaviour
{
    public static SkinDatabase Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<SwordData> _skinDB = new List<SwordData>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
