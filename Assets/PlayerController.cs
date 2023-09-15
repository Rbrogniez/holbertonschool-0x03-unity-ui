using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 1000f;
    public float sideForce = 1000f;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText;
	public Image winLoseImage;

    // Le démarrage est appelé avant la mise à jour de la première image
    void Start()
    {
        // Vous pouvez ajouter des initialisations ici si nécessaire.
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            winLoseImage.gameObject.SetActive(true);
			winLoseImage.color = Color.red;
			winLoseText.color = Color.white;
			winLoseText.text = "Game Over!";
            StartCoroutine(LoadScene(3));

        }
        else
        {
            // Si la santé n'est pas à zéro, vous pouvez continuer à contrôler le joueur.
            if (Input.GetKey("d"))
            {
                rb.AddForce(sideForce * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey("a"))
            {
                rb.AddForce(-sideForce * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey("w"))
            {
                rb.AddForce(0, 0, forwardForce * Time.deltaTime);
            }

            if (Input.GetKey("s"))
            {
                rb.AddForce(0, 0, -forwardForce * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
            SceneManager.LoadScene(0);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifiez si le collider entré a le tag "Pickup".
        if (other.CompareTag("Pickup"))
        {
            // Incrémentez le score.
            score++;

            SetScoreText();

            // Affichez le score mise à jour dans la console.
            // Debug.Log("Score: " + score);

            // Désactivez ou détruisez l'objet "Coin".
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Trap"))
        {
            // Réduisez la santé du joueur en cas de collision avec un "Trap".
            health--;

            SetHealthText();

            // Affichez la santé mise à jour dans la console.
            Debug.Log("Health: " + health);
        }

        if (other.CompareTag("Goal"))
        {
            // Le joueur a atteint son objectif.
            winLoseImage.gameObject.SetActive(true);
			winLoseImage.color = Color.green;
			winLoseText.color = Color.black;
			winLoseText.text = "You Win!";
            StartCoroutine(LoadScene(3));
        }
    }

    void SetScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void SetHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health;
        }
    }

    IEnumerator LoadScene(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
