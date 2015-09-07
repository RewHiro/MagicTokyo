using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SlideDirector : MonoBehaviour
{

    [SerializeField]
    GameObject tutorial_root_ = null;

    bool is_create_turorial_root_ = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (is_create_turorial_root_) return;
        if (gameObject.transform.localPosition.x > -2100) return;

        Instantiate(tutorial_root_);

        is_create_turorial_root_ = true;
    }

    public void StartSlide()
    {
        is_create_turorial_root_ = false;
        iTween.MoveTo(gameObject, iTween.Hash("islocal", true, "x", -2200));
        MyNetworkLobbyManager.s_singleton.IsTutorial = true;
    }

    public void FinishSlide()
    {
        iTween.MoveTo(gameObject, iTween.Hash("islocal", true, "x", 0));
        MyNetworkLobbyManager.s_singleton.IsTutorial = false;
    }
}
