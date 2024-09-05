using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource coinSound;
    private int velocidadRotacion = 220;

    void Start()
    {
        coinSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //coinSound.Play();
            gameObject.SetActive(false);//esconde la moneda
            AudioSource.PlayClipAtPoint(coinSound.clip, transform.position);//da el sonido
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime); //rotar el objeto
    }
}
