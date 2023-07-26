using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedBlock : MonoBehaviour
{
    //keyÇälìæÇ∑ÇÈÇ∆è¡Ç¶ÇÈï«
    MeshRenderer mesh;
    Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        Reset();
    }
    private void OnEnable()
    {
        LevelManager.Instance.onRespawn += Reset;
        Key.onKeyCollected += Open;
    }

    private void OnDisable()
    {
        if (Key.onKeyCollected != null)
            Key.onKeyCollected -= Open;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Open()
    {
        Debug.Log("Open()");
        mesh.enabled = false;
        collider.enabled = false;
    }

    void Reset()
    {
        mesh.enabled = true;
        collider.enabled = true;
    }
}
