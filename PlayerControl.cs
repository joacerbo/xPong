using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Side { left, right};

public class PlayerControl : MonoBehaviour
{

    // Variables públicas
    public bool isCPUControlled = false;
    public Transform ballTransform;
    public float speed = 3f;
    public Side mySide = Side.left;

    // Variables privadas
    private AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isCPUControlled)
        {
            // Controlado por CPU
            if ((transform.position.y < ballTransform.position.y) && (transform.position.y < 4.2f))
            {
                transform.Translate(0f, speed * Time.deltaTime, 0f);
            }
            if ((transform.position.y > ballTransform.position.y) && (transform.position.y > -4.2f))
            {
                transform.Translate(0f, -speed * Time.deltaTime, 0f);
            }

        }
        else
        {

            // Entrada de teclado

            if (mySide == Side.right)
            {
                // Es el jugador de la derecha
                if ((Keyboard.current.oKey.isPressed) && (transform.position.y < 4.2f))
                {
                    transform.Translate(0f, speed * Time.deltaTime, 0f);
                }
                if ((Keyboard.current.lKey.isPressed) && (transform.position.y > -4.2f))
                {
                    transform.Translate(0f, -speed * Time.deltaTime, 0f);
                }
            }
            else
            {
                // Es el jugador de la izquierda
                if ((Keyboard.current.qKey.isPressed) && (transform.position.y < 4.2f))
                {
                    transform.Translate(0f, speed * Time.deltaTime, 0f);
                }
                if ((Keyboard.current.aKey.isPressed) && (transform.position.y > -4.2f))
                {
                    transform.Translate(0f, -speed * Time.deltaTime, 0f);
                }
            }
        }
    }

    // Gestión de la colisión
    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAudioSource.Play();
    }
}
