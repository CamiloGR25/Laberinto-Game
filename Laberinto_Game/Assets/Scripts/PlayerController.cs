using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public Transform particulas;//trae el objeto particula
    private ParticleSystem sistemaparticula;//sistema de particula
    private Rigidbody rb;
    private Vector3 posicion;
    public float speed;
    private AudioSource golpePared;
    private int puntaje = 0;
    public AudioSource explosion;
    private ParticleSystem explosionParticula1;
    private ParticleSystem explosionParticula2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //trae componente del rigibody
        golpePared = GetComponent<AudioSource>(); //trae componente de audio
        sistemaparticula = particulas.GetComponent<ParticleSystem>();//trae el componente de la particula
        sistemaparticula.Stop(); //para la particula
        explosion = GameObject.Find("Explosion").GetComponent<AudioSource>(); //traer el componente por nombre del objeto
        explosionParticula1 = GameObject.FindWithTag("ExplosionP").GetComponent<ParticleSystem>();
        explosionParticula2 = GameObject.FindWithTag("Explosion2").GetComponent<ParticleSystem>();
        explosionParticula1.Stop();
        explosionParticula2.Stop();

    }

    // Update is called once per frame
    void Update()
    {

    }
    //detectar la pared con el collider
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            golpePared.Play();
        }
    }


    //OnTriggerEnter cuando colisiona con el collider del trigger, other es el objeto del colaider
    void OnTriggerEnter(Collider other)
    {
        //objeto que choco con la etiqueta correspondiente
        if (other.gameObject.CompareTag("Moneda"))
        {
            Debug.Log("Se ha sumado 5 puntos por la MONEDA");
            puntaje += 5;
            Debug.Log("PUNTAJE TOTAL: " + puntaje + " PUNTOS");
        }

        if (other.gameObject.CompareTag("Cofre"))
        {
            posicion = other.gameObject.transform.position; //trae la posicion exacta del objeto que choco
            particulas.position = posicion; //asigna la posicion de la particula a la misma del objeto
            sistemaparticula = particulas.GetComponent<ParticleSystem>();//trae el compnente al sistema particula
            sistemaparticula.Play(); //activa la particula
            Debug.Log("Se ha sumado 25 puntos por el COFRE");
            puntaje += 25;
            Debug.Log("PUNTAJE TOTAL: " + puntaje + " PUNTOS");
        }

        if (other.gameObject.CompareTag("Destruir"))
        {
            GameObject plataforma = GameObject.FindWithTag("Plataformas");//guardar objeto por su tag
            plataforma.SetActive(false);//ocultar plataformas
            other.gameObject.SetActive(false);
            explosion.Play();
            explosionParticula1.Play();
            explosionParticula2.Play();
            StartCoroutine(PausarParticulas());
        }

    }
    //Metodo para hacer algo despues de x segundos
    private IEnumerator PausarParticulas()
    {
        yield return new WaitForSeconds(4f);//cantidad de tiempo de espera para ejecutar
        explosionParticula1.Stop();
        explosionParticula2.Stop();
        Debug.Log("Se pauso particulas explosion");
    }

    void FixedUpdate()
    {//movimiento:
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movimiento * speed); //se mueve con la velocidad constante
    }
}
