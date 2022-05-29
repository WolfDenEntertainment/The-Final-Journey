using UnityEngine;
using UnityEngine.UI;

public class GhostStats : MonoBehaviour
{
    [Header("Base Abilities")]
    [SerializeField] float currentEnergy;
    [SerializeField] float maxEnergy = 1000;
    [SerializeField] float energyRegenRate = 2.5f;

    [Header("UI")]
    [SerializeField] Slider energyBar;
    [SerializeField] GameObject pauseMenuContainer;

    public float CurrentEnergy { get => currentEnergy; set => currentEnergy = value; }
    PlayerControls input;
    InteractController interaction;
    bool isDead = false;

    void Awake()
    {
        input = GetComponent<PlayerControls>();
        interaction = GetComponent<InteractController>();

        currentEnergy = maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = maxEnergy;        
    }

    void Update()
    {
        if (isDead) Die();
        Regen();

        energyBar.value = currentEnergy;

        if (currentEnergy <= 0)
            currentEnergy = 0;

        if (input.escape)
        {
            pauseMenuContainer.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    void Die()
    {
        interaction.Ascend();
    }

    void Regen()
    {
        if (currentEnergy < maxEnergy)
            currentEnergy += energyRegenRate * Time.deltaTime;
    }

    // Pause Menu Methods
    public void ResumeGame()
    {
        pauseMenuContainer.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
