using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    bool switching;
    [SerializeField]
    float onDuration;
    [SerializeField]
    float offDuration;
    [SerializeField]
    float offset;
    [SerializeField]
    Transform startPosition;
    [SerializeField]
    LayerMask collideWith;
    Vector3 endPosition;
    bool isOn = true;
    bool stopLaserOnOff = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startPosition.position);
        endPosition = this.transform.up * 100;
        lineRenderer.enabled = false;
        if (switching)
        {
            isOn = false;
            StartCoroutine(StartLaser());
        }
        else
            isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            RaycastHit hit;
            if (Physics.Raycast(startPosition.position, transform.up, out hit, 100, collideWith))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("Player"))
                {
                    hit.collider.GetComponent<PlayerHealth>().Kill();
                    Debug.Log("Hit");
                }
                else
                    lineRenderer.SetPosition(1, hit.point);
            }
            else
                lineRenderer.SetPosition(1, endPosition);
        }

        lineRenderer.enabled = isOn;
    }

    IEnumerator StartLaser()
    {
        yield return new WaitForSeconds(offset);
        isOn = true;
        if (switching)
            StartCoroutine(LaserOnOff());
    }

    IEnumerator LaserOnOff()
    {
        while (!stopLaserOnOff)
        {
            isOn = true;
            yield return new WaitForSeconds(onDuration);
            isOn = false;
            yield return new WaitForSeconds(offDuration);
        }
    }
}
