using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask objectMask;
    [SerializeField] private float lifeTime = 2f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem impactPS;


    private void Start()
    {

    }
    
    private void Return()
    {
        // SetActiveをfalseにしてプールに戻る
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (lifeTime > 0)
        {
            Invoke(nameof(Return), lifeTime);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
