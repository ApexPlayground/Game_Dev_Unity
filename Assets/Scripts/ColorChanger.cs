using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ColorChanger : MonoBehaviour
{ 
    public Material randomColors;

   
    // Start is called before the first frame update
    void Start()
    {
        //getting material color from the game 
        randomColors = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //changes randomColor variable to a random color for every collision 
        randomColors.color = Random.ColorHSV();
    }
}
