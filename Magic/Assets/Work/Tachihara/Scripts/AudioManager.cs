
using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
class AudioVolume {
  [Range(1.0f, 10.0f)]
  public float delay_time_ = 5.0f;

  [Range(0.0f, 1.0f)]
  public float bgm_volume_ = 1.0f;

  [Range(0.0f, 1.0f)]
  public float se_volume_ = 1.0f;
}


public class AudioManager : MonoBehaviour {

  [SerializeField, Tooltip("同時に再生できる SE の最大数")]
  uint max_se_num_ = 4;

  [SerializeField]
  AudioVolume av_ = null;

  bool stop_delay_bgm_ = false;
  float delay_speed_ = 0.0f;

  AudioSource bgm_ = null;
  List<AudioSource> se_ = null;

  List<AudioSource> clips_ = null;

  public static AudioManager Instance { get; private set; }


  void Awake() {
    Debug.Log("AudioManager.Awake()");

    var objects = FindObjectsOfType<AudioManager>();
    if (objects.Length > 1) { Destroy(gameObject); }
    else { DontDestroyOnLoad(gameObject); }

    Instance = gameObject.GetComponent<AudioManager>();
    bgm_ = null;
    se_ = new List<AudioSource>();
    clips_ = new List<AudioSource>();

    Debug.Log("AudioManager.Awake() fin");
  }

  void Start() {
    Debug.Log("AudioManager.Start()");

    var setting = FindObjectOfType<AudioSetting>();
    var list = GameObject.Find("Audio");

    var bgm_list = setting.GetBgmClips();
    var bgm = new GameObject();
    bgm.transform.parent = list.transform;
    bgm_ = bgm.AddComponent<AudioSource>();
    bgm_.clip = bgm_list[Random.Range(0, bgm_list.Count)];
    bgm_.loop = setting.IsBgmLooping();
    bgm_.volume = av_.bgm_volume_;
    bgm_.spatialBlend = 0.0f;

    Debug.Log("AudioManager.Start() fin");
  }

  void Update() {
    Debug.Log(string.Format("volume = {0}", delay_speed_));

    if (se_.Count > 0) {
      foreach (var se in se_) { if (!se.isPlaying) se_.Remove(se); }
    }

    if (!stop_delay_bgm_) { return; }

    delay_speed_ += Time.deltaTime;
    var delay_time = (av_.delay_time_ - delay_speed_) / av_.delay_time_;
    bgm_.volume = delay_time;
  }

  public void PlayBgm() {
    bgm_.volume = av_.bgm_volume_;
    bgm_.Play();

    stop_delay_bgm_ = false;
  }

  public void StopBgm() {
    bgm_.Stop();
  }

  /// <summary>
  /// delay_time 秒かけてフェードアウト
  /// </summary>
  public void StopDelayBgm() {
    stop_delay_bgm_ = true;
    delay_speed_ = 0.0f;
  }

  public void PlaySe(int index) {
    if (se_.Count >= max_se_num_) { return; }
    if (index < 0 || index >= clips_.Count) { return; }

    se_.Add(clips_[index]);

    foreach (var se in se_) { if (!se.isPlaying) se.Play(); }
  }
}
