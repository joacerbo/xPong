using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    // Variables que contienen los puntos de los jugadores
    private int leftPlayerScore = 0;
    private int rightPlayerScore = 0;

    [Header("Variables")]
    public int maxScore = 5;

    // Referencias a objetos de juego
    private AudioSource myAudioSource;
    private GameObject myball;
    private BallControl myballControl;

    [Header("Objects")]
    public GameObject ballPrefab;
    public GameObject leftPlayer;
    public GameObject rightPlayer;
    public PlayerControl rightPlayerControl;

    // Referencias a objetos del Interfaz de usuario
    // Paneles
    [Header("UI")]
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject setupPanel;
    public GameObject informationPanel;
    public GameObject gamePanel;
    public GameObject gameOverPanel;

    // Textos
    [Header("Texts")]
    public Text leftPlayerScoreText;
    public Text rightPlayerScoreText;
    [Space(20)]
    public Text winText;
    public Text maxScoreText;
    public Text speedIncrementText;
    public Text rightPlayerText;


    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        MainMenu();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // El jugador de la izquierda marca un tanto
    public void LeftPlayerPoint()
    {
        leftPlayerScore++;
        if (leftPlayerScore >= maxScore)
        {
            ShowWinPanel("DE LA IZQUIERDA");
        }
        else
        {
            leftPlayerScoreText.text = leftPlayerScore.ToString();
            StartCoroutine(CreateBallToRight());
        }
        myAudioSource.Play();
    }

    // El jugador de la derecha marca un tanto
    public void RightPlayerPoint()
    {
        rightPlayerScore++;
        if (rightPlayerScore >= maxScore)
        {
            ShowWinPanel("DE LA DERECHA");
        }
        else
        {
            rightPlayerScoreText.text = rightPlayerScore.ToString();
            StartCoroutine(CreateBallToLeft());
        }
        myAudioSource.Play();
    }

    // Muestra el panel de Game Over
    public void ShowWinPanel(string winnerName)
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        leftPlayer.SetActive(false);
        rightPlayer.SetActive(false);
        winText.text = "¡EL JUGADOR " + winnerName + " ES EL GANADOR!";
    }

    // Empezar una nueva partida
    public void InitGame()
    {
        leftPlayerScore = 0;
        rightPlayerScore = 0;

        leftPlayer.SetActive(true);
        rightPlayer.SetActive(true);

        mainMenuPanel.SetActive(false);
        informationPanel.SetActive(false);
        setupPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);

        leftPlayerScoreText.text = leftPlayerScore.ToString();
        rightPlayerScoreText.text = rightPlayerScore.ToString();

        // La lanza hacia un lado u otro
        if (Random.value > 0.5f)
        {
            StartCoroutine(CreateBallToRight());

        }
        else
        {
            StartCoroutine(CreateBallToLeft());
        }

    }


    // Corutina para crear bola y lanzarla a la derecha
    public IEnumerator CreateBallToRight()
    {
        //Espera 1 segundo
        yield return new WaitForSeconds(1);

        // Crea la bola
        myball = Instantiate(ballPrefab);
        myballControl = myball.GetComponent<BallControl>();
        rightPlayerControl.ballTransform = myball.transform;

        // Fija el incremento de velocidad
        if (speedIncrementText.text == "Lento")
        {
            myballControl.speedIncrement = 0.2f;
        }
        else if (speedIncrementText.text == "Moderado")
        {
            myballControl.speedIncrement = 0.6f;
        }
        else
        {
            myballControl.speedIncrement = 1f;
        }

        // Lanza la bola hacia la derecha
        myballControl.ThrowRight();
    }

    // Corutina para crear bola y lanzarla a la izquierda
    public IEnumerator CreateBallToLeft()
    {
        //Espera 1 segundo
        yield return new WaitForSeconds(1);

        // Crea la bola
        myball = Instantiate(ballPrefab);
        myballControl = myball.GetComponent<BallControl>();
        rightPlayerControl.ballTransform = myball.transform;

        // Fija el incremento de velocidad
        if (speedIncrementText.text == "Lento")
        {
            myballControl.speedIncrement = 0.2f;
        }
        else if (speedIncrementText.text == "Moderado")
        {
            myballControl.speedIncrement = 0.6f;
        }
        else
        {
            myballControl.speedIncrement = 1f;
        }

        // Lanza la bola hacia la derecha
        myballControl.ThrowLeft();
    }


    // Ir al Menú principal
    public void MainMenu()
    {
        mainMenuPanel.SetActive(true);
        informationPanel.SetActive(false);
        setupPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    // Ir al menú de Información
    public void InformationPanel()
    {
        mainMenuPanel.SetActive(false);
        informationPanel.SetActive(true);
        setupPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);

    }

    // Ir al menú de Configuración
    public void SetupPanel()
    {
        mainMenuPanel.SetActive(false);
        informationPanel.SetActive(false);
        setupPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    // Cambiar el Máximo de puntos
    public void ChangeMaxScore()
    {
        if (maxScoreText.text=="5") 
        {
            maxScoreText.text = "10";
            maxScore = 10;
        } 
        else if (maxScoreText.text == "10")
        {
            maxScoreText.text = "15";
            maxScore = 15;
        }
        else
        {
            maxScoreText.text = "5";
            maxScore = 5;
        }
    }

    // Cambiar el Incremento de velocidad
    public void ChangeSpeedIncrement()
    {
        if (speedIncrementText.text == "Lento")
        {
            speedIncrementText.text = "Moderado";
        }
        else if (speedIncrementText.text == "Moderado")
        {
            speedIncrementText.text = "Rápido";
        }
        else
        {
            speedIncrementText.text = "Lento";
        }
    }

    // Cambiar el Incremento de velocidad
    public void ChangeRightPlayer()
    {
        if (rightPlayerText.text == "Humano")
        {
            rightPlayerText.text = "CPU";
            rightPlayerControl.isCPUControlled = true;
        }
        else
        {
            rightPlayerText.text = "Humano";
            rightPlayerControl.isCPUControlled = false;
        }
    }

}
