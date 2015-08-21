
using UnityEngine;


public class LeapHandSortingLayer : MonoBehaviour
{

    void Awake() { }

    void Start()
    {
        //var hand = GetComponent<SkinnedMeshRenderer>();
        //hand.sortingLayerName = "LeapHand";
        //hand.sortingOrder = 1;
    }

    void Update()
    {
        foreach (var finger in FindObjectsOfType<SkeletalFinger>())
        {
            foreach (var renderer in finger.GetComponentsInChildren<MeshRenderer>())
            {
                renderer.sortingLayerName = "Defult";
                renderer.sortingOrder = 10;
            }
        }
    }
}
