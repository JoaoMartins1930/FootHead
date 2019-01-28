using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraShake : MonoBehaviour
{

    public IEnumerator CameraShakeMethod(float _duration, float _amount)    // precisamos de duraçao e tamanho do shake e iniciamos aqui
    {
        // yield return new WaitForSeconds(3f);  // temos sempre de por isto do yield return +ara o Enumerator nao dar erro

        float startTime = Time.time;
        Vector3 startPos = transform.position;


        while (Time.time < startTime + _duration)   // ver se o tempo que temos é menos que o tempo inical mais a duraçao
        {
            float newPosX = Random.Range(-1f, 1f) * _amount;
            float newPosY = Random.Range(-1f, 1f) * _amount;
            Vector3 newPos = new Vector3(newPosX, newPosY);
            Vector3 finalPos = startPos + newPos * Time.deltaTime;

            transform.position = finalPos;


            // este return vai fazer com que while passem por varios frames
            yield return null;
        }



    }
}
