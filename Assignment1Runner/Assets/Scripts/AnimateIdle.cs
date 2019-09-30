using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateIdle : MonoBehaviour
{


    public Animator idleChillMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        idleChillMenu.SetInteger("condition",1);
    }
}
