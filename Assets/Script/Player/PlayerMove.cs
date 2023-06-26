using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private GameObject bodyModel;
    [SerializeField]
    private GameObject propellerModel;
    [SerializeField]
    private float propellorSpinSpeed = 10;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float idleTurnSpeed;
    [SerializeField]
    private float movingTurnSpeed;
    [SerializeField]
    private float turnAccelerationTime = 0.3f;
    [SerializeField]
    private float maxTiltAngle = 45;
    private float turnSpeed;
    private float currentTurnSpeed = 0;
    private float turnSpeedSmoothing = 0;
    

    Rigidbody rb;
    PlayerFuel playerFuel;
    float input;
    bool isAccelerateButtonPressing = false;
    bool isAccelerating = false;
    //for propellor animation
    float currentPropellorVelocity;
    float refPropellorSpinSpeed;
    AudioSource audioSource;
    private bool mobileInput;


    private void OnEnable()
    {
        LevelManager.Instance.onRespawn += OnRespawn;
    }

    private void OnDisable()
    {
        //if (gameObject.scene.isLoaded)
        //{
        //    return;
        //}
        //if (LevelManager.Instance != null)
        //    LevelManager.Instance.onRespawn -= OnRespawn;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerFuel = GetComponent<PlayerFuel>();
        audioSource = GetComponent<AudioSource>();
        currentTurnSpeed = 0;
        turnSpeedSmoothing = 0;
}

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Turn();
        Move();
        Fuel();
        PlaySound();
        SetAnimation();
    }

    private void GetInput()
    {
        if (!mobileInput)
            input = -Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.Space) || isAccelerateButtonPressing)
            isAccelerating = true;
        else
            isAccelerating = false;
    }

    private void Move()
    {
        if (isAccelerating)
        {
            rb.AddForce(this.transform.right * speed * Time.deltaTime * 300);
            //rb.useGravity = false;
        }
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    void Turn()
    {
        turnSpeed = isAccelerating ? movingTurnSpeed : idleTurnSpeed;
        currentTurnSpeed = Mathf.SmoothDamp(currentTurnSpeed, turnSpeed * input, ref turnSpeedSmoothing, turnAccelerationTime);
        //rb.AddRelativeTorque(Vector3.forward * currentTurnSpeed * Time.deltaTime);
        rb.angularVelocity = Vector3.forward * currentTurnSpeed * Time.fixedDeltaTime * Mathf.PI * 3;
    }

    void Fuel()
    {
        if (isAccelerating)
            playerFuel.UpdateFuel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Smoke")
        {
            SmokeGenerator.Instance.OnTouch(other.gameObject.transform.parent.gameObject);
        }
    }

    private void SetAnimation()
    {
        float targePropellorSpeed = isAccelerating ? propellorSpinSpeed : 0;
        currentPropellorVelocity = Mathf.SmoothDamp(currentPropellorVelocity, targePropellorSpeed, ref refPropellorSpinSpeed, 0.5f);
        propellerModel.transform.Rotate(rb.velocity.magnitude * Time.deltaTime * currentPropellorVelocity, 0, 0);
        float currentXAngle = Mathf.Abs(bodyModel.transform.localEulerAngles.x % 360);
        currentXAngle = currentXAngle < 180 ? currentXAngle : currentXAngle - 360;
        if ((currentXAngle > -maxTiltAngle && input < 0) || (currentXAngle < maxTiltAngle && input > 0))
        {
            bodyModel.transform.Rotate(currentTurnSpeed * Time.deltaTime * 2, 0, 0);
        }
        else if(input == 0)
        {
            if (currentXAngle < -0.1f)
                bodyModel.transform.Rotate(Time.deltaTime * 20, 0, 0);
            else if (currentXAngle > 0.1f)
                bodyModel.transform.Rotate(Time.deltaTime * -20, 0, 0);
            else
                bodyModel.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void PlaySound()
    {
        if (Time.timeScale == 0)
        {
            audioSource.Stop();
            return;
        }
        if (isAccelerating)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
            audioSource.Stop();
    }

    public void SetInput(float verticalInput)
    {
        input = -verticalInput;
        mobileInput = true;
    }

    public void ResetInput()
    {
        input = 0;
        mobileInput = false;
    }

    public void OnPressAccelerateButton()
    {
        isAccelerateButtonPressing = true;
    }

    public void OnReleaseAccelerateButton()
    {
        isAccelerateButtonPressing = false;
    }

    private void OnRespawn()
    {
        this.transform.localEulerAngles = Vector3.zero;
        bodyModel.transform.localEulerAngles = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        currentTurnSpeed = 0;
    }
}
