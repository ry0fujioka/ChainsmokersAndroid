using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Singleton<Goal>
{

    public delegate void OnGoal();
    public OnGoal onGoal;
    private bool isInvoked;

    // Start is called before the first frame update
    void Start()
    {
        isInvoked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player"&&!isInvoked)
        {
            //call end game function
            SoundManager.Instance.PlaySound(SoundManager.Instance.GoalClip, 0.7f);
            onGoal?.Invoke();
            isInvoked = true;
        }
    }
}
