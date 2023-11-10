using UnityEngine;
using Photon.Pun;
using Cinemachine;
public class AudioListenerMine : MonoBehaviour
{
    #region vars
    private AudioListener audioListener;
    private PhotonView view;
    [SerializeField] private CinemachineFreeLook cam3d;
    #endregion
    void Start()
    {
        view = GetComponent<PhotonView>();
        audioListener = GetComponent<AudioListener>();

        if (!view.IsMine)
        {
            audioListener.enabled= false;
            cam3d.gameObject.SetActive(false);
            GetComponent<Camera>().enabled = false;
            GetComponent<CinemachineBrain>().enabled = false;
        }
    }
}
