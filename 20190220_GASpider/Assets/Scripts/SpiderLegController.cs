using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegController : MonoBehaviour
{
    public int[] bit;

    /* 측정 기준이 되는 값, 실제 실행시 출발선으로 부터 거리로 바꿔야해여*/
    public float result = 1;
    /* 측정 기준이 되는 값, 실제 실행시 출발선으로 부터 거리로 바꿔야해여*/

    public int numofSeq = 6;
    public int numofRound = 3;
    private int numofJoint = 3;
    private int numofLeg = 8;

    private int bitLength;

    private int trash = 0;//언제든지 쓸수있는 쓰레기값
    private int seqCount = 0;
    private int roundCount=0;

    public float countTime = 0;
    public float perSeqTime = 3f;

    public float countStabilizeTime = 0;
    public float roundStabilizeTime = 3f;

    public bool isRoundOngoing = false;
    public bool isRoundStabilizeEnd = false;
    /* 동적할당 불가능 so 다리, 조인트 갯수 바뀔때마다 바꿔줘야해여 */
    public Rigidbody body;
    public HingeJoint[] joints = new HingeJoint[8 * 3];
    public int[] highLimit = new int[3] { 35, 23, 30 };
    public int[] lowLimit = new int[3] { -35, -23, -30 };
    private JointSpring spr;

    public GameObject controllerGA;
    /* 동적할당 불가능 so 다리, 조인트 갯수 바뀔때마다 바꿔줘야해여 */

    //  H   L
    //  35  -60
    //  23  -23
    //  30  -30

    //public bool isSeqEnd;

    // Start is called before the first frame update
    void Start()
    {

        // 첫비트 랜덤생성
        bitLength = numofJoint * numofLeg * numofSeq;
        bit = new int[bitLength];
        for (int i = 0; i < bitLength; i++)
        {
            bit[i] = Random.Range(0, 2);
            //Debug.Log(bit[i]);
        }
        isRoundOngoing = true;
        //isSeqEnd = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isRoundOngoing)
        {
            countStabilizeTime += Time.deltaTime;
            if (countStabilizeTime >= roundStabilizeTime)
            {
                isRoundStabilizeEnd = true;
                isRoundOngoing = false;
                countStabilizeTime = 0;
            }
        }
        if (isRoundStabilizeEnd)
        {
            // 시간 측정
            countTime += Time.deltaTime;
            if (countTime >= perSeqTime)
            {
                // 모든다리에서 하나씩 동작 실행
                // (24개의 관절)
                for (int i = 0; i < numofLeg * numofJoint; i++)
                {
                    if (bit[seqCount * (numofLeg * numofJoint) + i] == 1)
                    {
                        spr = joints[i].spring;
                        spr.targetPosition = highLimit[i % numofJoint];
                        joints[i].spring = spr;
                    }
                    else//bit[seqCount * (numofLeg * numofJoint) + i] == 0
                    {
                        spr = joints[i].spring;
                        spr.targetPosition = lowLimit[i % numofJoint];
                        joints[i].spring = spr;
                    }
                }
                countTime = 0;
                seqCount++;
                // 0~23 24~47 ... 120~143
                // (1)   (2)  ...   (6)
                // 6번 진행
            }
            //동작을 6번( 사실상 == numofSeq ) 만큼 진행시 종료
            if (seqCount >= numofSeq)
            {
                //isRoundOngoing = false;
                countTime = 0;
                seqCount = 0;
                roundCount++;
            }
            if (roundCount >= numofRound)
            {
                countTime = 0;
                roundCount = 0;
                isRoundStabilizeEnd = false;
                StartCoroutine(sleeping(controllerGA));
            }
        }
        // 결과 저장
        result = body.transform.position.x;
        if (result < 0)
            result = 0;
    }
    IEnumerator sleeping(GameObject ControllerGA)
    {
        yield return new WaitForSeconds(2.5f);
        controllerGA.GetComponent<SimpleJointController>().roundStartCount++;
    }
}
