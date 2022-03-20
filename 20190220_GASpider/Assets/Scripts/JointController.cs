using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{
    public HingeJoint[] leftFirstLeg = new HingeJoint[6];
    public HingeJoint[] leftSecondLeg = new HingeJoint[6];
    public HingeJoint[] leftThirdLeg = new HingeJoint[6];
    public HingeJoint[] leftFourthLeg = new HingeJoint[6];
    public HingeJoint[] rightFirstLeg = new HingeJoint[6];
    public HingeJoint[] rightSecondLeg = new HingeJoint[6];
    public HingeJoint[] rightThirdLeg = new HingeJoint[6];
    public HingeJoint[] rightFourthLeg = new HingeJoint[6];

    private int[] highLimit = new int[6] { 0, 25, 40, 20, 110, 0 };
    private int[] lowLimit = new int[6] { -60, -80, -85, 0, 2, -80 };
    //from Coxa to Metatarsus

    private JointSpring spr;
    private bool isHighLimit = false;

    private int sizeofArr = 3;
    void Start()
    {
    } 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isHighLimit)//스프링 스트레칭 테스트
            {
                for (int i = 0; i < sizeofArr; i++)
                {
                    spr = leftFirstLeg[i].spring;
                    spr.targetPosition = highLimit[i];
                    leftFirstLeg[i].spring = spr;
                    spr = leftSecondLeg[i].spring;
                    spr.targetPosition = highLimit[i];
                    leftSecondLeg[i].spring = spr;
                    spr = leftThirdLeg[i].spring;
                    spr.targetPosition = highLimit[i];
                    leftThirdLeg[i].spring = spr;
                    spr = leftFourthLeg[i].spring;
                    spr.targetPosition = highLimit[i];
                    leftFourthLeg[i].spring = spr;
                    spr = rightFirstLeg[i].spring;
                    spr.targetPosition = highLimit[i];
                    rightFirstLeg[i].spring = spr;
                    spr = rightSecondLeg[i].spring;
                    spr.targetPosition = highLimit[i];
                    rightSecondLeg[i].spring = spr;
                    spr = rightThirdLeg[i].spring;
                    spr.targetPosition = highLimit[i];
                    rightThirdLeg[i].spring = spr;
                    spr = rightFourthLeg[i].spring;
                    spr.targetPosition = highLimit[i];
                    rightFourthLeg[i].spring = spr;
                }
                isHighLimit = !isHighLimit;
            }
            else
            {
                for (int i = 0; i < sizeofArr; i++)
                {
                    spr = leftFirstLeg[i].spring;
                    spr.targetPosition = lowLimit[i];
                    leftFirstLeg[i].spring = spr;
                    spr = leftSecondLeg[i].spring;
                    spr.targetPosition = lowLimit[i];
                    leftSecondLeg[i].spring = spr;
                    spr = leftThirdLeg[i].spring;
                    spr.targetPosition = lowLimit[i];
                    leftThirdLeg[i].spring = spr;
                    spr = leftFourthLeg[i].spring;
                    spr.targetPosition = lowLimit[i];
                    leftFourthLeg[i].spring = spr;
                    spr = rightFirstLeg[i].spring;
                    spr.targetPosition = lowLimit[i];
                    rightFirstLeg[i].spring = spr;
                    spr = rightSecondLeg[i].spring;
                    spr.targetPosition = lowLimit[i];
                    rightSecondLeg[i].spring = spr;
                    spr = rightThirdLeg[i].spring;
                    spr.targetPosition = lowLimit[i];
                    rightThirdLeg[i].spring = spr;
                    spr = rightFourthLeg[i].spring;
                    spr.targetPosition = lowLimit[i];
                    rightFourthLeg[i].spring = spr;
                }
                isHighLimit = !isHighLimit;
            }
        }
    }
}
