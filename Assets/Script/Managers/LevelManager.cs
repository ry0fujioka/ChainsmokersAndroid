using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public delegate void OnRespawn();
    public OnRespawn onRespawn;
    private GameObject player;
    [SerializeField]
    private Transform[] respawnTransforms;
    int currentTransformIndex;
    [SerializeField]
    private float respawnInterval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initialize");
        player = PlayerHealth.Instance.gameObject;
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerHealth.Instance.onDeath += OnDeath;
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded)
        {
            return;
        }
        //if (PlayerHealth.Instance != null)
        //    PlayerHealth.Instance.onDeath -= OnDeath;
    }

    void Initialize()
    {
        currentTransformIndex = 0;
        player.transform.position = respawnTransforms[currentTransformIndex].position;
        player.transform.rotation = Quaternion.identity;
        PlayerHealth.Instance.ResetTrail();
    }

    void OnDeath()
    {
        StartCoroutine("WaitForRespawn");
    }

    void Respawn()
    {
        player.transform.position = respawnTransforms[currentTransformIndex].position;
        player.transform.rotation = Quaternion.identity;
        PlayerHealth.Instance.OnRespawn();
        PlayerHealth.Instance.ResetTrail();
    }

    void UpdateRespawnPoint()
    {
        currentTransformIndex = (currentTransformIndex >= respawnTransforms.Length) ? 0 : currentTransformIndex + 1;
    }

    void OnGoal()
    {

    }

    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(respawnInterval);
        Respawn();
        onRespawn?.Invoke();
    }
}
