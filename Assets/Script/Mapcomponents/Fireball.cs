using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Transform fireball;
    [SerializeField] private GameObject _fireball;
    [SerializeField] private float rotationSpeed = 150f;
    [SerializeField] private Vector3 moveSpeed = new Vector3(0, 12, 0);

    [SerializeField] private ParticleSystem explosion;

    private void Awake()
    {
        StartCoroutine(fireballCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        fireball.transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
        fireball.transform.position = transform.position + moveSpeed * Time.deltaTime;
    }

    private void removefireball()
    {
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, this.transform.rotation).gameObject;
            ParticleSystem ps = exp.GetComponent<ParticleSystem>();
            float totalDuration = ps.startLifetime;
            Destroy(exp, totalDuration);
            removefireball();
        }
    }

    private IEnumerator fireballCountdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            removefireball();
        }
    }
}
