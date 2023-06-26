using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuel : MonoBehaviour
{
    [SerializeField]
    float maxFuel;
    [SerializeField]
    float fuelConsumeSpeed;

    float currentFuel;
    bool onDeathInvoked = false;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void OnEnable()
    {
        LevelManager.Instance.onRespawn += Reset;
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded)
        {
            return;
        }
        //if (LevelManager.Instance != null)
        //    LevelManager.Instance.onRespawn -= Reset;
    }


    public void UpdateFuel()
    {
        if (currentFuel > 0)
        {
            currentFuel -= fuelConsumeSpeed * Time.deltaTime;
            currentFuel = Mathf.Max(0, currentFuel);
            UIManager.Instance.ChangeFuelAmount(Mathf.Clamp(currentFuel / maxFuel, 0, 1));
        }
        else if (!onDeathInvoked)
        {
            PlayerHealth.Instance.onDeath?.Invoke();
            onDeathInvoked = true;
        }
    }

    void Reset()
    {
        currentFuel = maxFuel;
        onDeathInvoked = false;
    }

    public void RestoreFuel(float amount)
    {
        currentFuel += amount;
        currentFuel = Mathf.Min(currentFuel, maxFuel);
        UpdateFuel();
    }
}
