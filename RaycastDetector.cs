using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetamorfosisScript : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask PlayerLayer;// en el inspector pone EVERYTHING excepto la layer de la que forma parte tu gameobj(la asignas vos)
    Animator animator;
    Vector3 HeadDirection;
    public Camera Aim;
    public GameObject Target;
    void Start() 
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //creamos rayo, se proyecta desde la posicion del transform del gameobj que tiene el script, y tiene como direccion hacia el frente
        Ray ray =  new Ray(Aim.transform.position, Target.transform.position);
        
       
        RaycastHit hit;
        //RaycastHit nos permite acceder a informacion sobre el impacto del raycast, como la posicion y normal(rotacion de la superficie impactada)
        if(Physics.Raycast(ray, out hit, PlayerLayer))// PlayerLayer es la capa de la que forma parte el gameobj. en este parametro se inserta la layer que queres ignorar(porque el raycast sale del interior del player y puede colisionar con su propio collider)
        {
            //esta es una representacion grafica del raycast
            Debug.DrawRay(ray.origin, ray.direction, Color.black);
            Debug.Log("tdftfg");
            if(hit.collider.tag == "Transformable")//hit.collider nos dice con que collider colisiono y con .tag accedemos al tag que le pusimos
            {
                Debug.Log("Transformable detectado");
                if(Input.GetKey(KeyCode.Tab))
                {
                    Destroy(this.gameObject);
                }
                //borrar control humanoide e insertar control objeto
                //Destroy(GetComponent<CapsuleCollider>()); 
                //decidir despues si es mejor activar y desactivar componentes o añadir y borrar

            }
        }
    }
}
