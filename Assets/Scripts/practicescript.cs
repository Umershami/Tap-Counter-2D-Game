using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practicescript : MonoBehaviour
{
    public Dictionary<string,int> leveldic=new Dictionary< string,int >();
    void Start()
    {
        leveldic ["umer"] = 1;
        leveldic["shami"] = 2;
        leveldic["Muhammad"] = 3;
      Console.WriteLine(leveldic["umer"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
