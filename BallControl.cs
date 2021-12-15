using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Control del movimiento
    public float speed = 5f;
    public float speedIncrement = 1f;
    private float angle = Mathf.PI / 4;

    // Referencias a otros objetos
    private AudioSource myAudioSource;
    private GameObject myManagerGO;
    private GameManager myManager;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myManagerGO = GameObject.FindWithTag("GameController");
        myManager = myManagerGO.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculamos las proyecciones en x e y de la velocidad
        float vx = speed * Mathf.Cos(angle);
        float vy = speed * Mathf.Sin(angle);

        // Efectúan la traslación
        Vector2 increment = new Vector2(vx, vy);
        transform.Translate(increment * Time.deltaTime);

        // Si la bola llega arriba: rebota hacia abajo
        if ((angle > 0) && (transform.position.y > 4.7)) 
        {
            angle = -angle;
            myAudioSource.Play();
        }

        // Si la bola llega abajo: rebota hacia arriba
        if ((angle < 0) && (transform.position.y < -4.7))
        {
            angle = -angle;
            myAudioSource.Play();
        }

        // Si la bola llega al extremo izquierdo: ...
        if (transform.position.x < -6f)
        {
            Destroy(gameObject);
            myManager.RightPlayerPoint();
        }

        // Si la bola llega al extremo derecho: ...
        if (transform.position.x > 6f)
        {
            Destroy(gameObject);
            myManager.LeftPlayerPoint();
        }
    }

    // Gestión de la colisión
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Código para el rebote controlado
        if (transform.position.x < 0) 
        {
            // Colisión con el jugador de la izquierda
            float distance = transform.position.y - collision.gameObject.transform.position.y;
            angle = distance * Mathf.PI / 2;
            speed += speedIncrement;
        }
        else
        {
            // Colisión con el jugador de la derecha
            float distance = transform.position.y - collision.gameObject.transform.position.y;
            angle = Mathf.PI - distance * Mathf.PI / 2;
            if (angle > Mathf.PI)
            {
                angle -= (2 * Mathf.PI);
            }
            speed += speedIncrement;
        }

        // Código para rebote elástico
        /*
        if (angle>=0)
        {
            angle = Mathf.PI - angle;
            speed += speedIncrement;
        } 
        else
        {
            angle = - Mathf.PI - angle;
            speed += speedIncrement;
        }
        */
    }

    // Lanzar hacia la izquierda
    public void ThrowLeft()
    {
        angle = Random.Range(2 * Mathf.PI / 3f, 4 * Mathf.PI / 3f);
        if (angle > Mathf.PI)
        {
            angle -= 2 * Mathf.PI;
        }
    }

    // Lanzar hacia la derecha
    public void ThrowRight()
    {
        angle = Random.Range(-Mathf.PI / 3f, Mathf.PI / 3f);
    }
}
