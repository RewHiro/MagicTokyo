
using UnityEngine;


public class DebugCommand : MonoBehaviour {

  void Update() {
    if (Input.GetKeyDown(KeyCode.A)) { Debug.Log("se_test1"); AudioManager.Instance.PlaySe(0); }
    if (Input.GetKeyDown(KeyCode.S)) { Debug.Log("se_test2"); AudioManager.Instance.PlaySe(1); }

    if (Input.GetKeyDown(KeyCode.Q)) { Debug.Log("bgm_play"); AudioManager.Instance.PlayBgm(); }
    if (Input.GetKeyDown(KeyCode.W)) { Debug.Log("bgm_stop"); AudioManager.Instance.StopBgm(); }
    if (Input.GetKeyDown(KeyCode.E)) { Debug.Log("bgm_delay_stop"); AudioManager.Instance.StopDelayBgm(); }
  }
}
