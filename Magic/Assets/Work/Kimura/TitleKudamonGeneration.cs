
using UnityEngine;
using System.Collections;

public class TitleKudamonGeneration : MonoBehaviour
{

    [SerializeField]
    GameObject[] fruit_sprites_ = null;
    GameObject[] game_object_ = new GameObject[2];

    [SerializeField]
    float force_power_ = 10.0f;

    [SerializeField]
    float CREATE_TIME = 1.0f;
    float create_time_ = 0.0f;

    // Use this for initialization
    void Start()
    {
        create_time_ = Random.Range(0.0f, 0.2f);
        transform.rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        create_time_ += Time.deltaTime; ;
        if (create_time_ <= CREATE_TIME) return;
        create_time_ = Random.Range(0.0f, 0.2f);

        for (int i = 0; i < 2; ++i)
        {
            game_object_[i] = Instantiate(fruit_sprites_[i]);
            game_object_[i].transform.SetParent(gameObject.transform);
            game_object_[i].transform.localScale = Vector3.one;

            Vector3 power = new Vector3(100000 * Random.Range(0.9f, 1.1f), 100000 * Random.Range(0.9f, 1.1f), 0);

            if (game_object_[0] == game_object_[i])
            {
                game_object_[i].transform.localPosition = new Vector3(-500, -180, 0);
                game_object_[i].GetComponent<Rigidbody>().AddForce(new Vector3(power.x, power.y, power.z));
            }
            else if (game_object_[1] == game_object_[i])
            {
                game_object_[i].transform.localPosition = new Vector3(500, -180, 0);
                game_object_[i].GetComponent<Rigidbody>().AddForce(new Vector3(-power.x, power.y, power.z));
            }
        }

        for (int i = 0; i < 2; ++i)
        {
            game_object_[i] = Instantiate(fruit_sprites_[i]);
            game_object_[i].transform.SetParent(gameObject.transform);
            game_object_[i].transform.localScale = Vector3.one;

            Vector3 power = new Vector3(100000 * Random.Range(0.9f, 1.1f), 100000 * Random.Range(0.9f, 1.1f), 0);

            if (game_object_[0] == game_object_[i])
            {
                game_object_[i].transform.localPosition = new Vector3(-500, -180, 0);
                game_object_[i].GetComponent<Rigidbody>().AddForce(new Vector3(power.x, power.y, power.z));
            }
            else if (game_object_[1] == game_object_[i])
            {
                game_object_[i].transform.localPosition = new Vector3(500, -180, 0);
                game_object_[i].GetComponent<Rigidbody>().AddForce(new Vector3(-power.x, power.y, power.z));
            }
        }

        for (int i = 0; i < 2; ++i)
        {
            game_object_[i] = Instantiate(fruit_sprites_[i]);
            game_object_[i].transform.SetParent(gameObject.transform);
            game_object_[i].transform.localScale = Vector3.one;

            Vector3 power = new Vector3(100000 * Random.Range(0.9f, 1.1f), 100000 * Random.Range(0.9f, 1.1f), 0);

            if (game_object_[0] == game_object_[i])
            {
                game_object_[i].transform.localPosition = new Vector3(-500, -180, -150);
                game_object_[i].GetComponent<Rigidbody>().AddForce(new Vector3(power.x, power.y, power.z));
            }
            else if (game_object_[1] == game_object_[i])
            {
                game_object_[i].transform.localPosition = new Vector3(500, -180, -150);
                game_object_[i].GetComponent<Rigidbody>().AddForce(new Vector3(-power.x, power.y, power.z));
            }
        }

        //foreach (var obj in game_object_)
        //{
        //    var force = new Vector3(10000,1000,0) - obj.transform.localPosition;
        //    force.Normalize();

        //    var rigid = obj.GetComponent<Rigidbody>();
        //    rigid.AddForceAtPosition(force * force_power_, Vector3.zero, ForceMode.Impulse);
        //}
    }
}