using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private int velocidadRotacion = 170;
    private AudioSource PuertaAbiertaSonido;
    void Start()
    {
        PuertaAbiertaSonido = GameObject.Find("Explosion Sonido").GetComponent<AudioSource>();//traer el componente por nombre del objeto

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(PuertaAbiertaSonido.clip, transform.position);
            GameObject puerta = GameObject.FindWithTag("Puerta");
            puerta.SetActive(false);
            GameObject luz = GameObject.Find("Luz Llave");
            luz.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back, velocidadRotacion * Time.deltaTime); //rotar el objeto
    }
}
