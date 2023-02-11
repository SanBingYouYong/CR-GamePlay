using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Protagonist protagonist;

    // Start is called before the first frame update
    void Start()
    {
        protagonist = GameObject.Find("HullProtagonist").GetComponent<Protagonist>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Slider>().value = protagonist.health;
    }
}
