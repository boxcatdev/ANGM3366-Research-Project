using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsProfile
{
    /*public Dictionary<PropertyType, float> properties = new Dictionary<PropertyType, float>
    {
        { PropertyType.Cooldown, 1f },
        { PropertyType.Speed, 2f },
        { PropertyType.Range, 0.5f }
    };*/

    private float minCooldown = 0f;
    private float maxSpeed = 15f;
    private float maxRange = 5f;

    //public properties
    public float cooldownTime { get; private set; }
    public float playerSpeed { get; private set; }
    public float collectionRange { get; private set; }

    public bool isProfileSetup = false;

    public StatsProfile()
    {
        isProfileSetup = false;
    }

    public void ApplyMultiplier(float multiplier, PropertyType property)
    {
        multiplier = multiplier * 0.01f;

        switch (property)
        {
            case PropertyType.Cooldown:
                // cooldown multiply
                cooldownTime -= (cooldownTime * multiplier);
                if (cooldownTime < minCooldown) cooldownTime = minCooldown;
                Debug.Log(cooldownTime);
                //end
                break;
            case PropertyType.Speed:
                // speed multiply
                playerSpeed += (playerSpeed * multiplier);
                if(playerSpeed > maxSpeed) playerSpeed = maxSpeed;
                Debug.Log(playerSpeed);
                //end
                break;
            case PropertyType.Range:
                // range multiply
                collectionRange += (collectionRange * multiplier);
                if (collectionRange > maxRange) collectionRange = maxRange;
                Debug.Log(collectionRange);
                //end
                break;
        }
    }
    public void ResetProperties(float cooldown, float speed, float range)
    {
        cooldownTime = cooldown;
        playerSpeed = speed;
        collectionRange = range;

        isProfileSetup = true;
    }
}

//property enum
public enum PropertyType { Cooldown, Speed, Range }
