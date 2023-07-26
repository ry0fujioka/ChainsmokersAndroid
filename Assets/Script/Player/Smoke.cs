using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public bool isVisible;
    public GameObject sphere;
    float disappear;

    private void OnEnable()
    {
        PlayerHealth.Instance.onDeath += OnDeath;
    }


    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = null;
        isVisible = false;
        sphere.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isVisible)
        {
            if (Time.time >= disappear)
            {
                SmokeGenerator.Instance.meshPositions.Remove(new KeyValuePair<GameObject, Vector3>(this.gameObject, this.transform.position));
                sphere.SetActive(false);
                isVisible = false;
            }
        }
    }

    public void Initialize(float time)
    {
        if (!sphere.activeSelf)
            sphere.SetActive(true);
        isVisible = true;
        disappear = time + Time.time;
    }

    void OnDeath()
    {
        if(isVisible)
        {
            SmokeGenerator.Instance.meshPositions.Remove(new KeyValuePair<GameObject, Vector3>(this.gameObject, this.transform.position));
            sphere.SetActive(false);
            isVisible = false;
        }
    }
}
