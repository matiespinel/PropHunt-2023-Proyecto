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
    PhotonView propPhotonView;
    [SerializeField] private LineRenderer propGun;
    private float time;
    private ParticleSystem metamorphSmoke;
    public static bool isTransformed = false;
    #endregion
    private RaycastHit hit;
    void Awake() 
    {
        propPhotonView = Prop.GetPhotonView();
        metamorphSmoke = Prop.GetComponent<ParticleSystem>();
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
        if(!photonView.IsMine) return;
        Ray ray =  cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        if(hit.collider != null) 
        {
            hit.collider.GetComponent<Outline>()?.ToggleHighlight(false);
        }

        if(Physics.Raycast(ray, out hit, 10, ignoreLayer))
        {
            hit.collider.GetComponent<Outline>()?.ToggleHighlight(true);
            propGun.SetPosition(0, propGun.transform.position);
            if (Input.GetKey(KeyCode.Tab) && oneRequestBool)
            {
                propGun.enabled = true;
                propGun.SetPosition(1, hit.transform.position);
                oneRequestBool = false;
                view.RPC("Metamorph", RpcTarget.All, hit.collider.GetComponent<PhotonView>().ViewID);
                StartCoroutine(MetaCooldown());
            }
        }
        

    }

    [PunRPC]
    private void Metamorph(int id)
    {
        PhotonView clone = PhotonView.Find(id);
        Debug.Log("Metamorfosis ejecutada");
        Renderer cloneRenderer = clone.gameObject.GetComponent<Renderer>();
    
        metamorphSmoke.Play();
        propPhotonView.GetComponent<MeshFilter>().mesh = clone.gameObject.GetComponent<MeshFilter>().mesh;
        propPhotonView.GetComponent<Renderer>().materials = cloneRenderer.materials;

        if (Prop != gameObject)
        {
            Prop.transform.position = transform.position;
            Prop.SetActive(true);
            cam3d.LookAt = Prop.transform;
            cam3d.Follow = Prop.transform;
            Destroy(gameObject);
        }
        isTransformed = true;
        Prop.GetComponent<Renderer>().materials[0].color = cloneRenderer.materials[0].color;
        cloneRenderer.materials[0].shader = Shader.Find("Standard");
    }


    IEnumerator MetaCooldown()
    {
        yield return new WaitForSeconds(1);
        propGun.enabled = false;
        yield return new WaitForSeconds(8);
        oneRequestBool = true;
    }

}