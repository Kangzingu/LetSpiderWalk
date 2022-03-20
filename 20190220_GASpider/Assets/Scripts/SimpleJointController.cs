using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleJointController : MonoBehaviour
{
    public int population = 10;//개체 수
    public int generation = 30;//세대 수
    public int generationCount = 0;
    private int bitLength;
    public int[,] bit;
    public float[] x;
    public int[,] newGeneration;
    public int mutationProbability = 5;
    // 1000개 비트중 돌연변이 발생

    public int numofSeq = 6;
    private int numofJoint = 3;
    private int numofLeg = 8;

    public GameObject[] spiders=new GameObject[10];
    public bool isCalculateEnd = true;
    public bool isResetManagerOn = false;
    public bool isRoundStart = false;
    public int resetReadyCount = 0;
    public int numofResetReady;
    public int roundStartCount = 0;
    public int numofRoundStart;


    public HingeJoint[] leftFirstLeg = new HingeJoint[3];
    public HingeJoint[] leftSecondLeg = new HingeJoint[3];
    public HingeJoint[] leftThirdLeg = new HingeJoint[3];
    public HingeJoint[] leftFourthLeg = new HingeJoint[3];
    public HingeJoint[] rightFirstLeg = new HingeJoint[3];
    public HingeJoint[] rightSecondLeg = new HingeJoint[3];
    public HingeJoint[] rightThirdLeg = new HingeJoint[3];
    public HingeJoint[] rightFourthLeg = new HingeJoint[3];


    //private int[] highLeftLimit = new int[2] { 23, 30 };
    //private int[] lowLeftLimit = new int[2] { -22, -30 };
    //private int[] highRightLimit = new int[2] { -22, 30 };
    //private int[] lowRightLimit = new int[2] { 23, -30 };
    //35 0
    private int[,] leftBehaveSequence = new int[3, 6] { { 35, 35, 35, -60, -60, -60 }, { -22, -22, 23, 23, 23, -22 }, { -30, 30, 30, 30, -30, -30 } };
    //HHHHLL    LLHHHL    LHHLLL
    private int[,] rightBehaveSequence = new int[3, 6] { { 35, 35, 35, -60, -60, -60 }, { 22, 22, -23, -23, -23, 22 }, { -30, 30, 30, 30, -30, -30 } };
    //from Coxa to Metatarsus

    private JointSpring spr;
    private int seqCount = 0;
    private int sizeofArr = 3;
    private int sizeofSeq = 6;//총 6단계
    private bool isCross = false;

    void Start()
    {
        // 첫 비트 = 랜덤
        // 각각의 팔에 관여, 종합적인 1줄의 비트로..?
        // (마디 3개 * 팔 8개) * 단계는 넉넉잡아 ... 6개
        // ( 3 * 8 ) * 6 = 144 비트...
        // 한 라운드는 동일한 단계를 10번??? 반복
        // 한단계 계산 144비트, 평가 척도 : 원점으로 부터 이동거리

        bitLength = numofJoint * numofLeg * numofSeq;
        bit = new int[population, bitLength];
        newGeneration = new int[population, bitLength];
        x = new float[population];
        numofResetReady =
            (numofLeg *
            (numofJoint + 1) +// include Tibia
            1) *// include body
            population;
        numofRoundStart = population;
        //Debug.Log(numofResetReady);
        //spiders = new GameObject[population];
    }
    private void Update()
    {
        if (isRoundStart)
        {// 세대교체
            // isRoundOngoing에 대한 조건도 추가하면 좋을듯여
            if (isCalculateEnd)
            {   
                isCalculateEnd = false;
                for (int i = 0; i < population; i++)
                {
                    for (int j = 0; j < bitLength; j++)
                    {
                        bit[i, j] = spiders[i].GetComponent<SpiderLegController>().bit[j];
                    }
                    x[i] = spiders[i].GetComponent<SpiderLegController>().result;
                    //Debug.Log(x[i]);
                }
                GeneticAlgorithm(bit, x, population, bitLength, generation, newGeneration, mutationProbability);
                //generation은 지금 당장 필요없어보임;
                for (int i = 0; i < population; i++)
                {
                    for (int j = 0; j < bitLength; j++)
                    {
                        spiders[i].GetComponent<SpiderLegController>().bit[j] = newGeneration[i, j];
                    }
                    /*결과값 초기화 각 컨트롤러에서 해줄거면 뺴면대영*/
                    //spiders[i].GetComponent<SpiderLegController>().result = 0;
                    /*결과값 초기화 각 컨트롤러에서 해줄거면 뺴면대영*/
                }
                isCalculateEnd = true;
                isResetManagerOn = true;
                isRoundStart = false;
            }            
        }
        if (roundStartCount >= numofRoundStart)
        {
            roundStartCount = 0;
            isRoundStart = true;
        }
        if (resetReadyCount >= numofResetReady)
        {
            isResetManagerOn = false;
            for (int i = 0; i < population; i++)
            {
                spiders[i].GetComponent<SpiderLegController>().isRoundOngoing = true;
                //spiders[i].GetComponent<SpiderLegController>().StartRound();
            }
            generationCount++;
            resetReadyCount = 0;
        }

    }
    public void GeneticAlgorithm(int[,] bit, float[] x, int population, int bitLength, int generation, int[,] newGeneration, int mutationProbability)
    {
        // 각각의 거미로부터 비트를 받는다, 거미는 총 10마리?쯤으로
        // bit[10,144] 일듯

        // x는 평가 척도가될듯 : 이동 거리
        
        // population 만큼 다 초기화 시켜야하징 10이 될듯 : 거미 개체수(10)

        // bitLength : 3 * 8 * 6 = 144

        // generation 만큼 반복이지 일단 30세대정도 까지

        // 새로 계산된 세대의 비트는 new generation에 저장해서 각각의 거미 개체에 적용

        // 돌연변이도 넣어야함, 각 단계당 144비트 * 10마리 = 1440비트라고 가정, 0.5%?정도 적당할듯?



        //population 인구수    //generation 세대수

        //int[,] bit = new int[population, bitLength];
        //비트    10101010...
        //float[] x = new float[population];
        //세대 변경에 기준이 되는 우성 값
        float[] expectByX = new float[population];
        //x값으로 예측한 다음세대 뽑힐 넘들 확률
        int[] actualCount = new int[population];
        //실제 뽑힌 인덱스 저장
        int[,] bitActualCount = new int[population, bitLength];
        //실제 뽑힌 넘들 비트 정보
        //int[,] bitNextGen = new int[population, bitLength];
        //다음 세대의 비트 정보

        float[] sortedByX = new float[population];
        int[] sortedIndexByX = new int[population];
        //=========================

        //x[0] = 5.1f;x[1] = 0.2f;x[2] = 0.4f;x[3] = 4.9f;
        //=========================
        float sumFx = 0.0f;
        //x^2의 총합
        float trash = 0.0f;
        //언제든 쓸수있는 쓰레기값


        for (int i = 0; i < population; i++)
        {//우성 정도에 따라 확률 게산할거임
            sumFx += Mathf.Pow(x[i], 2);
            //x^2의 총합을 저장해둠
        }
        for (int i = 0; i < population; i++)
        {//뽑힐 확률 예측해볼거야
            expectByX[i] = Mathf.Pow(x[i], 2) / sumFx;// * (float)population;
            sortedByX[i] = expectByX[i];
            sortedIndexByX[i] = i;
            //뽑힐 확률 expectByX 에 들어가 있음
            //Debug.Log(expectByX[i]);
        }
        //상위 개체 뽑는기능은 추후에 추가 일단은 확률로만  
        //진짜 뽑아볼까
        float sumExpect = 0.0f;
        for (int i = 0; i < population; i++)
        {//다음세대가 될넘들이 누가될지를 뽑을거얌
            float randomCount = (float)(Random.Range(0, population * 100.0f)) / (float)(population * 100.0f);
            //랜덤으로 숫자 돌려서
            //Debug.Log(randomCount);
            for (int j = 0; j < population; j++)
            {//애들 뽑힐 확률 가중치 따라 뽑을거임
                sumExpect += expectByX[j];
                if (sumExpect >= randomCount)
                {//가중치 따라 계산하다가 적중하면
                    actualCount[i] = j;
                    //바로그냥 인덱스 뽑아버리지
                    sumExpect = 0.0f;
                    //다음놈을 뽑기위해 변수 초기화
                    break;
                    //뽑았으니 나가볼까
                }
            }
        }
        //우성인 넘들이 많이 뽑혔겠지?
        for (int i = 0; i < population; i++)
        {
            for (int j = 0; j < bitLength; j++)
            {
                bitActualCount[i, j] = bit[actualCount[i], j];
                if (mutationProbability >= Random.Range(1, 1001))// 1 to 1000
                {
                    bitActualCount[i, j] = (bitActualCount[i, j] + 1) % 2;
                }
            }
        }
        //실제 뽑힌 넘들의 유전자를 다음세대로 물려줄거임
        int randomMateIndex;
        for (int i = 0; i < population / 2; i++)
        {
            randomMateIndex = Random.Range(1, bitLength - 2);
            //ex)bitLength가 8이면 /(/////)/ 욜케 한칸식 띈 인덱스 범위 내부중 한곳 나오게
            for (int j = 0; j < randomMateIndex; j++)
            {
                newGeneration[i, j] = bitActualCount[i, j];
                newGeneration[i + population / 2, j] = bitActualCount[i + population / 2, j];
            }
            for (int j = randomMateIndex; j < bitLength; j++)
            {
                newGeneration[i, j] = bitActualCount[i + population / 2, j];
                newGeneration[i + population / 2, j] = bitActualCount[i, j];
            }
        }

        /* 최상위 결과 5마리를 다음세대에 그대로 추가 */

        // 최상위 5개체 다음세대 5개체의 유전자에 그대로 유전
        // result 값 순으로 정렬
        for (int i = 0; i < population; i++)
        {
            for (int j = i + 1; j < population; j++)
            {
                if (sortedByX[j] > sortedByX[i])
                {
                    trash = sortedByX[i];
                    sortedByX[i] = sortedByX[j];
                    sortedByX[j] = trash;
                    trash = (float)sortedIndexByX[i];
                    sortedIndexByX[i] = sortedIndexByX[j];
                    sortedIndexByX[j] = (int)trash;
                }
            }
        }

        /* 여기까지 이상 무 */
        for (int i = 0; i < population; i++)
        {
            
            //Debug.Log(sortedByX[i] + " ->얘의 정렬 순서가 :" + sortedIndexByX[i] + "번쨰인것"+", 요렇게: "+ x[sortedIndexByX[i]]);
        }


        //for (int i = 0; i < population; i++)
        //{
        //    Debug.Log("bit" + bit[i, 0] +
        //        bit[i, 1] +
        //        bit[i, 2] +
        //        bit[i, 3] +
        //        bit[i, 4] +
        //        bit[i, 5] +
        //        bit[i, 6] +
        //       bit[i, 7]);
        //}
        //for (int i = 0; i < population; i++)
        //{
        //    Debug.Log("bitNG" + bitNextGen[i, 0] +
        //        bitNextGen[i, 1] +
        //        bitNextGen[i, 2] +
        //        bitNextGen[i, 3] +
        //        bitNextGen[i, 4] +
        //        bitNextGen[i, 5] +
        //        bitNextGen[i, 6] +
        //        bitNextGen[i, 7]);
        //}
    }
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.J))
    //    {
    //        for (int i = 0; i < sizeofArr; i++)
    //        {
    //            spr = leftFirstLeg[i].spring;
    //            spr.targetPosition = leftBehaveSequence[i, seqCount % sizeofSeq];
    //            leftFirstLeg[i].spring = spr;
    //            spr = leftThirdLeg[i].spring;
    //            spr.targetPosition = leftBehaveSequence[i, seqCount % sizeofSeq];
    //            leftThirdLeg[i].spring = spr;
    //            spr = rightSecondLeg[i].spring;
    //            spr.targetPosition = rightBehaveSequence[i, seqCount % sizeofSeq];
    //            rightSecondLeg[i].spring = spr;
    //            spr = rightFourthLeg[i].spring;
    //            spr.targetPosition = rightBehaveSequence[i, seqCount % sizeofSeq];
    //            rightFourthLeg[i].spring = spr;

    //            spr = leftSecondLeg[i].spring;
    //            spr.targetPosition = leftBehaveSequence[i, (seqCount + (sizeofSeq / 2)) % sizeofSeq];
    //            leftSecondLeg[i].spring = spr;
    //            spr = leftFourthLeg[i].spring;
    //            spr.targetPosition = leftBehaveSequence[i, (seqCount + (sizeofSeq / 2)) % sizeofSeq];
    //            leftFourthLeg[i].spring = spr;
    //            spr = rightFirstLeg[i].spring;
    //            spr.targetPosition = rightBehaveSequence[i, (seqCount + (sizeofSeq / 2)) % sizeofSeq];
    //            rightFirstLeg[i].spring = spr;
    //            spr = rightThirdLeg[i].spring;
    //            spr.targetPosition = rightBehaveSequence[i, (seqCount + (sizeofSeq / 2)) % sizeofSeq];
    //            rightThirdLeg[i].spring = spr;
    //        }
    //        //1 set end
    //        seqCount++;
    //    }
    //}
}
