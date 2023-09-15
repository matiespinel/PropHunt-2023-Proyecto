using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

public class MetamorfosisScript : MonoBehaviourPunCallbacks
{
    #region vars
    [SerializeField] private LayerMask ignoreLayer;// en el inspector pone EVERYTHING excepto la layer de la que forma parte tu gameobj(la asignas vos)
    [SerializeField] private Camera cam;
    private bool oneRequestBool = true;
    private PhotonView view;
    [SerializeField] private CinemachineFreeLook cam3d;
    [SerializeField] private bool offlinemode;// offline o online
    [SerializeField] private GameObject Prop;
    #endregion
    void Start() 
    {

        view = GetComponent<PhotonView>();
        PhotonNetwork.OfflineMode = offlinemode;
    }
    void FixedUpdate()
    {

        if (view.IsMine)
        {
            
        Ray ray =  cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        

        RaycastHit hit;
        //RaycastHit nos permite acceder a informacion sobre el impacto del raycast, como la posicion y normal(rotacion de la superficie impactada)
        if(Physics.Raycast(ray, out hit, ignoreLayer))// ignoreLayer es la capa de la que forma parte el gameobj. en este parametro se inserta la layer que queres ignorar(porque el raycast sale del interior del player y puede colisionar con su propio collider)
        {
            //esta es una representacion grafica del raycast
            if(hit.collider.tag == "Transformable")//hit.collider nos dice con que collider colisiono y con .tag accedemos al tag que le pusimos
            {

                if(Input.GetKey(KeyCode.Tab) && oneRequestBool == true)
                {
                   oneRequestBool = false;
                   
                   view.RPC("Metamorph", RpcTarget.All, hit.collider.GetComponent<PhotonView>().ViewID);
                   StartCoroutine(MetaCooldown());
                }
            }
        }
        }
        //creamos rayo, se proyecta desde la posicion del transform del gameobj que tiene el script, y tiene como direccion hacia el frente
        

    }

    [PunRPC]
    private void Metamorph(int id)
    {
        PhotonView clone = PhotonView.Find(id);
        Debug.Log("Metamorfosis ejecutada");
        Prop.GetComponent<PhotonView>().GetComponent<MeshFilter>().mesh = clone.gameObject.GetComponent<MeshFilter>().mesh;
        Prop.GetComponent<PhotonView>().GetComponent<Renderer>().material = clone.gameObject.GetComponent<Renderer>().material;
        if (Prop != this.gameObject)
        {
            Prop.transform.position = transform.position;
            Prop.SetActive(true);
            cam3d.LookAt = Prop.transform;
            cam3d.Follow = Prop.transform;
            Destroy(this.gameObject);
        }
       


 
    }


    IEnumerator MetaCooldown()
    {
        yield return new WaitForSeconds(8);
        oneRequestBool = true;
    }

}