using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeathMenu : MonoBehaviour
{
    public Text LevelEnd;
    public Text Countdown;

    //To fade to black
    public Image backGround;
    private bool isShowned = false;
    private float transition= 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //make sure the game menu is turned off
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShowned)
            return;
        //if this is showing then increment
        transition += Time.deltaTime;
        backGround.color = Color.Lerp(new Color(0,0,0,0),Color.black,transition);
    }

    public void ToggleEndMenu(float timeAtDeath, float levelReached)
    {
        gameObject.SetActive(true);
        LevelEnd.text = "Level: "+levelReached.ToString("0");
        Countdown.text = "Time: "+timeAtDeath.ToString("0");
        isShowned = true;
    }

    public void PlayAgain()
    {
        //loading a scene, asking the scene manager what scene we are on
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
