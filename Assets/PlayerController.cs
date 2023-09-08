using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 500f;
    public float sideForce = 500f;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        // Vous pouvez ajouter des initialisations ici si nécessaire.
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            // Si la santé du joueur atteint 0 ou moins, c'est le jeu terminé.
            Debug.Log("Game Over!");

            // Rechargez la scène actuelle pour redémarrer le jeu.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            // Réinitialisez la santé et le score aux valeurs d'origine.
            health = 5;
            score = 0;
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
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifiez si le collider entré a le tag "Pickup".
        if (other.CompareTag("Pickup"))
        {
            // Incémentez le score.
            score++;

            // Appel de la Methode SetScoreText()
            SetScoreText();

            // Affichez le nouveau score dans la console.
            // Debug.Log("Score: " + score);

            // Désactivez ou détruisez l'objet "Coin".
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Trap"))
        {
            // Réduisez la santé du joueur en cas de collision avec un "Trap".
            health--;

            // Affichez la santé mise à jour dans la console.
            Debug.Log("Health: " + health);
        }

        if (other.CompareTag("Goal"))
        {
            // Le joueur a atteint son objectif.
            Debug.Log("You Win !");
        }


    }

    void SetScoreText()
    {
        {
        scoreText.text = "Score: " + score;
        }
    }

}
