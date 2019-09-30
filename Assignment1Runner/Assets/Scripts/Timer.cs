using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static int levelCounter = 0;
    public static float currentTime=0f;
    public float startingTime;
    public Text countDownText;
    public Text LevelCounter;

    private bool isDead = false;

    private int difficultLevel=1;

    private float textDisappear;
    private float timeToAppear = 1f;
    public Text levelUpMessage;

    public DeathMenu deathMenu;
    // Start is called before the first frame update
    void Start()
    {
        levelCounter = 1;
        startingTime = 30f;
        currentTime = startingTime;
        LevelCounter.text= currentTime.ToString("0");
        levelUpMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        disableText();
        if (isDead)
        {
            return;
        }
        currentTime -= 1 * Time.deltaTime;

        countDownText.text = currentTime.ToString("0");
        LevelCounter.text = "Level: " + levelCounter.ToString("0");
        if (currentTime <= 0)
        {

            currentTime = startingTime;
            
            //countDownText.text = "You Win Yay!";
            levelCounter++;
            LevelCounter.text = "Level: " + levelCounter.ToString();
            LevelUp();

        }
    }

    public void LevelUp()
    {
        difficultLevel++;
        displayLevelAlert();
        GetComponent<PlayerController>().SetSpeed(difficultLevel);
        Debug.Log("Level: "+difficultLevel);
    }

    public void OnDeath()
    {
        isDead = true;
        
        deathMenu.ToggleEndMenu(currentTime, levelCounter);
    }

    public void displayLevelAlert()
    {
        levelUpMessage.enabled = true;
        textDisappear = Time.time + timeToAppear;
    }

    public void disableText()
    {
        if (levelUpMessage.enabled && (Time.time >= textDisappear))
        {
            levelUpMessage.enabled = false;
        }
    }
}
