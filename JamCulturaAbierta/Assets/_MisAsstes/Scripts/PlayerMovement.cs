using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IPlayer
{
    [Header("Movement Penalty")]
    [Range(0, 100)]
    public float lateralPenalty = 30;
    [Range(0, 100)]
    public float backwardPenalty = 30;

    [Header("Effects")]
    public SoundTypeVolume walkSound;

    private Stats stats;
    private float acceleration;
    private Vector3 movement;
    private Rigidbody rb;
    private bool isPlayerPaused;

    private const string walkSoundId = "player_walk";

    private void Awake()
    {
        Initialize();
    }


    private void Update()
    {
        if (IsPaused) return;
        HandleInput();
        ManageSound();
    }

    private void Initialize()
    {
        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody>();
        movement = Vector3.zero;
        acceleration = 0;
        walkSound.soundModifiers.Add(new Modifier(walkSoundId, 0));
    }


    public void EnterPainting()
    {
        isPlayerPaused = true;
        if (walkSound)
        {

            walkSound.SetModifierValue(walkSoundId, 0);
        }
    }

    public void ExitPainting()
    {
        isPlayerPaused = false;
    }

    public void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementInput = new Vector3(horizontal, 0, vertical);

        ApplyMovement(movementInput);

    }

    private void ManageSound()
    {
        if (walkSound)
        {

            walkSound.SetModifierValue(walkSoundId, WalkSoundModifier);
        }
    }

    private void ApplyMovement(Vector3 movementInput)
    {
        Vector3 maxMovement = (movementInput.normalized * stats.MovementSpeed);

        float zMax = Mathf.Abs(maxMovement.z) * (movementInput.z < 0 ? BackwardSpeedMultiplier : 1);
        float xMax = Mathf.Abs(maxMovement.x) * LateralSpeedMultiplier;


        //sacar proporción y multiplicar ms con ella

        //si es mayor entro (y si es menor FBI)
        if (movementInput.magnitude > 0.01f)
        {
            acceleration = Mathf.Lerp(acceleration, stats.Acceleration, stats.Acceleration * Time.deltaTime);
        }
        else
        {
            acceleration = Mathf.Lerp(acceleration, 0, stats.Acceleration * Time.deltaTime);
        }

        if (movementInput.magnitude > 0.01f)
        {
            movement.x = Mathf.Lerp(movement.x, xMax * movementInput.x, acceleration * Time.deltaTime);
            movement.z = Mathf.Lerp(movement.z, zMax * movementInput.z, acceleration * Time.deltaTime);
        }
        else
        {
            movement.x = Mathf.Lerp(movement.x, 0, stats.Acceleration * 2 * Time.deltaTime);
            movement.z = Mathf.Lerp(movement.z, 0, stats.Acceleration * 2 * Time.deltaTime);
        }




        movement.x = Mathf.Clamp(movement.x, -xMax, xMax);
        movement.z = Mathf.Clamp(movement.z, -zMax, zMax);

        float yVel = rb.velocity.y;
        



        //print("" + xMax + "   " + zMax + "   " + maxVel);



        //print(movement + " / " + movement.magnitude);

        rb.velocity = transform.TransformDirection(movement);
        rb.velocity = new Vector3(rb.velocity.x, yVel, rb.velocity.z);
        //rb.MovePosition(rb.position + (movement * Time.deltaTime));

    }


    public bool IsPaused
    {
        get
        {
            return isPlayerPaused || PauseManager.Instance.onPause;
        }
    }

    private float WalkSoundModifier
    {
        get
        {
            return rb.velocity.magnitude / stats.MovementSpeed;
        }
    }

    private float BackwardSpeedMultiplier
    {
        get
        {
            return (100 - backwardPenalty) * 0.01f;
        }
    }

    private float LateralSpeedMultiplier
    {
        get
        {
            return (100 - lateralPenalty) * 0.01f;
        }
    }

}
