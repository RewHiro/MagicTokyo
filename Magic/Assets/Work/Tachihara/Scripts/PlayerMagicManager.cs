
using UnityEngine;
using UnityEngine.Networking;


public class PlayerMagicManager : NetworkBehaviour {

  // 使用可能な魔法タイプ（-1: 使用不可）
  // FIXME: 列挙型のほうが信頼性高そう
  public int MagicType { get; private set; }

  ItemSpriteManager sprite_ = null;
  TuboInDestroy tubo_ = null;

  readonly int FPS = 60;
  int cool_time_ = 0;

  [SerializeField, Tooltip("魔法発動後、次の魔法が使用可能になるまでの時間（単位：秒）")]
  int[] MAGIC_COOL_TIME = null;


  void Start() {
    if (!isLocalPlayer) return;
    MagicType = -1;
    sprite_ = FindObjectOfType<ItemSpriteManager>();
    tubo_ = FindObjectOfType<TuboInDestroy>();
  }

  void Update() {
    if (IsCoolDown()) { --cool_time_; return; }

    if (!isLocalPlayer) return;
    if (!OnGetMomon() || EnableMagic()) { return; }

    MagicType = Random.Range(0, sprite_.IconSize - 1);
    sprite_.SlotTriggerEnter();
  }

  bool OnGetMomon() {
    return tubo_.GetMomonCount();
  }

  bool EnableMagic() {
    return MagicType != -1;
  }

  public void MagicExecute() {
    // クールダウン中、またはスロット点滅中は発動できない
    if (IsCoolDown() || sprite_.IsSlotBlink()) return;

    sprite_.MagicAction();
    cool_time_ = FPS * MAGIC_COOL_TIME[MagicType];
    MagicType = -1;
    tubo_.ResetMomon();
  }

  bool IsCoolDown() {
    return cool_time_ > 0;
  }
}
