using UnityEngine;

public class LuzParpadea : MonoBehaviour
{

    [SerializeField] Light luz;
    [SerializeField] Light luz2;
    [SerializeField] Light luz3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // hacer secuencia de luz que parpadea
        luz.intensity = Random.Range(0.5f, 5f);
        luz2.intensity = Random.Range(0.5f, 5f);
        luz3.intensity = Random.Range(0.5f, 5f);

    }
}
