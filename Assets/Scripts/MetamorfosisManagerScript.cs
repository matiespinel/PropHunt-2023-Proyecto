using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MetamorfosisManagerScript : MonoBehaviour, IPunPrefabPool
{

   [SerializeField] private GameObject VersatilePropUnit;
   [SerializeField] private List<GameObject> PropPool;
   [SerializeField] private int PropPoolSize = 10;
   private static MetamorfosisManagerScript instance;
   public static MetamorfosisManagerScript Instance { get { return instance;}}

    public void Destroy(GameObject gameObject)
    {

    }


    private void Awake()
   {
       if(instance == null)
       {
        instance = this;
       }
       else
       {
        Destroy(gameObject);
       }
   }
   

    void Start() => AddPropUnitsToPool(PropPoolSize);

    private void AddPropUnitsToPool(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            GameObject PropUnit = Instantiate(VersatilePropUnit);
            //PropUnit.SetActive(false);
            PropPool.Add(PropUnit);
            PropUnit.transform.parent = transform;
        }
    }

    public GameObject Instantiate(string preFabID, Vector3 pos, Quaternion rot)
    {
        for(int i = 0; i < PropPool.Count; i++)
        {
            if(!PropPool[i].activeSelf)
            {
                PropPool[i].SetActive(true);
                PropPool[i].transform.rotation = rot;
                PropPool[i].transform.position = pos;
                return PropPool[i];
            }
        }
        return null;
    }
}
