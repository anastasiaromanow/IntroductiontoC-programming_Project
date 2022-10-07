using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float health;
    private float lerpTimer; //Steer interpolation life/damage(?)
    [Header ("Health Bar")]
    public float maxHealth = 100f;

    public static bool isGameOver;
    public float chipSpeed = 2f; //"chipping away"- Speed of interpolation
    public Image frontHealthBar; //shows health
    public Image backHealthBar;//shows damage taken "in" "chip"
    [Header ("Damage Overlay")]
    public Image overlay; //DamageOverlay Gameobject
    public float duration; //how long image stays fully opaque
    public float fadeSpeed; //controls how quickly image will fade

    private float durationTimer; //timer to check against duration

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if (overlay != null)
        {
            overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b, 0); //clear damageoverlay
        }
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0f, maxHealth); //0-100 ratio.
        UpdateHealthUI();

        if (overlay == null)
        {
            return;
        }
        else 
        {
        if(overlay.color.a > 0) //if alpha of overlay is greater than 0
        {
            if(health < 30)
                return; //pause durationtimer and will never start the beneath fade-process
            durationTimer += Time.deltaTime;
            if(durationTimer > duration) //if durationTimer has been counting longer than duration value
            {//fade the image 
                float tempAlpha = overlay.color.a; //temp value gets set to the alpha of the image
                tempAlpha -= Time.deltaTime * fadeSpeed; //decrement temp value 
                overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b, tempAlpha); //assigning back to the alpha channel

            }
        }
        }
    }

    public void UpdateHealthUI()
    {
        if(frontHealthBar == null && backHealthBar == null)
        {
            return;
        }
        else
        {

        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount; 
        float hFraction = health / maxHealth;  //This will keep the value of health fraction between 0 and 1. When damaged, health value drops. As result;hfraction drops. sum/total.
        if(fillB > hFraction)//if true = damage taken
        {
            frontHealthBar.fillAmount = hFraction; //?
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime; //Time increment of interpolation
            float percentComplete = lerpTimer / chipSpeed; //a local variable to track the completion(avslut,fulländning) of the lerp
            percentComplete = percentComplete * percentComplete; // Square value for an easing-effect. Animation starts slow and speeds up as reaching completion
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);

            //"chase" of the front healthbar
        }
        if(fillF < hFraction) // if healthvalue is greater than fillfront
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete; // Square value for an easing-effect
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        }
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        lerpTimer = 0f;//Reset interpolation
        durationTimer = 0; //when taken damage, the durationTimer resets
        overlay.color = new Color(overlay.color.r,overlay.color.g,overlay.color.b, 1);
        
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    void Die()
        {
            print(name + " was destroyed");
            Destroy(gameObject);
            FindObjectOfType<GameManager>().EndGame();

        }
    }

