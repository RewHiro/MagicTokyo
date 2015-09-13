using UnityEngine;
using System.Collections;

public class SotringLayer : MonoBehaviour
{
    [SerializeField]
    string SOTRING_LAYER_NAME = "Default";

    void Start()
    {
        GetComponent<Renderer>().sortingLayerName = SOTRING_LAYER_NAME;
    }
}
