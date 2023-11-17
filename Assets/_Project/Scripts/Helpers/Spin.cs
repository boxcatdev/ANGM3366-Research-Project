using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float spinSpeed = 10f;
    [SerializeField] bool spinOnX;
    [SerializeField] bool spinOnY;
    [SerializeField] bool spinOnZ;

    private void Update()
    {
        if (spinOnX) transform.Rotate(Vector3.right * spinSpeed * Time.deltaTime);
        if (spinOnY) transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
        if (spinOnZ) transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
    }

}
