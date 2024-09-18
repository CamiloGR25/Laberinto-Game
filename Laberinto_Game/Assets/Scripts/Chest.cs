using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private AudioSource audioCofre;
    void Start()
    {
        audioCofre = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);//esconde el cofre
            AudioSource.PlayClipAtPoint(audioCofre.clip, transform.position);//da el sonido
        }
    }

}

