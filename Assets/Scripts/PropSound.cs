using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float timer = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (!audioSource.isPlaying && timer <= 0)
        {
            audioSource.Play();
            timer = 15;
        }
    }
}
