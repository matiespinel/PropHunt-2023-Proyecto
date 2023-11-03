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
    [SerializeField] private LineRenderer propGun;
    private float time;
    private ParticleSystem metamorphSmoke;
    public static bool isTransformed = false;
    #endregion
    private RaycastHit hit;
    void Awake() 
    {
        metamorphSmoke = GetComponent<ParticleSystem>();
        view = GetComponent<PhotonView>();
        PhotonNetwork.OfflineMode = offlinemode;
    }
void Update()
{
   time += Time.deltaTime;
    if (isTransformed)
    {
       

        //hacer contador de cada 15 segundos
        if (time%15 == 0)
        {
        
        }
    }
}

    void FixedUpdate()
    {

        if (view.IsMine)
        {
            
        Ray ray =  cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        if(hit.collider != null) 
        {
                hit.collider.GetComponent<Outline>()?.ToggleHighlight(false);
        }
        //RaycastHit nos permite acceder a informacion sobre el impacto del raycast, como la posicion y normal(rotacion de la superficie impactada)
        if(Physics.Raycast(ray, out hit, ignoreLayer))// ignoreLayer es la capa de la que forma parte el gameobj. en este parametro se inserta la layer que queres ignorar(porque el raycast sale del interior del player y puede colisionar con su propio collider)
        {
                hit.collider.GetComponent<Outline>()?.ToggleHighlight(true);
                propGun.SetPosition(0, transform.position);
                if (Input.GetKey(KeyCode.Tab) && oneRequestBool && hit.collider.tag == "Transformable")
                {
                    propGun.enabled = true;
                    propGun.SetPosition(1, hit.transform.position);
                    oneRequestBool = false;
                    
                    view.RPC("Metamorph", RpcTarget.All, hit.collider.GetComponent<PhotonView>().ViewID);
                    
                    StartCoroutine(MetaCooldown());
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
        metamorphSmoke.Play();
        Prop.GetComponent<PhotonView>().GetComponent<MeshFilter>().mesh = clone.gameObject.GetComponent<MeshFilter>().mesh;
        Prop.GetComponent<PhotonView>().GetComponent<Renderer>().material = clone.gameObject.GetComponent<Renderer>().material;
        if (Prop != this.gameObject)
        {
            Prop.transform.position = transform.position;
            Prop.SetActive(true);
            cam3d.LookAt = Prop.transform;
            cam3d.Follow = Prop.transform;
            cam3d.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = 0.35f;
            Destroy(this.gameObject);
        }
        isTransformed = true;
       


 
    }


    IEnumerator MetaCooldown()
    {
        propGun.enabled = false;
        yield return new WaitForSeconds(8);
        oneRequestBool = true;
    }

}