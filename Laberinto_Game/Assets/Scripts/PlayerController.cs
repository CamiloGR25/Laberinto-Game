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
    public float speed = 8;
    private AudioSource golpePared;
    private int puntaje = 0;
    public AudioSource explosion;
    private ParticleSystem explosionParticulas;
    private GameObject paredTrampa1;
    private GameObject paredTrampa2;
    public Transform puntoAparicion;
    GameObject[] plataformas;
    GameObject identificadorDestruir;
    private ParticleSystem victoriaParticulas;
    private TimeController tiempoJuego;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PUNTAJE TOTAL: " + puntaje + " PUNTOS");

        rb = GetComponent<Rigidbody>(); //trae componente del rigibody

        golpePared = GetComponent<AudioSource>(); //trae componente de audio

        sistemaparticula = particulas.GetComponent<ParticleSystem>();//trae el componente de la particula
        sistemaparticula.Stop(); //para la particula

        explosion = GameObject.Find("Explosion Sonido").GetComponent<AudioSource>(); //traer el componente por nombre del objeto

        explosionParticulas = GameObject.FindWithTag("Explosion").GetComponent<ParticleSystem>();
        explosionParticulas.Stop();

        plataformas = GameObject.FindGameObjectsWithTag("Plataformas");//guardar varios objetos por su tag
        identificadorDestruir = GameObject.FindWithTag("Destruir");//guardar un solo objeto por su tag

        paredTrampa1 = GameObject.FindWithTag("ParedTrampa");
        paredTrampa2 = GameObject.FindWithTag("ParedTrampa2");
        paredTrampa1.SetActive(false);
        paredTrampa2.SetActive(false);

        victoriaParticulas = GameObject.FindWithTag("Victoria").GetComponent<ParticleSystem>();
        victoriaParticulas.Stop();

        tiempoJuego = GameObject.Find("Temporizador").GetComponent<TimeController>();
        tiempoJuego.StartTime();
    }

    //detectar la pared con el collider al chocar
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pared") || collision.gameObject.CompareTag("ParedTrampa") || collision.gameObject.CompareTag("ParedTrampa2"))
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
            Debug.Log("Se ha sumado 2 puntos por la MONEDA");
            puntaje += 2;
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
            foreach (GameObject plataforma in plataformas)
            {
                plataforma.SetActive(false);//ocultar plataformas
            }

            identificadorDestruir.SetActive(false);
            explosion.Play();
            explosionParticulas.Play();

            StartCoroutine(PausarParticulas());//llamar el metodo para hacer algo desoues de x segundos
        }

        if (other.gameObject.CompareTag("Trampa"))
        {
            paredTrampa1.SetActive(true);
            paredTrampa2.SetActive(true);
        }

        if (other.gameObject.CompareTag("Muerte"))
        {
            foreach (GameObject plataforma in plataformas)
            {
                plataforma.SetActive(true);//ocultar plataformas
            }
            paredTrampa1.SetActive(false);
            paredTrampa2.SetActive(false);
            speed = 0;
            rb.position = puntoAparicion.position;//aparecer en el punto del respawn o inicio
            identificadorDestruir.SetActive(true);
            StartCoroutine(AjustarVelocidad());
        }

        if (other.gameObject.CompareTag("Ganar"))
        {
            victoriaParticulas.Play();
            AudioSource audioGanar = other.gameObject.GetComponent<AudioSource>();
            tiempoJuego.StopTime();
            var finTiempo = tiempoJuego.GetTime();
            Debug.Log("El tiempo transcurrido para pasar el laberinto fue: " + finTiempo + " Segundos");
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(audioGanar.clip, transform.position);
        }

    }
    //Metodo para hacer algo despues de x segundos
    private IEnumerator PausarParticulas()
    {
        yield return new WaitForSeconds(4f);//cantidad de tiempo de espera para ejecutar
        explosionParticulas.Stop();
        Debug.Log("Se pauso particulas explosion");
    }

    private IEnumerator AjustarVelocidad()
    {
        yield return new WaitForSeconds(2f);
        speed = 8;
    }
    void FixedUpdate()
    {//movimiento:
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movimiento * speed); //se mueve con la velocidad constante
    }
}
