using System.Collections;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

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

    CharacterController controller;
    void Awake() 
    {
        propPhotonView = Prop.GetPhotonView();
        metamorphSmoke = Prop.GetComponent<ParticleSystem>();
        view = GetComponent<PhotonView>();
        PhotonNetwork.OfflineMode = offlinemode;
        controller = Prop.GetComponent<CharacterController>();


    }
void Update()
{
   time += Time.deltaTime;
}

    void FixedUpdate()
    {
        if(!view.IsMine) return;
        Ray ray =  cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        if(hit.collider != null) 
        {
            hit.collider.GetComponent<Outline>()?.ToggleHighlight(false);
        }

        if(Physics.Raycast(ray, out hit, 10, ignoreLayer))
        {
            hit.collider.GetComponent<Outline>()?.ToggleHighlight(true);

            if (Input.GetKey(KeyCode.Tab) && oneRequestBool)
            {

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
        Renderer cloneRenderer = clone.gameObject.GetComponent<Renderer>();
    
        metamorphSmoke.Play();

        propPhotonView.GetComponent<MeshFilter>().mesh = clone.gameObject.GetComponent<MeshFilter>().mesh;
        propPhotonView.GetComponent<Renderer>().materials = cloneRenderer.materials;
        propPhotonView.GetComponent<Renderer>().materials[0].color = cloneRenderer.materials[0].color;
        controller.radius = hit.collider.bounds.extents.x;
        controller.height = hit.collider.bounds.extents.y;
        if (hit.collider.GetType() == typeof(BoxCollider)) controller.center = clone.GetComponent<BoxCollider>().center;
        if (Prop != gameObject)
        {
            Prop.transform.position = transform.position;
            Prop.SetActive(true);
            cam3d.LookAt = Prop.transform;
            cam3d.Follow = Prop.transform;
            cam3d.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.x = 0;
            Destroy(gameObject);
        }
        isTransformed = true;


    }


    IEnumerator MetaCooldown()
    {
        Prop.GetComponent<Renderer>().materials[0].shader = Shader.Find("Standard");
        yield return new WaitForSeconds(3);
        oneRequestBool = true;
    }

    new private void OnEnable()
    {
        if (gameObject == Prop) 
        {
            StartCoroutine(MetaCooldown());
            controller.height = GetComponent<Renderer>().bounds.size.y;
        }
        
    }
}