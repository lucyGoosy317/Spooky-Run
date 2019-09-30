using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveVector;
    public CharacterController controller;
    Animator anime;
    public AudioSource[] sounds;
    private AudioSource bashInto;
    private AudioSource drinkPowerUp;
    public Text[] powerUpDisplays;
    private float gravity=25.0f;
    private float jumpSpeed=15.0f;

    private float speed = 10.0f;

    private bool isDead = false;
    private float timeToApper=1f;
    private float textDisappear;
    private int indexOfPowerUp;
    public DeathMenu deathMenu;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anime = GetComponent<Animator>();
        bashInto = sounds[0];
        drinkPowerUp = sounds[1];
        powerUpDisplays[0].enabled = false;
        powerUpDisplays[1].enabled = false;
        powerUpDisplays[2].enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isDead);


        disableText(indexOfPowerUp);

       // 
        if (moveVector.y < -15)
        {

            
            Debug.Log("Im falling, should be playing sound"+ moveVector.y);
           
            anime.SetInteger("condition", 4);
            isDead = true;
            
            DeathOnFall();

            return;
        }
        if (isDead != true ) { 
           

        if (controller.isGrounded)
        {
            //jumpSpeed = 0.0f;
            moveVector = Vector3.zero;
            anime.SetInteger("condition" ,1);
            moveVector.x = Input.GetAxis("Horizontal") * speed ;
            
                if (Input.GetButton("Jump"))
            {
                    //jumpSpeed = 15.0f;
                //anime.SetInteger("condition", 2);
                moveVector.y = jumpSpeed;
            }
            moveVector.z = speed ;
            
        }
        else
        {
            moveVector.y -= gravity * Time.deltaTime;
        }
        
        controller.Move((moveVector) * Time.deltaTime);
        }
        else
        {
            
            anime.SetInteger("condition", 4);
            isDead = true;
            Death();
            
        }
    }


    public void SetSpeed(int modSpeed)
    {
        speed += modSpeed;//*Time.deltaTime;
    }


    //is being called every time your capsule hits something
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(isDead!=true)
            {
            
            if (hit.gameObject.tag.Equals("Obstacle"))
            {
                bashInto.Play();
                hitDecrease();
                Destroy(hit.gameObject);
             
            }else if(hit.gameObject.tag.Equals("HealthPup")){
                     
                hitIncrease();
                drinkPowerUp.Play();
                Destroy(hit.gameObject);

            }else if (hit.gameObject.tag.Equals("SlowDownTimePup"))
            {
                slowDownTime();
                drinkPowerUp.Play();
                Destroy(hit.gameObject);

            }
            else if (hit.gameObject.tag.Equals("SuperJumpPup"))
            {
            JumpIncrease();
            Destroy(hit.gameObject);
            }

        }
        else
        {
            isDead = true;
            Death();
            
            return;
        }
        
    }
    
    private void Death()
    {
        
        Debug.Log("I'm Dead");
        isDead = true;
        //moveVector = Vector3.zero;
        //anime.SetInteger("condition", 4);
        GetComponent<Timer>().OnDeath();
        
        return;
    }

    private void hitDecrease()
    {
        float hit = 0.1f;
        Debug.Log("lowering health");
        if (GetComponent<Health>().lowerHealth(hit) == true)
        {
            isDead = true;
        }
    }

    private void hitIncrease()
    {
        float healthUp = 0.1f;
        Debug.Log("raise health");
        GetComponent<Health>().raiseHealth(healthUp);
        indexOfPowerUp = showPowerUp(0);
    }

    private void JumpIncrease()
    {
        if (jumpSpeed !=20)
        {
            jumpSpeed++;
            string noJumpIncrease = "Jump Increase";
            powerUpDisplays[1].text = noJumpIncrease.ToString();
            indexOfPowerUp = showPowerUp(1);
            
        }
        else
        {
            string noJumpIncrease = "Max Jump";
            powerUpDisplays[1].text = noJumpIncrease.ToString();
            indexOfPowerUp = showPowerUp(1);
            
        }

        
        
    }

    private void slowDownTime()
    {
        if (speed != 10f)
        {
            string SpeedIncrease = "Speed decrease";
            powerUpDisplays[2].text = SpeedIncrease.ToString();
            indexOfPowerUp = showPowerUp(2);

            speed -= 0.1f;
        }
        else
        {
            string noSpeedIncrease = "No Speed decrease";
            powerUpDisplays[2].text = noSpeedIncrease.ToString();
            indexOfPowerUp = showPowerUp(2);
            
            return;
        }
    }

    private void DeathOnFall()
    {
        
        Debug.Log("I fell");
        isDead = true;
        GetComponent<Timer>().OnDeath();
    }

    public int showPowerUp(int hit)
    {
        
         if (hit==0)
        {

            powerUpDisplays[hit].enabled = true;
            textDisappear = Time.time + timeToApper;

        }
        else if (hit==1)
        {
            powerUpDisplays[hit].enabled = true;
            textDisappear = Time.time + timeToApper;
        }
        else if (hit==2)
        {
            powerUpDisplays[hit].enabled = true;
            textDisappear = Time.time + timeToApper;
        }
        return hit;
    }

    private void disableText(int index)
    {
        if (powerUpDisplays[index].enabled && (Time.time >= textDisappear ))
        {
            powerUpDisplays[index].enabled = false;
        }


    }
    

}
