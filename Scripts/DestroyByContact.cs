using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyByContact : MonoBehaviour
{

   
    private GameController gc;
   
    private string nome;

   

    private void Start()
    {
        GameObject ballObject = GameObject.FindWithTag("GameController");

        gc = ballObject.GetComponent<GameController>();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.name == "UpLimit")
        {
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
        else if (other.transform.name == "Field" || other.transform.name == "Ball")
        {
            
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {

            print(other.transform.name + " MORREU");
            Destroy(gameObject);
            
            other.gameObject.SetActive(false);
           

            nome = other.transform.name;

            gc.DestroyWin(nome);



            


        }

    }

   
}
