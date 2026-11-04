using Gamekit3D;
using UnityEngine;

public class SurvivalMod : MonoBehaviour
{
    [SerializeField] private float thirstLevel = 100f;
    [SerializeField] private float timer = 10f;

    [SerializeField] private float thirstSpeed;
    [SerializeField] private bool thirsty;
    [SerializeField] private bool parched;
    public int waterBottles = 0;

    [SerializeField] private GameObject playerObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /*Subtracting the current value of thirstLevel with thirstSpeed. thirstSpeed is normalized
        by using Time.deltaTime, so that it's frame rate independent. Math.Clamp restricts the thirstLevel
        between its current max value and 0.
        */
        thirstLevel -= Mathf.Clamp(thirstSpeed * Time.deltaTime, 0, thirstLevel);
        /* if statement checks if thirstLevel is equal to, or lower, than 50 and 25, for thirsty and parched.
         Therefor they check or uncheck the bools in comparison to the value
        */
        if (thirstLevel <= 50f && !thirsty)
        {
            Debug.Log("Getting thirsty...");
            thirsty = true;
        }

        else if (thirstLevel >= 51f && thirsty == true)
        {
            thirsty = false;
        }

        if (thirstLevel <= 25f && !parched)
        {
            Debug.Log("I'm feeling parched...");
            parched = true;
        }

        else if (thirstLevel >= 26f && parched == true)
        {
            parched = false;
        }

        if (thirstLevel <= 0 && thirsty)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ThirstDamage();
                timer = 10f;
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            DrinkWater();
        }
    }

    private void ThirstDamage()
    {
        Damageable.DamageMessage msg = new Damageable.DamageMessage()
        {
            amount = 1,
            damager = this,
            damageSource = new Vector3(x: 0, y: 0, z: 0),
            direction = new Vector3(x: 0, y: 0, z: 0),
            throwing = false,
            stopCamera = false
        };
        playerObject.GetComponent<Damageable>().ApplyDamage(msg);
        Debug.Log("Damage by thirst!");
        timer = 10f;
    }


    public void AddWater()
    {
        int randomWater = Random.Range(1, 6);
        waterBottles += randomWater;

        Debug.Log("Added " + randomWater + " water!");
    }
}

    public void DrinkWater()
    {
        if (waterBottles > 0)
        {
            waterBottles--;
            Debug.Log("Drank water! Remaining: " + waterBottles);
        }
        else
        {
            Debug.Log("No water left!");
        }
    }
}