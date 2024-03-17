using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalleSolo : MonoBehaviour
{
    Transform transfo;
    Rigidbody2D monRB;
    AudioSource monAS;
    float limiteDroite = 10.0f;

    //vitesse
    public float vitesse = 5.0f;
    public float augmentationVitesse = 0.1f;

    //son
    public AudioClip autreSon;
    public AudioClip sonFail;

    //pointage
    public TextMeshPro monTMP;
    public int pointageInitial = 10;
    int points;

    //Ecrans
    public GameObject EcranAccueil;
    public GameObject JeuSolo;


    // Start is called before the first frame update
    void Awake()
    {
        //raccourcis
        transfo = gameObject.transform;
        monRB = gameObject.GetComponent<Rigidbody2D>();
        monAS = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //Réinitialisation de la position et impulsion initiale
        transfo.position = Vector2.zero;
        monRB.velocity = Vector2.zero;
        monRB.AddForce(Vector2.one * vitesse, ForceMode2D.Impulse);

        //initialisation de du nombre de points
        points = pointageInitial;
        monTMP.text = points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //perte de point
        if (transfo.position.x > limiteDroite)  
        {
            //réinitialisation de la balle au centre 
            transfo.position = Vector2.zero;
            monRB.velocity = Vector2.zero;
            monRB.AddForce(Vector2.one * vitesse, ForceMode2D.Impulse);

            //ajustement des points
            points--;
            monTMP.text = points.ToString();

            //gestion d'écran
            if(points == 0)
            {
                EcranAccueil.SetActive(true);
                JeuSolo.SetActive(false);
            }

            //son quand la balle sort de la limite 
            monAS.PlayOneShot(sonFail);

            //augmentation de la vitesse de 10% et vérification dasn la console
            vitesse = vitesse + (vitesse * augmentationVitesse);
            print(vitesse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //son pour collision de la palette 
        if (collision.transform.name == "palette")
        {
            monAS.PlayOneShot(autreSon);
        }
        else
        {
            monAS.Play();
        }
        
    }
}
