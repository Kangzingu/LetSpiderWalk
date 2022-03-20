using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text generation;
    public Text bestGene;
    public Text bestResult;
    private SimpleJointController controllerGA;
    // Start is called before the first frame update
    void Start()
    {
        controllerGA=GetComponent<SimpleJointController>();
    }

    // Update is called once per frame
    void Update()
    {
        generation.text = "세대: "+controllerGA.generationCount;
        /* 추가해야 합니당 ^^^ */
        bestGene.text = "우성 유전자: ";
        bestResult.text = "최대 이동 거리: ";


    }
}
