using UnityEngine;
using System.Collections;

public class Ike3Billboard : MonoBehaviour {

    [SerializeField]
    private GameObject _target;

    void Start()
    {
        if (_target != null)
        {
            _target = GameObject.Find(_target.name);

            Vector3 vecDifference = _target.transform.position - transform.position;
            vecDifference.x = 0.0f;
            vecDifference.z = 0.0f;
            transform.LookAt(_target.transform.position - vecDifference);
            transform.Rotate(90.0f, 0.0f, 0.0f);

            transform.Rotate(0.0f, Random.Range(0.0f, 360.0f), 0.0f);
        }
    }

    void Update()
    {
        //if (_target != null)
        //{
        //    Vector3 vecDifference = _target.transform.position - transform.position;
        //    vecDifference.x = 0.0f;
        //    vecDifference.z = 0.0f;
        //    transform.LookAt(_target.transform.position - vecDifference);
        //    transform.Rotate(90.0f, 0.0f, 0.0f);
        //}
    }
}
