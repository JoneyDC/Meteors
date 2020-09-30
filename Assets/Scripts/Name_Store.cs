using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name_Store : MonoBehaviour
{
    public string Name;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


}
