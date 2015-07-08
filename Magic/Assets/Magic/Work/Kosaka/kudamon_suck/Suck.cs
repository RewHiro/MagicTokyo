using UnityEngine;
using System.Collections;

public class Suck : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f), Tooltip("吸い取る力 (弱)<--->(強) ")]
    float suck_pow_y = 0.5f;

    [SerializeField, Range(10, 100), Tooltip("質量 (軽)<--->(重) ")]
    float rigid_mass = 100;
    [SerializeField, Range(0, 10), Tooltip("空気抵抗 (小)<--->(大) ")]
    float rigid_drag = 5;

    public void OnTriggerEnter(Collider other)
    {
        var Rigid = other.gameObject.GetComponent<Rigidbody>();

        Rigid.drag = rigid_drag;

        if (other.name == "re-mon" ||
            other.name == "apumon" ||
            other.name == "momon")
            Debug.Log(" Rigid Trriger Collision");
    }

    public void OnTriggerStay(Collider other)
    {
        var Rigid = other.gameObject.GetComponent<Rigidbody>();

        Rigid.mass += rigid_mass;
    }

    public void OnTriggerExit(Collider other)
    {
        var Rigid = other.gameObject.GetComponent<Rigidbody>();

        Rigid.mass = 1;
        Rigid.drag = 0;
    }
}
