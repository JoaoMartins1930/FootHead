using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCountManager : MonoBehaviour
{

    // Game stuff
    [Header("GamePlay stuff")]
    [SerializeField]
    private Player[] playerMovsComp;

    [SerializeField]
    private GameController[] controlador;

    
    private Ball ballGameObject;

    private bool stop = false;

    public enum MatchState
    {
        ready = 0,
        match = 1

    }


    private void Start()
    {
        GameObject ballObject = GameObject.FindWithTag("Ball");

        ballGameObject = ballObject.GetComponent<Ball>();


    }


    public MatchState currentState;

    // UI stuff
    [Header("UI stuff")]

    [SerializeField]
    private TextMeshProUGUI readyText;

    private void OnEnable()
    {
        ChangeState(MatchState.ready);
    }


    public void ChangeState(MatchState _newState)
    {
        currentState = _newState;
        switch (currentState)
        {
            case MatchState.ready:
                print("siga");
                for (int i = 0; i < playerMovsComp.Length; i++)
                {
                    playerMovsComp[i].enabled = false;       // vai desativar este componente sempre que vai ao ready
                    print("aqui");
                }
                for (int j = 0; j < controlador.Length; j++)
                {
                    controlador[j].enabled = false;
                    
                        
                        StartCoroutine(StartReadyCount());
                    

                }
               
                stop = true;
                ballGameObject.Gravity(stop);




                
                break;
            case MatchState.match:
                for (int i = 0; i < playerMovsComp.Length; i++)
                {
                    playerMovsComp[i].enabled = true;       // aqui volta a ativar
                    print("oi");
                }
                for (int j = 0; j < controlador.Length; j++)
                {
                    controlador[j].enabled = true;
                print("match");
                }

                stop = false;
                ballGameObject.Gravity(stop);

                break;


            
        }
    }

    IEnumerator StartReadyCount()
    {

        readyText.gameObject.SetActive(true);



        readyText.text = "3";
        yield return new WaitForSeconds(1f);
        readyText.text = "2";
        yield return new WaitForSeconds(1f);
        readyText.text = "1";
        yield return new WaitForSeconds(1f);
        readyText.text = "GO!";
        yield return new WaitForSeconds(1f);
        ChangeState(MatchState.match);   // pomos aqui no fim porque é no final da contagem que queremos que começe






        readyText.gameObject.SetActive(false);

    }


}

