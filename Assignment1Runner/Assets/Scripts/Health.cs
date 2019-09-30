using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Image Bar;
    public float fill;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        fill = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool lowerHealth(float decrease)
    {

        Debug.Log("Fill at"+Bar.fillAmount);
        if (Bar.fillAmount == 0.0f)
        {
            Debug.Log("You died and ran out of health");
            GetComponent<Timer>().OnDeath();
            isDead = true;
        }
        else
        {
            Debug.Log("I hit something");
            fill -= decrease;
            Bar.fillAmount = fill;
            isDead = false;
        }
        return isDead;
    }
    public void raiseHealth(float increase)
    {
        if (fill != 1f)
        {
            fill += increase;
            Bar.fillAmount = fill;
        }
    }


}
