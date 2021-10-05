using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergePice : MonoBehaviour
{

    [SerializeField]
    private GameObject CubeA;

    [SerializeField]
    private GameObject CubeB;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = CubeB.transform.position - CubeA.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var translate = CubeA.transform.position;
            var quateernin = CubeA.transform.rotation;
            var scale = CubeA.transform.localScale;
            var matrixs = Matrix4x4.TRS(translate,quateernin,scale);
            CubeB.transform.position = matrixs.MultiplyPoint(CubeB.transform.position);
            CubeB.transform.rotation = CubeA.transform.rotation;
        }
    }
}
