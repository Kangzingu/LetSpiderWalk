using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    private Vector3 initPos;
    private Vector3 initVel;
    private Quaternion initRot;
    private Rigidbody body;
    private HingeJoint joint;
    private JointSpring spr;

    public GameObject controllerGA;
    public GameObject controllerLeg;

    private bool isReseted = false;

    private float countStabilizeTime;
    private float resetStabilizeTime = 3f;
    // Start is called before the first frame update
    void Awake()
    {
        initPos = this.transform.position;
        initRot = this.transform.rotation;
        body = GetComponent<Rigidbody>();
        initVel = body.velocity;
        if (GetComponent<HingeJoint>() != null)
            joint=GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        // GA와 초기화가 동시에 일어남을 방지하기 위해
        // 엇갈려서 GA연산 후 초기화가 일어나도록
        // 근데 GA계산이 넘 빨라서 그 사이에 isCalculateEnd값을 잡아내질 못하는중
        if ((controllerLeg.GetComponent<SpiderLegController>().isRoundOngoing))
        {
            body.velocity = new Vector3(0, 0, 0);
            this.transform.position = initPos;
            this.transform.rotation = initRot;
            if (GetComponent<HingeJoint>() != null)
            {
                spr = joint.spring;
                spr.targetPosition = 0;
                joint.spring = spr;
            }
            body.isKinematic = true;
        }
        else if((!controllerLeg.GetComponent<SpiderLegController>().isRoundOngoing) &&
            (body.isKinematic))
        {
            body.isKinematic = false;
        }
        if ((controllerGA.GetComponent<SimpleJointController>().isResetManagerOn) &&
            (!isReseted))
        {
            body.isKinematic = true;
            body.velocity = new Vector3(0, 0, 0);
            this.transform.position = initPos;
            this.transform.rotation = initRot;
            if (GetComponent<HingeJoint>() != null)
            {
                spr = joint.spring;
                spr.targetPosition = 0;
                joint.spring = spr;
            }
            isReseted = true;
            StartCoroutine(sleeping(controllerGA,body,this.gameObject));
        }
        else if ((!controllerGA.GetComponent<SimpleJointController>().isResetManagerOn) &&
            (isReseted))
        {
            isReseted = false;
        }
        IEnumerator sleeping(GameObject ControllerGA,Rigidbody body, GameObject inputThis)
        {
            yield return new WaitForSeconds(2.0f);
            body.velocity = new Vector3(0, 0, 0);
            this.transform.position = initPos;
            this.transform.rotation = initRot;
            body.isKinematic = false;
            controllerGA.GetComponent<SimpleJointController>().resetReadyCount++;
        }
    }
}
