using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float snowSpeed = 20f;
    [SerializeField] private float baseSpeed = 8f;
    [SerializeField] private ParticleSystem snowEffect; // Particle effect to play when the player snows
    SurfaceEffector2D surfaceEffector;
    InputAction moveAction;
    Vector2 moveInput;
    Rigidbody2D rb;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector = FindAnyObjectByType<SurfaceEffector2D>();
        baseSpeed = surfaceEffector.speed; // Store the base speed of the Surface Effector
    }

    void Update()
    {
        PlayerTorque();
        snowPlayer();
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
    void snowPlayer()
    {
        //Increase the player's speed when the up arrow key is pressed
        //Surface Efector speed is increased to snowSpeed
        if (moveInput.y > 0)
        {
            surfaceEffector.speed = snowSpeed;
            
        }
        else
        {
            surfaceEffector.speed = baseSpeed; //Normal speed
        }
    }

    // Detects collision with the floor and plays the snow effect particles
    void OnCollisionEnter2D(Collision2D collision)
    {
       int layerIndex = LayerMask.NameToLayer("Floor");

        if(collision.gameObject.layer == layerIndex)
        {
            snowEffect.Play(); // Play the snow effect particles
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (collision.gameObject.layer == layerIndex)
        {
            snowEffect.Stop(); // Stop the snow effect particles
        }
    }
}
