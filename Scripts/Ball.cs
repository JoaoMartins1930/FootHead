using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    // Kick
    private float kickForce = 8f;

    private Rigidbody body;

    Vector3 originalPos;

    Vector3 vel;

    void Start()
    {
        originalPos = gameObject.transform.position;

        body = gameObject.GetComponent<Rigidbody>();

        gameObject.SetActive(true);
    }




    void Update()
    {
        vel = body.velocity;
        
        if (vel.y < 0)
        {
            
        }
        else
        {
            body.mass = 0.5f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if ((other.transform.name == "PlayerDireita" || other.transform.name == "PlayerEsquerda") && other.transform.GetComponent<Player>().kicking)
        {
           
            Vector3 direction = (other.transform.position - transform.position).normalized * kickForce;
            direction += Vector3.up * 1f;
            GetComponent<Rigidbody>().AddForce(-direction, ForceMode.Impulse);
        }
        else if (other.transform.name == "BoundaryBarra")
        {
            
            Vector3 directionBarra = other.transform.position;
            directionBarra += Vector3.up;
            Vector3 position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
            GetComponent<Rigidbody>().AddForce(position * 1f, ForceMode.Impulse);
            //GetComponent<Rigidbody>().AddForce(directionBarra, ForceMode.Impulse);
        }
    }

    public void InitialPos(bool goal)
    {
        if (goal)
        {
            
            GetComponent<Rigidbody>().Sleep();
            gameObject.transform.position = originalPos;
            GetComponent<Rigidbody>().WakeUp();
        }
        else
        {
            
        }
    }


    public void Imobile(bool _stopball)
    {
        
        if(_stopball == true)
        {
            
            gameObject.SetActive(false);
        }

        else if(_stopball == false)
        {
            gameObject.SetActive(true);
        }
    }


    public void Gravity(bool _stopball)
    {
      
        if (_stopball == true)
        {
            
            GetComponent<Rigidbody>().useGravity = false;
        }

        else if (_stopball == false)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }


}
