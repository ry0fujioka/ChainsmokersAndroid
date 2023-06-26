using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Collectable
{
    [SerializeField]
    private float fuelAmount = 25;

    private PlayerFuel playerFuel;

    // Start is called before the first frame update
    void Start()
    {
        playerFuel = GameObject.FindObjectOfType<PlayerFuel>();
    }

    public override void OnCollected()
    {
        playerFuel.RestoreFuel(fuelAmount);
        SoundManager.Instance.PlaySound(SoundManager.Instance.FuelClip);
        Hide();
    }
}
