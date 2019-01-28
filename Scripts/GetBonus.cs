using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBonus : MonoBehaviour
{

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
            print(other.transform.name + " BONUS");
            Destroy(gameObject);
            
            other.gameObject.GetComponent<Player>().ChangeState(Player.playerStates.bonus);
        }
        
    }
}

