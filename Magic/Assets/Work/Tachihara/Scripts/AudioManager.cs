
using UnityEngine;
using System.Collections.Generic;


class Audio {
  public static AudioSource bgm = new AudioSource();
  public static List<AudioSource> se = null;

  public static Dictionary<uint, AudioClip> clips = null;

  public static void Init() {
    bgm.clip = null;
    se.Clear();
    clips.Clear();
  }
}


public class AudioManager : MonoBehaviour {

  [SerializeField, Tooltip("同時に再生できる SE の最大数")]
  uint max_se_num_ = 4;

  static uint unique_id_ = 0;
  uint GetUniqueID() {
    if (unique_id_ == ~(uint)0) { unique_id_ = 0; }
    return unique_id_++;
  }


  void Awake() {
    var objects = FindObjectsOfType<AudioManager>();
    if (objects.Length > 1) { Destroy(gameObject); }
    else { DontDestroyOnLoad(gameObject); }
  }

  void Update() {
    foreach (var se in Audio.se) {
      if (!se.isPlaying) Audio.se.Remove(se);
    }
  }

  public void Init() {
    var audio = FindObjectOfType<AudioSetting>();

    Audio.bgm.loop = audio.IsBgmLooping();
    Audio.bgm.spatialBlend = 0.0f;   // 距離で音量が変わらないようにする
  }

  public void PlayBgm(uint index) {
  }

  public void StopBgm() {
  }

  public void PlaySe(uint index) {
    if (Audio.se.Count >= max_se_num_) { return; }
    if (!Audio.clips.ContainsKey(index)) { return; }

    var source = new AudioSource();
    source.clip = Audio.clips[index];
    Audio.se.Add(source);

    foreach (var se in Audio.se) {
      if (!se.isPlaying) se.Play();
    }
  }

  /*
  static AudioManager manager_ = null;
  public static AudioManager instance {
    get {
      if (manager_ == null) { manager_ = new AudioManager(); }
      return manager_;
    }
  }

  public static uint GetUniqueID() {
    uint unique_id = 0;
    if (unique_id == ~(uint)0) { unique_id = 0; }
    return unique_id;
  }

  [SerializeField, Tooltip("同時に再生できる SE の最大数")]
  int max_se_ = 4;

  AudioSource bgm_ = null;
  List<AudioSource> se_ = null;

  Dictionary<uint, AudioClip> clips_ = null;

  void Awake() {
    var objects = FindObjectsOfType<AudioManager>();
    if (objects.Length > 1) { Destroy(gameObject); }
    else { DontDestroyOnLoad(gameObject); }
  }

  void Update() {
    foreach (var se in se_) { if (!se.isPlaying) se_.Remove(se); }
  }

  public void PlayBgm(uint index) {
    if (!clips_.ContainsKey(index)) { return; }

    var next_bgm = clips_[index];
    if (bgm_.clip == next_bgm) { return; }

    bgm_.Stop();
    bgm_.clip = next_bgm;
    bgm_.Play();
  }

  public void StopBgm() {
    bgm_.Stop();
    bgm_ = null;
  }

  public void PlaySe(uint index) {
  }
  */
}
