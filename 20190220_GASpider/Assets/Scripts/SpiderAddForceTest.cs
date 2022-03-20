using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAddForceTest : MonoBehaviour
{
    public Rigidbody[] leftCoxa = new Rigidbody[4];
    public Rigidbody[] leftTrochanter = new Rigidbody[4];
    public Rigidbody[] leftFemur = new Rigidbody[4];
    public Rigidbody[] leftPatella = new Rigidbody[4];
    public Rigidbody[] leftTibia = new Rigidbody[4];
    public Rigidbody[] leftMetatrsus = new Rigidbody[4];
    public Rigidbody[] leftTarsus = new Rigidbody[4];
    public Rigidbody[] leftClaws = new Rigidbody[4];

    public Rigidbody[] RightCoxa = new Rigidbody[4];
    public Rigidbody[] RightTrochanter = new Rigidbody[4];
    public Rigidbody[] RightFemur = new Rigidbody[4];
    public Rigidbody[] RightPatella = new Rigidbody[4];
    public Rigidbody[] RightTibia = new Rigidbody[4];
    public Rigidbody[] RightMetatrsus = new Rigidbody[4];
    public Rigidbody[] RightTarsus = new Rigidbody[4];
    public Rigidbody[] RightClaws = new Rigidbody[4];

    private float power = 100;
    private int sizeofArr = 4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < sizeofArr; i++)
            {//포함 - 안포함
             //x,z축 이동???? y도..
                leftCoxa[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                RightCoxa[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                // -1,0,1 셋중 하나
            }
            for (int i = 0; i < sizeofArr; i++)
            {//포함 - 안포함
             //x,z축 이동???? y도..
                leftTrochanter[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                RightTrochanter[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                // -1,0,1 셋중 하나
            }
            for (int i = 0; i < sizeofArr; i++)
            {//포함 - 안포함
             //x,z축 이동???? y도..
                leftFemur[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                RightFemur[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                // -1,0,1 셋중 하나
            }
            for (int i = 0; i < sizeofArr; i++)
            {//포함 - 안포함
             //x,z축 이동???? y도..
                leftPatella[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                RightPatella[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                // -1,0,1 셋중 하나
            }
            for (int i = 0; i < sizeofArr; i++)
            {//포함 - 안포함
             //x,z축 이동???? y도..
                leftTibia[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                RightTibia[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                // -1,0,1 셋중 하나
            }
            for (int i = 0; i < sizeofArr; i++)
            {//포함 - 안포함
             //x,z축 이동???? y도..
                leftMetatrsus[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                RightMetatrsus[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                // -1,0,1 셋중 하나
            }
            for (int i = 0; i < sizeofArr; i++)
            {//포함 - 안포함
             //x,z축 이동???? y도..
                leftTarsus[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                RightTarsus[i].AddRelativeForce(
                    new Vector3(
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power,
                        Random.Range(-1, 2) * Random.Range(1, 6) * power));
                // -1,0,1 셋중 하나
            }
        }
    }

}
