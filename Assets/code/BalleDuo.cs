using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalleDuo : MonoBehaviour
{
    Transform transfo;
    Rigidbody2D monRB;
    AudioSource monAS;

    //vitesse
    public float vitesse = 5.0f;

    //limites
    float limiteDroite = 10.0f;
    float limiteGauche = -10.0f;

    //son
    public AudioClip autreSon;
    public AudioClip sonFail;

    //pointage
    public TextMeshPro monTMPDroite;
    public TextMeshPro monTMPGauche;
    public int pointageMaximal = 10;
    int pointsGauche;
    int pointsDroite;

    //Ecrans
    public GameObject EcranAccueil;
    public GameObject JeuDuo;


    // Start is called before the first frame update
    void Awake()
    {
        //Raccourcis
        transfo = gameObject.transform;
        monRB = gameObject.GetComponent<Rigidbody2D>();
        monAS = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //Initialisation de la position et impulsion initiale
        transfo.position = Vector2.zero;
        monRB.velocity = Vector2.zero;
        monRB.AddForce(Vector2.one * vitesse, ForceMode2D.Impulse);

        //initialisation de du nombre de points à droite et à gauche
        pointsDroite = 0;
        pointsGauche = 0;
        monTMPDroite.text = pointsDroite.ToString();
        monTMPGauche.text = pointsGauche.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Gauche gagne un point
        if (transfo.position.x > limiteDroite)
        {
            //réinitialisation de la balle au centre (impulsion vers la palette droite)
            transfo.position = Vector2.zero;
            monRB.velocity = Vector2.zero;
            monRB.AddForce(Vector2.one * vitesse, ForceMode2D.Impulse);

            //ajustement des points
            pointsGauche ++;
            monTMPGauche.text = pointsGauche.ToString();

            //gestion d'écran (affichage du menu si partie terminée)
            if (pointsGauche == pointageMaximal)
            {
                EcranAccueil.SetActive(true);
                JeuDuo.SetActive(false);
            }

            //son quand la balle sort de la limite 
            monAS.PlayOneShot(sonFail);

        }

        //Droite gagne un point
        if (transfo.position.x < limiteGauche)
        {
            //réinitialisation de la balle au centre (impulsion vers la palette gauche)
            transfo.position = Vector2.zero;
            monRB.velocity = Vector2.zero;
            Vector2 monVecteur = new Vector2(1, -1);
            monRB.AddForce(monVecteur * -vitesse, ForceMode2D.Impulse);

            //ajustement des points
            pointsDroite++;
            monTMPDroite.text = pointsDroite.ToString();

            //gestion d'écran (affichage du menu si partie terminée)
            if (pointsDroite == pointageMaximal)
            {
                EcranAccueil.SetActive(true);
                JeuDuo.SetActive(false);
            }

            //son quand la balle sort de la limite 
            monAS.PlayOneShot(sonFail);
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
