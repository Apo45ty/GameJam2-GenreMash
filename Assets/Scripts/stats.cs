using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
{
    private float currentEnergy = 100f;
    [SerializeField]
    private float maxEnergy = 100;
    [SerializeField]
    private float energyRegen=5;

    // Update is called once per frame
    void Update()
    {
        currentEnergy = Mathf.Min(currentEnergy+energyRegen,maxEnergy);
    }
    public float getFractionOfEnergy (){
        return currentEnergy/maxEnergy;
    }
    public float getCurrentEnergy(){
        return currentEnergy;
    }

    internal void deductEnergy(float weaponCost)
    {
        currentEnergy-=weaponCost;
    }

    internal void addCurrentEnergy(float energyBoost)
    {
        currentEnergy+=energyBoost;
    }
}
