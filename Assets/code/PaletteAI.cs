using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteAI : MonoBehaviour
{
    Transform transfo;
    float limiteDeplacement = 3.5f;
    public float vitesseDeplacement = 1.0f;

    //Balle
    public GameObject balleAI;
    float posBalleY;
    public float dumbLvl;

    // Start is called before the first frame update
    void Start()
    {
        //raccourci
        transfo = gameObject.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Trouver la position de la balle en y
        posBalleY = balleAI.transform.position.y;

        //lire la position de l'objet
        Vector3 laPos = transfo.position;

        //calculer le déplacement * l'"handicap"
        laPos.y = posBalleY * dumbLvl;

        //calculer les limites
        laPos.y = Mathf.Min(laPos.y, limiteDeplacement);
        laPos.y = Mathf.Max(laPos.y, -limiteDeplacement);

        //modifier la position de la palette
        transfo.position = laPos;
    }
}
