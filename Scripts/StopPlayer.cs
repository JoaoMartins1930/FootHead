using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayer : MonoBehaviour
{

    private Rigidbody myBody;

    private Player player;

    Vector3 lastPos;



    // Use this for initialization
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        lastPos = gameObject.transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        float valor = 0;

        if (other.transform.name == "PlayerDireita" || other.transform.name == "PlayerEsquerda")
        {
            // print("COLIDIU");
            if (other.transform.position.y < gameObject.transform.position.y)
            {
                //("OTHER - " + other.transform.position.y + " " + other.transform.name);
                //print("GAME OBJECT - " + gameObject.transform.position.y + " " + gameObject.transform.name);
                other.gameObject.GetComponent<Player>().ChangeState(Player.playerStates.stun);

                if (myBody.velocity.normalized.x == 0)
                {
                    valor = Random.Range(1f, 2f);

                    //print("VALOR - " + valor);

                    //Vector3 jump = Vector3.up * 10f;

                    if (valor <= 1.5)
                    {
                        Vector3 jump = Vector3.up * 7f;
                        myBody.AddForce(jump, ForceMode.Impulse);

                        //Vector3 horizontal = new Vector3(300f, 0, 0);
                        //myBody.AddForce(horizontal, ForceMode.Force);
                        print("força");
                    }
                    else
                    {
                        Vector3 jump = Vector3.up * 7f;
                        myBody.AddForce(jump, ForceMode.Impulse);

                        //Vector3 horizontal = new Vector3(-300f, 0, 0);
                        //myBody.AddForce(horizontal, ForceMode.Force);
                        print("força");
                    }


                }
                else if (myBody.velocity.normalized.x != 0)
                {
                    Vector3 jump = Vector3.up * 5f;
                    jump += new Vector3(myBody.velocity.normalized.x * 15f, 0, 0);

                    myBody.AddForce(jump, ForceMode.Impulse);
                }

                //SALTAR PARA O LADO QUANDO ESTÁ EM CIMA SEM MEXER NAS TECLAS
            }
        }
    }
}

