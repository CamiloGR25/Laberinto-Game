using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource coinSound;

    // Start is called before the first frame update
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
}
