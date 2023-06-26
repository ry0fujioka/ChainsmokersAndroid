using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField]
    float timeToDisappear;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(this.gameObject, timeToDisappear);
    }
}
