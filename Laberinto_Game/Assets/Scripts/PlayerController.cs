using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    //public Transform particulas;
    //private ParticleSystem systemaparticula;
    private Rigidbody rb;
    private Vector3 posicion;
    public float speed;
    // private AudioSource audioRecoleccion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /*systemaparticula = particulas.GetComponent<ParticleSystem>();
        systemaparticula.Stop();
        audioRecoleccion = GetComponent<AudioSource>();*/
    }

    // Update is called once per frame
    void Update()
    {

    }


    //OnTriggerEnter cuando colisiona con el colaider, other es el objeto del colaider
    /* void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.CompareTag("Recolectable"))
         {
             posicion = other.gameObject.transform.position;
             particulas.position = posicion;
             systemaparticula = particulas.GetComponent<ParticleSystem>();
             systemaparticula.Play();
             other.gameObject.SetActive(false); //oculta el cubo
             audioRecoleccion.Play();
             cont += 1;
             if (cont == 12)
             {
                 Debug.Log("yaaa");
                 SceneManager.LoadScene(1);
             }
         }

     }*/

    void FixedUpdate()
    {//movimiento:
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movimiento * speed); //se mueve con la velocidad constante
    }
}
