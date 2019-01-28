using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameController : MonoBehaviour {

    private int scorePE;
    private int scorePD;
    [SerializeField]
    private int pont;
    private AudioSource somcrowd;

    [SerializeField]
    public TextMeshProUGUI scoreTextPE;
    [SerializeField]
    public TextMeshProUGUI scoreTextPD;

    [SerializeField]
    private string equipaE;
    [SerializeField]
    private string equipaD;

    

    // Get gameObjects
    private Ball ballGameObject;
    private Player playerGameObjectE;
    private Player playerGameObjectD;

    public Vector3[] posIniciais;
    private Player player;

    [SerializeField]
    private Transform playerE;
    [SerializeField]
    private Transform playerD;

    [SerializeField]
    private TextMeshProUGUI TextWinner;
    // clock
    [SerializeField]
    private TextMeshProUGUI clockText;

    private float mainTimer = 60;



    //public float timer = 0;
    private bool canCount = true;
    private bool doOnce = false;
    //clock

    //atirarObjetos
    public int objectsCount;
    public Vector3 spawnValues;
    public GameObject supportersObject;
    public float spawnWait;
    public float startWait;
    public float objectsWait;

    //bonus
    public int objectsCountBonus;
    public Vector3 spawnValuesBonus;
    public GameObject bonusObject;
    //public float spawnWaitBonus;
    public float startWaitBonus;
    public float objectsWaitBonus;


    private CamaraShake camShake;  // camera shake

    private bool ganhar = false;

    [SerializeField]
    private GameObject MatchScreenGO;

    [SerializeField]
    private GameObject MenuScreenGO;

    [SerializeField]
    private GameObject Ball;



    private float tempoMenu = 3;

    // Use this for initialization
    void Start()
    {
        somcrowd = gameObject.GetComponent<AudioSource>();

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
        }
        if (player == null)
        {
            Debug.Log("Cannot find 'Player' script");
        }

        scorePE = 0;
        scorePD = 0;

        UpdateScorePD();
        UpdateScorePE();

       
        clockText.color = Color.white;

        //StartCoroutine(SpawnObjects());
        StartCoroutine(SpawnBonus());

        GameObject ballObject = GameObject.FindWithTag("Ball");

        ballGameObject = ballObject.GetComponent<Ball>();

        

    }

    private void Awake()
    {
        camShake = GameObject.Find("Main Camera").GetComponent<CamaraShake>();
    }

    void Update()
    {
        //clock
        if (mainTimer >= 0.0f && canCount == true)
        {
            mainTimer -= Time.deltaTime;
            int seconds = Mathf.RoundToInt(mainTimer % 60f);

            if (seconds <= 10)
            {
                clockText.color = Color.red;
                
            }
            else
            {
                clockText.color = Color.green;
            }
           
            clockText.text = seconds.ToString();
        }
        else if (mainTimer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            clockText.text = "0";
            mainTimer = 0.0f;

            StartCoroutine(SpawnObjects());
            somcrowd.Play();
        }

        if (scorePE == 3)
        {
            ganhar = true;
            print("O jogador da esquerda ganhou");

            
            ballGameObject.Imobile(ganhar);

            StartCoroutine(camShake.CameraShakeMethod(0.5f, 8f));
            StartCoroutine(StartWinnerPiloto());

            mainTimer = 60;
           
            if (tempoMenu >= 0.0f)
            {
               


                tempoMenu -= Time.deltaTime;
                if(tempoMenu <= 0.0f)
                {
                    
                    MatchScreenGO.SetActive(false);
                    MenuScreenGO.SetActive(true);

                    scorePD = 0;
                    scorePE = 0;
                    print("scorePE :" + scorePE);
                    UpdateScorePE();
                    UpdateScorePD();
                    tempoMenu = 3;
                }
            }
            



            //readyText.gameObject.SetActive(true);
            //readyText.text = "PILOTO WIN!";



        }
        else if (scorePD == 3)
        {

            ganhar = true;
            print("O jogador da direita ganhou");

            
            ballGameObject.Imobile(ganhar);

            StartCoroutine(camShake.CameraShakeMethod(0.5f, 8f));
            StartCoroutine(StartWinnerBarbas());

            mainTimer = 60;
            
            if (tempoMenu >= 0.0f)
            {
                
                tempoMenu -= Time.deltaTime;
                if (tempoMenu <= 0.0f)
                {
                    
                    MatchScreenGO.SetActive(false);
                    MenuScreenGO.SetActive(true);

                    scorePD = 0;
                    scorePE = 0;
                    UpdateScorePE();
                    UpdateScorePD();
                    tempoMenu = 3;
                }
            }

            
        }
    }



    public void Goal(Transform baliza)
    {
        

        playerE.position = posIniciais[0];
        playerD.position = posIniciais[1];

        if (baliza.name == "BalizaEsquerda")
        {
            
            gameObject.transform.position = posIniciais[0];
            scorePD += pont;
            UpdateScorePD();
        }
        else if (baliza.name == "BalizaDireita")
        {
            
            gameObject.transform.position = posIniciais[1];
            scorePE += pont;
            UpdateScorePE();
        }
    }

    void UpdateScorePE()
    {

        scoreTextPE.text = equipaE + " " + scorePE + " : ";
        //scoreTextPE.text = scorePE.ToString();
    }

    void UpdateScorePD()
    {
        scoreTextPD.text = scorePD + " " + equipaD ;
       
    }

    IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (var i = 0; i < objectsCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(supportersObject, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(objectsWait);
        }
    }

    IEnumerator SpawnBonus()
    {
        yield return new WaitForSeconds(startWaitBonus);
        while (mainTimer > 0.0f)
        {
            //for (var i = 0; i < objectsCountBonus; i++)
            //{
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValuesBonus.x, spawnValuesBonus.x), spawnValuesBonus.y, spawnValuesBonus.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(bonusObject, spawnPosition, spawnRotation);
            // yield return new WaitForSeconds(spawnWaitBonus);
            //}
            yield return new WaitForSeconds(objectsWaitBonus);
        }
    }


    public void DestroyWin(string nome)
    {
        
        StartCoroutine(camShake.CameraShakeMethod(0.5f, 5f));

        if (nome == "PlayerEsquerda")
        {
            StartCoroutine(StartWinnerBarbas());
            
        }
        else if (nome == "PlayerDireita")
        {
            StartCoroutine(StartWinnerPiloto());
        }

        /*   Este codigo é o que gostariamos de usar para informar os jogadores de quem venceu e passado 3 segundos a tela do menu apareceria, mas como nao foi possivel devido à funcao apenas ser chamada uma vez, optamos por enviar diretamento para o menu
        mainTimer = 60;
        if (tempoMenu >= 0.0f) 
        {
            tempoMenu -= Time.deltaTime;
            
            if (tempoMenu <= 0.0f)
            {

                Ball.SetActive(false);
                MatchScreenGO.SetActive(false);
                MenuScreenGO.SetActive(true);
               

                scorePD = 0;
                scorePE = 0;
                UpdateScorePE();
                UpdateScorePD();

                tempoMenu = 3;
            }
        }
        */
        mainTimer = 60;

        
        Ball.SetActive(false);
        MatchScreenGO.SetActive(false);
        MenuScreenGO.SetActive(true);
        

        scorePD = 0;
        scorePE = 0;
        UpdateScorePE();
        UpdateScorePD();




    }

    

    IEnumerator StartWinnerPiloto()
    {

        TextWinner.gameObject.SetActive(true);
        yield return new WaitForSeconds(0f);


       
        TextWinner.text = "PILOTO Wins!";
       
    
    }

    IEnumerator StartWinnerBarbas()
    {

        TextWinner.gameObject.SetActive(true);
        yield return new WaitForSeconds(0f);



        TextWinner.text = "BARBAS Wins!";


    }




}
