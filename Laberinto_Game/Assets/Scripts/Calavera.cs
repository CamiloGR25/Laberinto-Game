using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calavera : MonoBehaviour
{
    private int velocidadRotacion = 170;
    private AudioSource muerteSonido;
    void Start()
    {
        muerteSonido = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(muerteSonido.clip, transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime); //rotar el objeto
    }
}
