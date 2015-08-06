using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour
{
}


public static class MyRandom
{
    public static bool RandomBool()
    {
        return Random.Range(0, 2) == 0;
    }
};