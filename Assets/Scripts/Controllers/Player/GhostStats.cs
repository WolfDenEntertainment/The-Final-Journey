using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GhostStats : MonoBehaviour
{
    [Header("Base Abilities")]
    [SerializeField] float currentEnergy;
    [SerializeField] float maxEnergy = 1000;
    [SerializeField] float energyRegenRate = 2.5f;

    [Header("UI")]
    [SerializeField] Slider energyBar;

    public float CurrentEnergy { get => currentEnergy; set => currentEnergy = value; }

    //public List<Ability> Abilities { get => abilities; }

    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = maxEnergy;
    }

    void Update()
    {
        Regen();

        energyBar.value = currentEnergy;

        if (currentEnergy <= 0)
            currentEnergy = 0;

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void Regen()
    {
        if (currentEnergy < maxEnergy)
            currentEnergy += energyRegenRate * Time.deltaTime;
    }


}
