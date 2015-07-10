using UnityEngine;
using System.Collections;

public class SuckAdd : MonoBehaviour {

    [SerializeField, Range(0, 10), Tooltip("空気抵抗 (小)<--->(大) ")]
    float rigid_drag = 1;

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
        var tubo_pos = GameObject.Find("tubo_kai").GetComponent<Transform>().position;

        if (other.name == "re-mon" ||
            other.name == "apumon" ||
            other.name == "momon")
        {
            var kudamon_pos = other.gameObject.GetComponent<Transform>().position;

            kudamon_pos =
                Vector3.MoveTowards(transform.position, tubo_pos, 5.0f);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var Rigid = other.gameObject.GetComponent<Rigidbody>();

        Rigid.drag = 0;
    }
}
