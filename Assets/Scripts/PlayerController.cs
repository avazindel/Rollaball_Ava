using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        count = 0;


        SetCountText();
        winTextObject.SetActive(false);


   
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();

        // Check if the count has reached or exceeded the win condition.
        if (count >= 12)
        {
            // Display the win text.
            winTextObject.SetActive(true);

            // Destroy the enemy GameObject.
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }


    private void FixedUpdate()
    {

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);

            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";


        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            audioSource.Play();


            SetCountText();
        }

    }

}
