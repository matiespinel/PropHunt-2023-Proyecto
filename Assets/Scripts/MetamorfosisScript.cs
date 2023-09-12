using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

public class MetamorfosisScript : MonoBehaviourPunCallbacks
{
    #region vars
    [SerializeField] private LayerMask PlayerLayer;// en el inspector pone EVERYTHING excepto la layer de la que forma parte tu gameobj(la asignas vos)
    private Animator animator;
    [SerializeField] private GameObject Target;
    private bool CM = true;
    private PhotonView view;
    [SerializeField]private CinemachineFreeLook cam3d;
    #endregion
    [SerializeField] private bool offlinemode;// offline o online
    void Start() 
    {
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        PhotonNetwork.OfflineMode = offlinemode;
    }
    void FixedUpdate()
    {
        if (view.IsMine)
        {
Ray ray =  new Ray(animator.GetBoneTransform(HumanBodyBones.Head).position, Target.transform.position);
        

        RaycastHit hit;
        //RaycastHit nos permite acceder a informacion sobre el impacto del raycast, como la posicion y normal(rotacion de la superficie impactada)
        if(Physics.Raycast(ray, out hit, PlayerLayer))// PlayerLayer es la capa de la que forma parte el gameobj. en este parametro se inserta la layer que queres ignorar(porque el raycast sale del interior del player y puede colisionar con su propio collider)
        {
            //esta es una representacion grafica del raycast
            if(hit.collider.tag == "Transformable")//hit.collider nos dice con que collider colisiono y con .tag accedemos al tag que le pusimos
            {

                if(Input.GetKey(KeyCode.Tab) && CM == true)
                {
                   CM = false;
                   view.RPC("Metamorph", RpcTarget.All, hit.collider.GetComponent<PhotonView>().ViewID);
                   Debug.Log("bang");
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
        GameObject Prop = MetamorfosisManagerScript.Instance.Instantiate("0", transform.position, transform.rotation);
        Prop.GetComponent<PhotonView>().GetComponent<MeshFilter>().mesh = clone.gameObject.GetComponent<MeshFilter>().mesh;
        Prop.GetComponent<PhotonView>().GetComponent<Renderer>().material = clone.gameObject.GetComponent<Renderer>().material;
        Prop.transform.parent = cam3d.transform.parent;
        cam3d.LookAt = Prop.transform;
        cam3d.Follow = Prop.transform;
        
        Destroy(this.gameObject);
    }

    IEnumerator MetaCooldown()
    {
        yield return new WaitForSeconds(8);
        CM = true;
    }

}