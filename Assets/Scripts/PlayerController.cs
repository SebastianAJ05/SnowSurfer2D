using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float boostSpeed = 20f;
    [SerializeField] private ParticleSystem boostEffect; // Particle effect to play when the player boosts
    [SerializeField] private ScoreManager scoreManager; // Reference to the ScoreManager script
    float baseSpeed; // Store the base speed of the Surface Effector
    private bool canControlPlayer = true; // Flag to control player input

    public bool CanControlPlayer { get => canControlPlayer; set => canControlPlayer = value; }
    SurfaceEffector2D surfaceEffector;
    InputAction moveAction;
    Vector2 moveInput;
    Rigidbody2D rb;
    float previousRotation; // Store the previous rotation of the player for flip calculation
    float currentRotation; // Store the current rotation of the player for flip calculation
    float totalRotation; // Store the total rotation of the player for flip calculation
    int flipCount; // Store the number of flips performed by the player
    int activePowerUpsCount; // Track the number of active power-ups


    void Start()
    {
        transform.GetChild(0).GetChild(PlayerPrefs.GetInt("SelectedCharacter", 0)).gameObject.
        SetActive(true); // Activate the selected character model
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector = FindAnyObjectByType<SurfaceEffector2D>();
        baseSpeed = surfaceEffector.speed; // Store the base speed of the Surface Effector
    }

    void Update()
    {
        if (!canControlPlayer) return; // If player control is disabled, exit the method
        PlayerTorque();
        boostPlayer();
        CalculateFlips();
    }

    private void CalculateFlips()
    {
        float currentRotation = transform.eulerAngles.z; // Get the current rotation of the player in degrees
        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation); // Calculate the change in rotation since the last frame
        if (Math.Abs(totalRotation) > 340)
        {
            flipCount++;
            scoreManager.AddScore(flipCount * 100); // Update the score in the ScoreManager
            totalRotation = 0; // Reset the total rotation after a flip is counted
        }
        previousRotation = currentRotation; // Update the previous rotation for the next frame
    }

    /// <summary>
    /// Applies torque to the player based on input from the Move action.
    /// </summary>
    void PlayerTorque()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.x < 0)
        {
            rb.AddTorque(torqueAmount);
        }
        else if (moveInput.x > 0)
        {
            rb.AddTorque(-torqueAmount);
        }
    }
    void boostPlayer()
    {
        //Increase the player's speed when the up arrow key is pressed
        //Surface Efector speed is increased to boostSpeed
        if (moveInput.y > 0)
        {
            surfaceEffector.speed = boostSpeed;

        }
        else
        {
            surfaceEffector.speed = baseSpeed; //Normal speed
        }
    }

    // Detects collision with the floor and plays the boost effect particles
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (moveInput.y > 0)
        {
            int layerIndex = LayerMask.NameToLayer("Floor");

            if (collision.gameObject.layer == layerIndex)
            {
                boostEffect.Play(); // Play the boost effect particles
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (collision.gameObject.layer == layerIndex)
        {
            boostEffect.Stop(); // Stop the boost effect particles
        }
    }

    public void ApplyPowerUp(PowerUpScriptableObject powerUpData)
    {
        activePowerUpsCount++; // Increment the count of active power-ups
        if (powerUpData.PowerUpType == "Speed")
        {
            baseSpeed += powerUpData.PowerUpValue; // Increase the base speed by the power-up value
            boostSpeed += powerUpData.PowerUpValue; // Increase the boost speed by the power-up value

        }
    }
    public void DeactivePowerUp(PowerUpScriptableObject powerUpData)
    {
        activePowerUpsCount--; // Decrement the count of active power-ups
        if (activePowerUpsCount == 0)
        {
            if (powerUpData.PowerUpType == "Speed")
            {
                baseSpeed -= powerUpData.PowerUpValue; // Decrease the base speed by the power-up value
                boostSpeed -= powerUpData.PowerUpValue; // Decrease the boost speed by the power-up value
            }
        }
    }
}
