
using UnityEngine;


public class DebugCommand : MonoBehaviour {

  void Update() {
    if (Input.GetKeyDown(KeyCode.A)) { AudioManager.Instance.PlaySe(0); }
    if (Input.GetKeyDown(KeyCode.S)) { AudioManager.Instance.PlaySe(1); }

    if (Input.GetKeyDown(KeyCode.Q)) { AudioManager.Instance.PlayBgm(); }
    if (Input.GetKeyDown(KeyCode.W)) { AudioManager.Instance.StopBgm(); }
    if (Input.GetKeyDown(KeyCode.E)) { Debug.Log("delay"); AudioManager.Instance.StopDelayBgm(); }
  }
}
