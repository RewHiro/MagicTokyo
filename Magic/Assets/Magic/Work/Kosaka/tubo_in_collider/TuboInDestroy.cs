using UnityEngine;
using System.Collections;

public class TuboInDestroy : MonoBehaviour
{
    int lemon_count = 0;
    int apumon_count = 0;
    int momon_count = 0;

    public int getLemonCount() { return lemon_count; }
    public int getApumonCount() { return apumon_count; }
    public int getMomonCount() { return momon_count; }
    public int getKudamonCount() {
        var kudamonAdd = lemon_count + apumon_count + momon_count;
        return kudamonAdd;
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.name == "re-mon")
        {
            Destroy(other.gameObject, 3);
            lemon_count++;
            Debug.Log(" Lemon Destroy " + lemon_count);
        }

        if (other.name == "apumon")
        {
            Destroy(other.gameObject, 3);
            apumon_count++;
            Debug.Log(" Apple Destroy " + apumon_count);
        }

        if (other.name == "momon")
        {
            Destroy(other.gameObject, 3);
            momon_count++;
            Debug.Log(" Peach Destroy " + momon_count);
        }

    }
}
