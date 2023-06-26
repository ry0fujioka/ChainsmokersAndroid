using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public delegate void OnDeath();
    public OnDeath onDeath;
    private Rigidbody rb;
    private PlayerMove playerMove;
    private BoxCollider[] boxColliders;
    [SerializeField]
    private GameObject bodyModel;
    [SerializeField]
    private GameObject trail;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject brokenBodyPrefab;
    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMove>();
        boxColliders = GetComponents<BoxCollider>();
        isAlive = true;
    }

    private void OnEnable()
    {
        onDeath += Death;
    }

    private void OnDisable()
    {
        //if (gameObject.scene.isLoaded)
        //{
        //    return;
        //}
        //if (LevelManager.Instance != null)
        //    LevelManager.Instance.onRespawn -= OnRespawn;
        //onDeath -= Death;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRespawn()
    {
        playerMove.enabled = true;
        rb.useGravity = true;
        bodyModel.SetActive(true);
        foreach (var collider in boxColliders)
            collider.enabled = true;
        trail.gameObject.SetActive(true);
        isAlive = true;
    }

    private void Death()
    {
        isAlive = false;
        Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        Instantiate(brokenBodyPrefab, this.transform.position, Quaternion.identity);
        bodyModel.SetActive(false);
        foreach (var collider in boxColliders)
            collider.enabled = false;
        playerMove.enabled = false;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        trail.gameObject.SetActive(false);
        SoundManager.Instance.PlaySound(SoundManager.Instance.DeathClip);
    }

    public void ResetTrail()
    {
        trail.GetComponent<TrailRenderer>().Clear();
    }

    public void Kill()
    {
        if (isAlive)
        {
            onDeath?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Damager"&&isAlive)
        {
            onDeath?.Invoke();
        }
    }
}
