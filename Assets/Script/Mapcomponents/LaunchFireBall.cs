using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchFireBall : MonoBehaviour
{
    private Animator anim;
    [SerializeField ]private ParticleSystem ps;
    [SerializeField] private GameObject fireball;

    [SerializeField] private float timer1 = 5f;
    private float timer2 = 10;
    [SerializeField] private Vector3 rotation = new Vector3(180,0,0);

    private bool firstShoot;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        ps.Stop();
        firstShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstShoot)
        {
            if (timer1 > 0)
            {
                timer1 -= Time.deltaTime;
                if (timer1 < 0)
                {
                    playAnim();
                    firstShoot = false;
                }
            }
        }
        else if (!firstShoot)
        {
            if (timer2 > 0)
            {
                timer2 -= Time.deltaTime;
                Debug.Log(timer2);
                if (timer2 < 0)
                {
                    playAnim();
                }
            }
        }
    }

    private void playAnim()
    {
        anim.SetBool("Animate", true);
        ps.Play();
        timer2 = 7;
    }

    private void launchFireBall()
    {
        GameObject f = Instantiate(fireball, this.transform.position, Quaternion.Euler(rotation));
        anim.SetBool("Animate", false);
        ps.Stop();
    }
}
