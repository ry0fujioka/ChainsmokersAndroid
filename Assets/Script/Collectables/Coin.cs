using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnCollected()
    {
        UIManager.Instance.OnCoinCollected();
        SoundManager.Instance.PlaySound(SoundManager.Instance.CoinClip);
        Hide();
    }
}
