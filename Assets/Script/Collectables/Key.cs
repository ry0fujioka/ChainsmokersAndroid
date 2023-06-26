using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectable
{
    public delegate void OnKeyCollected();
    static public OnKeyCollected onKeyCollected;

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
        Debug.Log("Oncollected");
        base.OnCollected();
        SoundManager.Instance.PlaySound(SoundManager.Instance.UnlockClip);
        onKeyCollected?.Invoke();
        Hide();
    }
}
