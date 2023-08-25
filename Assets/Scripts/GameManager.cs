using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{


    [SerializeField] GameObject Pared1;
    [SerializeField] GameObject Pared2;
    [SerializeField] GameObject Pared3;
    [SerializeField] AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        // Hacer coroutine para destruir las paredes
        StartCoroutine("destruirParedes");

    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
     IEnumerator destruirParedes()
    {
        yield return new WaitForSeconds(15);
        audioSource.Play();
        Pared1.gameObject.SetActive(false);
        Pared2.gameObject.SetActive(false);
        Pared3.gameObject.SetActive(false);
    }
}
