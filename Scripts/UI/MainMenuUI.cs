using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {

    [SerializeField]
    private GameObject MatchScreenGO;

    [SerializeField]
    private GameObject BallGO;

    [SerializeField]
    private GameObject PlayerEsquerda;

    [SerializeField]
    private GameObject PlayerDireita;






    public void StartGameMethod()
    {
       
        MatchScreenGO.SetActive(true);
        gameObject.SetActive(false);

        BallGO.SetActive(true);

        PlayerEsquerda.SetActive(true);
        PlayerDireita.SetActive(true);


    }

    public void OutGameMethod()
    {
        Application.Quit();
    }
}
