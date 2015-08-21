
using UnityEngine;
using System.Collections.Generic;


public class AudioSetting : MonoBehaviour {

  [SerializeField]
  bool bgm_loop_ = false;
  public bool IsBgmLooping() { return bgm_loop_; }

  [SerializeField]
  List<AudioClip> bgm_clips_ = null;
  public List<AudioClip> GetBgmClips() { return bgm_clips_; }

  [SerializeField]
  List<AudioClip> se_clips_ = null;
  public List<AudioClip> GetSeClips() { return se_clips_; }
}
