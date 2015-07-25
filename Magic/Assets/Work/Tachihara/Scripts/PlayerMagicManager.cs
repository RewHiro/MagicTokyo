
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMagicManager : NetworkBehaviour {

  // 使用可能な魔法タイプ（-1: 使用不可）
  // FIXME: 列挙型のほうが信頼性高そう
  public int MagicType { get; private set; }

  SpriteManager sprite_ = null;
  TuboInDestroy tubo_ = null;


  void Start() {
    if (!isLocalPlayer) return;
    MagicType = -1;
    sprite_ = FindObjectOfType<SpriteManager>();
    tubo_ = FindObjectOfType<TuboInDestroy>();
  }

  void Update() {
    if (!isLocalPlayer) return;
    if (!OnGetMomon() || EnableMagic()) { return; }

    MagicType = Random.Range(0, sprite_.IconSize - 1);
    sprite_.SlotTriggerEnter();

    //TODO: モモン（と他のくだモン）のカウントを消す処理が必要になると思われる
  }

  bool OnGetMomon() {
    return tubo_.GetMomonCount();
  }

  bool EnableMagic() {
    return MagicType != -1;
  }

  public void MagicExecute() {
    sprite_.MagicAction();
    MagicType = -1;
    tubo_.ResetMomon();
  }
}
