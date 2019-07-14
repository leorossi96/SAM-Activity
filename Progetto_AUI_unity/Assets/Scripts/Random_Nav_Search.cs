using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Nav_Search : MonoBehaviour
{

    public Vector3 targetPos;
    public bool turning;
    public RotationsSlow rotate;
    public Quaternion rotateDirection;
    private float initialX;
    private float initialZ;

    // Use this for initialization
    void Start()
    {

        float initial = Random.Range(0.0f, 50f);

        this.targetPos = transform.position + this.transform.forward * initial;
        initialX = transform.position.x;
        initialZ = transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {


        if (turning)
        {

            this.rotateDirection = Quaternion.LookRotation((targetPos - transform.position).normalized, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, this.rotateDirection, 1.0f);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward * 100, 1.0f);

            if (transform.rotation == rotateDirection)

            {

                turning = false;

            }

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 1.0f);
        }


        if (Vector3.Distance(transform.position, targetPos) < 1.0f)
        {

            float X = Random.Range(-190f, 190f);
            float Z = Random.Range(-195f, 195f);

            this.targetPos = new Vector3(X + initialX, transform.position.y, Z + initialZ);

            turning = true;

            this.rotateDirection = Quaternion.LookRotation((targetPos - transform.position).normalized, transform.up);
        }


    }


}
