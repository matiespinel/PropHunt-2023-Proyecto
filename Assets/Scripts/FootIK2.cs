using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootIK2 : MonoBehaviour
{
    public float DistanceToGround;
    public LayerMask layermask;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //RFoot = Animator.GetBoneTransform(HumanBodyBones.RightFoot);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex) 
    {
        
        RaycastHit hit;
        if(animator)
        {
            Ray RFray = new Ray(animator.GetBoneTransform(HumanBodyBones.RightLowerLeg).position + Vector3.up, Vector3.down);
            Debug.DrawRay(RFray.origin, RFray.direction * 30f, Color.red);
            /* Ray LFray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down); */
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, animator.GetFloat("IKRFWeight"));
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, animator.GetFloat("IKRFWeight"));
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("IKLFWeight"));
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("IKLFWeight"));

            if(Physics.Raycast(RFray, out hit, DistanceToGround + 1f, layermask))
            {
                if(hit.transform.tag == "fis")
                {
                    
                    Vector3 RFootPos = hit.point;
                    RFootPos.y += DistanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, RFootPos);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            }
            
           /*  if(Physics.Raycast(LFray, out hit, DistanceToGround + 1f, layermask))
            {
                if(hit.transform.tag == "fis")
                {
                    Vector3 LFootPos = hit.point;
                    LFootPos.y += DistanceToGround;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, LFootPos);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
                }
            } */
        }
        
        
    }
}