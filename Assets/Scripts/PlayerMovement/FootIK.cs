using UnityEngine;

public class FootIK : MonoBehaviour
{
    [SerializeField] float DistanceToGround;
    [SerializeField] LayerMask playermask;
    Animator animator;
    // Start is called before the first frame update
    void Start() => animator = GetComponent<Animator>();

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex) 
    {
        
        RaycastHit hit;
        if(animator)
        {
            Ray RFray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            Ray LFray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            #region weights
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, animator.GetFloat("IKRFWeight"));
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, animator.GetFloat("IKRFWeight"));
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("IKLFWeight"));
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("IKLFWeight"));
            #endregion

            if (Physics.Raycast(RFray, out hit, DistanceToGround + 1f, playermask))
            {
                if(hit.transform.tag == "fis")
                {
                    
                    Vector3 RFootPos = hit.point;
                    RFootPos.y += DistanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, RFootPos);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
            
            if(Physics.Raycast(LFray, out hit, DistanceToGround + 1f, playermask))
            {
                Vector3 LFootPos = hit.point;
                LFootPos.y += DistanceToGround;
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, LFootPos);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
            }
        }
        
        
    }
}
