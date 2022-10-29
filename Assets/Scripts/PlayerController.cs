using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;

	public TextMeshProUGUI countText;
	public GameObject winTextObject;

    private float movementX;
    private float movementY;

	private Rigidbody rb;
	private int count, countYellow, countRed;


	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the counters to 0
		countYellow = 0;
		countRed = 0;
		count = 0;

		SetCountText ();

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
	}

	void FixedUpdate ()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;
			countYellow = countYellow + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}

		if (other.gameObject.CompareTag ("Enemy"))
		{
			other.gameObject.SetActive (false);

			// Substract one to the score variable 'count'
			count = count - 1;
			countRed = countRed + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}		
	}

        void OnMove(InputValue value)
        {
        	Vector2 v = value.Get<Vector2>();

        	movementX = v.x;
        	movementY = v.y;
        }

        void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (countYellow >= 12)
		{
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
			winTextObject.GetComponent<TextMeshProUGUI>().text="Biennnnnn ¡¡Ganaste!!";
		}

		if (countRed >= 6)
		{
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
			winTextObject.GetComponent<TextMeshProUGUI>().text="Oooooh! ¡¡Perdiste!!";
		}		
	}
}