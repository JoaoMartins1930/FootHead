using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI clockText;
    [SerializeField]
    private float mainTimer;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    // Use this for initialization
    void Start()
    {

        timer = mainTimer;
    }

    // Update is called once per frame
    void Update()
    {

        if (timer >= 0.0f && canCount == true)
        {
            timer -= Time.deltaTime;
            clockText.text = timer.ToString("F");
        }
        else if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            clockText.text = "0.00";
            timer = 0.0f;
        }
    }
}

