
using UnityEngine;
using UnityEngine.Networking;


public class PlayerMagicManager : NetworkBehaviour {

  // 使用可能な魔法タイプ（-1: 使用不可）
  // FIXME: 列挙型のほうが信頼性高そう
  public int MagicType { get; private set; }

  ItemSpriteManager sprite_ = null;
  TuboInDestroy tubo_ = null;

  float cool_time_ = 0;
  float[] MAGIC_COOL_TIME = null;


  void Start() {
    if (!isLocalPlayer) return;
    MagicType = -1;
    tubo_ = FindObjectOfType<TuboInDestroy>();

    MAGIC_COOL_TIME = new float[5];

    // FIXME: それぞれ発動中の長さが取得できないものは、仮の値を使用
    MAGIC_COOL_TIME[0] = FindObjectOfType<Ike3KinesisSetting>().FloatSecond;     // キネシス
    MAGIC_COOL_TIME[1] = FindObjectOfType<EggPlantAttacker>().GetMagicTime;      // おじゃま
    MAGIC_COOL_TIME[2] = FindObjectOfType<SmallFruit>().TotalSmallFruitSecond;   // ちいさく
    MAGIC_COOL_TIME[3] = FindObjectOfType<Ike3TyphoonSetting>().LimitTime_;      // ミキサー
    MAGIC_COOL_TIME[4] = 1.0f;    // ピーチェンジ（仮
  }

  void Update() {
    if (!isLocalPlayer) return;
    if (sprite_ == null) sprite_ = FindObjectOfType<ItemSpriteManager>();

    if (IsCoolDown()) { cool_time_ -= Time.deltaTime; return; }
    if (!OnGetMomon() || EnableMagic()) { return; }

    MagicType = Random.Range(0, sprite_.IconSize);
    sprite_.SlotTriggerEnter();
  }

  bool OnGetMomon() {
    return tubo_.GetMomonCount();
  }

  bool EnableMagic() {
    return MagicType != -1;
  }

  public void MagicExecute() {
    // クールダウン中、またはスロット回転、点滅中は発動できない
    var slot_blinking = sprite_.IsSlotBlink();
    var reel_playing = sprite_.SlotTrigger;
    if (IsCoolDown() || slot_blinking || reel_playing) return;

    cool_time_ = MAGIC_COOL_TIME[MagicType];
    MagicType = -1;
    sprite_.MagicAction();
    tubo_.ResetMomon();
  }

  bool IsCoolDown() {
    return cool_time_ > 0.0f;
  }
}
