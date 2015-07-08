using UnityEngine;
using System.Collections;

public class JyamamonCreater : MonoBehaviour {

    [SerializeField, TooltipAttribute("じゃまモンのprefabを入れてください")]
    GameObject jyamamon = null;

    [SerializeField, Range(0, 100), TooltipAttribute("じゃまモンの出す数")]
    int JYAMAMON_NUM = 5;

    HandController hand_controller_ = null;

    void Awake()
    {
        //魔法の発動の処理が入ったらいらない
        hand_controller_ = GameObject.Find("LeapHandController").GetComponent<HandController>();

    }

    void Start()
    {
        JyamamonCreate(JYAMAMON_NUM);
    }

    // Update is called once per frame
    void Update()
    {
        //魔法の発動の処理が入ったらif文の条件が変わる
        //if (hand_controller_.IsAttack)
        //{
        //    JyamamonCreate(JYAMAMON_NUM);
        //}
    }

    void JyamamonCreate(int Jyamamon_num)
    {
        GameObject Jyamamon_manager = GameObject.Find("JyamamonManager");
        for (int i = 0; i < Jyamamon_num; ++i)
        {
            if (jyamamon == null) continue;
            GameObject game_object = Instantiate(jyamamon);
            game_object.transform.SetParent(Jyamamon_manager.transform);
            game_object.name = jyamamon.name;
        }
    }
}
