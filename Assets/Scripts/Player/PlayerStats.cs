using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    private int maxlife, maxshield, maxenergy;

    public int lifepoints, shieldpoint, energypoints;
    public float speed;
    public int coins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateValues() {
        //Update HUD
    }

    public void AddLife(int value) {
        if (lifepoints > 0) {
            if (lifepoints + value <= maxlife) {
                lifepoints += value;
            } else {
                lifepoints = maxlife;
            }
        }
        UpdateValues();
    }
    public void AddShield(int value) {
        if(lifepoints > 0) {
            if(shieldpoint + value <= maxshield) {
                shieldpoint += value;
            } else {
                shieldpoint = maxshield;
            }
        }
        UpdateValues();
    }
    public void AddEnergy(int value) {
        if(lifepoints > 0) {
            if(energypoints + value <= maxenergy) {
                energypoints += value;
            } else {
                energypoints = maxenergy;
            }
        }
    }

    public void TakeDamage(int value) {
        if(shieldpoint > 0) {
            if(shieldpoint - value >= 0) {
                shieldpoint -= value;
            } else {
                value -= shieldpoint;
                shieldpoint = 0;
                lifepoints -= value;
            }
        } else {
            if(lifepoints - value >= 0) {
                lifepoints -= value;
            } else {
                lifepoints = 0;
            }
        }
        UpdateValues();
    }

    public bool useEnergy(int value) {
        if(energypoints > 0) {
            if(energypoints - value <= 0) {
                return false;
            } else {
                energypoints -= value;
            }
        } else {
            return false;
        }
        UpdateValues();
        return true;
    }
}
