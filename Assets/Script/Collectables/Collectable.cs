using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    MeshRenderer mesh;
    [SerializeField]
    Collider collider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        LevelManager.Instance.onRespawn += Show;
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded)
        {
            return;
        }
        //if (LevelManager.Instance != null)
        //    LevelManager.Instance.onRespawn -= Show;
    }

    public virtual void Reset()
    {
        collider.enabled = true;
        mesh.enabled = true;
    }

    public virtual void Show()
    {
        collider.enabled = true;
        mesh.enabled = true;
    }

    public virtual void Hide()
    {
        collider.enabled = false;
        mesh.enabled = false;
    }

    public virtual void OnCollected()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Area")
        {
            OnCollected();
        }
    }
}
