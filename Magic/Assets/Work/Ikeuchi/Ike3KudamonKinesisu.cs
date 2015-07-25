using UnityEngine;
using System.Collections;

public class Ike3KudamonKinesisu : MonoBehaviour
{
    //[SerializeField
    //,TooltipAttribute("prefab「Ike3KinesisSetting」を「Hierarchy」から入れてください\n「Project」から持ってきてもいいですが数値の設定がめんどくさいです")]
    //private GameObject setting_;

    //[SerializeField, Range(0, 1000), TooltipAttribute("浮かせる個数")]
    private int FLOAT_NUM_ = 20;

    void Start()
    {
        // Ike3KinesisSetting は自分が作った「prefab」だから名前勝手に変えないだろうという慢心Find
        var setting = GameObject.Find("Ike3KinesisSetting").GetComponent<Ike3KinesisSetting>();
        FLOAT_NUM_ = setting.FloatNum;
    }

    public void KudamonKinesis()
    {
        // どのくだもんを浮かせるか決める処理
        var kudamon_obj = GetComponentsInChildren<Ike3KudamonMove>();

        // 存在するくだもんが「FLOAT_NUM_」個未満の時は乱数値を出す必要がない
        // (今あるくだもんをすべて浮かせればよい)
        int array_length = (kudamon_obj.Length < FLOAT_NUM_) ? kudamon_obj.Length : FLOAT_NUM_;
        var float_kudamon_num = new int[array_length];
        int float_kudamon_set_count = 0;

        while (float_kudamon_set_count != array_length)
        {
            if (array_length == FLOAT_NUM_)
            {
                int rand = Random.Range(0, kudamon_obj.Length);
                bool through = false;
                // すでに出た乱数値が出た場合やり直す
                for (int i = 0; i < float_kudamon_set_count; i++)
                {
                    if (rand == float_kudamon_num[i])
                    {
                        through = true;
                        break;
                    }
                }
                if (through) { continue; }
                float_kudamon_num[float_kudamon_set_count] = rand;
            }
            else
            {
                float_kudamon_num[float_kudamon_set_count] = float_kudamon_set_count;
            }
            float_kudamon_set_count++;
        }

        for (var i = 0; i < array_length; i++)
        {
            kudamon_obj[float_kudamon_num[i]].KinesisuOn();
            Vector3 float_pos = FloatPosition(i);
            kudamon_obj[float_kudamon_num[i]].FloatPos = float_pos;
        }
    }

    Vector3 FloatPosition(int i)
    {
        const float KUDAMON_HEIGHT_OFFSET = 2.0f;
        const float KUDAMON_HEIGHT_INTERVAL = 0.4f;
        const float KUDAMON_ROW_INTERVAL = 0.7f;
        const float KUDAMON_COLUMN_INTERVAL = 1.5f;   
        const int COLUMN_MAX = 5;
        int row_num = (i / COLUMN_MAX);
        int column_num = (i % COLUMN_MAX);
        float coordinate = (COLUMN_MAX / 2 * KUDAMON_COLUMN_INTERVAL)
                           + ((row_num % 2) * (KUDAMON_COLUMN_INTERVAL / 2));
        float x = (column_num * KUDAMON_COLUMN_INTERVAL) - coordinate;
        float y = KUDAMON_HEIGHT_OFFSET + KUDAMON_HEIGHT_INTERVAL * row_num;
        float z = row_num * KUDAMON_ROW_INTERVAL;
        return new Vector3(x, y, z);
    }
}
