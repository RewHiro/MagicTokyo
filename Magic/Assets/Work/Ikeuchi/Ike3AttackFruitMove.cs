using UnityEngine;
using System.Collections;

public class Ike3AttackFruitMove : MonoBehaviour
{

    private Vector3 move_value_ = Vector3.zero;

    bool is_attack_ = false;

    int apple_num_ = 0;
    int lemon_num_ = 0;
    int egg_plant_num_ = 0;
    int durian_num_ = 0;

    // 上←→下の動きを切り替える関数
    public void UpDownChange(int apple_num = 0, int lemon_num = 0, int egg_plant_num = 0, int durian_num = 0)
    {
        move_value_ *= -1;
        is_attack_ = true;
        apple_num_ = apple_num;
        lemon_num_ = lemon_num;
        egg_plant_num_ = egg_plant_num;
        durian_num_ = durian_num;
    }

    // Use this for initialization
    void Awake()
    {
        const float LIMIT_VALUE = 0.15f;
        float up_down_value = 0.15f;
        move_value_ = new Vector3(Random.Range(-LIMIT_VALUE, LIMIT_VALUE),
                                  up_down_value,
                                  Random.Range(-LIMIT_VALUE, LIMIT_VALUE));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += move_value_;

        if (move_value_.x > 0) { move_value_.x -= 0.01f; }
        if (move_value_.x < 0) { move_value_.x += 0.01f; }
        if (move_value_.z > 0) { move_value_.z -= 0.01f; }
        if (move_value_.z < 0) { move_value_.z += 0.01f; }

        if (transform.position.y > 10) { Destroy(gameObject); }

        if (!is_attack_) return;
        if (transform.position.y < 0.5f)
        {

            const float y = 0.5f;

            var creater = FindObjectOfType<FruitCreater>();
            if (apple_num_ != 0)
            {
                var fruit = creater.AppleCreate();
                fruit.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
            }

            if (lemon_num_ != 0)
            {
                var fruit = creater.LemonCreate();
                fruit.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
            }

            if (egg_plant_num_ != 0)
            {
                var fruit = creater.EggPlantCreate();
                fruit.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
            }

            if (durian_num_ != 0)
            {
                var fruit = creater.DorianCreate();
                fruit.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
            }

            AudioManager.Instance.PlaySe(4);
            Destroy(gameObject);
        }
    }
}
