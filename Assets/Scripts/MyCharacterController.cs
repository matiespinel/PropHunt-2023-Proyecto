using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyCharacterController : MonoBehaviour
{
    [SerializeField]float speed = 10;
    [SerializeField]float rSpeed = 10;
    private PhotonView view;
    [SerializeField] AudioSource silvido;
    
    void Update()
    {

        if (MetamorfosisScript.isTransformed)
        {
            silvido.Play();
        }
        if (view.IsMine) 
        {

            if(Input.GetKey(KeyCode.W))
            {
                transform.Translate(0,0,speed * Time.fixedDeltaTime);
            }

            if(Input.GetKey(KeyCode.A))
            {
                transform.Translate(-speed * Time.fixedDeltaTime,0,0);
            }

            if(Input.GetKey(KeyCode.S))
            {
                transform.Translate(0,0,-speed * Time.fixedDeltaTime);
            }

            if(Input.GetKey(KeyCode.D))
            {
                transform.Translate(speed * Time.fixedDeltaTime,0,0);
            }

            if(Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0,rSpeed,0);
            }

            if(Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0,-rSpeed,0);
            }

        }
        else 
        {
            this.enabled = false;
        }

    }
}
