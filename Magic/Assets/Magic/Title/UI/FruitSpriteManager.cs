using UnityEngine;
using System.Collections;

public class FruitSpriteManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] fruit_sprites_ = null;

    float create_time_ = 0.0f;

    const float CREATE_TIME = 0.3f;

    // Use this for initialization
    void Start()
    {
        create_time_ = Random.Range(0.0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        create_time_ += Time.deltaTime; ;
        if (create_time_ <= CREATE_TIME) return;
        create_time_ = Random.Range(0.0f, 0.2f);
        var gameobject = Instantiate(fruit_sprites_[Random.Range(0, fruit_sprites_.Length)]);
        gameobject.transform.SetParent(gameObject.transform);
        gameobject.transform.localScale = Vector3.one;
    }
}
