using UnityEngine;
using System.Collections;

public class SpecialEvent : MonoBehaviour {

    [SerializeField
    , TooltipAttribute("モモン以外のモデルを何に変えるか")]
    private GameObject fruit_obj_ = null;

    [SerializeField
    , TooltipAttribute("そのフルーツのマネージャーの名前を入れてください")]
    private string manager_name_;

    [SerializeField
    , TooltipAttribute("紋章パーティクルシステムを入れてください")]
    private GameObject coat_of_arms_particle_;
    private Vector3 CoatOfArmsPos { get { return new Vector3(0.0f, 3.0f, 0.0f); } }
    private Vector3 CoatOfArmsRot { get { return new Vector3(0.0f, 0.0f, 0.0f); } }
    private Vector3 CoatOfArmsScale { get { return new Vector3(1.0f, 1.0f, 1.0f); } }

    [SerializeField
    , TooltipAttribute("くだもんたちに起こすパーティクルシステムを入れてください")]
    private GameObject kudamon_particle_;
    private Vector3 KudamonParticleRot { get { return new Vector3(-90.0f, 0.0f, 0.0f); } }

    [SerializeField
    , TooltipAttribute("生成されたときに出すパーティクルシステムを入れてください")]
    private GameObject when_making_particle_;

    GameObject fruit_manager_ = null;

    // Use this for initialization
    void Start () {
        fruit_manager_ = GameObject.Find("Ike3DorianSetting")
                        .GetComponent<Ike3DorianSetting>()
                        .FruitManager;

        GameObject when_making_particle = Instantiate(when_making_particle_);
        when_making_particle.transform.position = transform.position;
        when_making_particle.transform.eulerAngles = transform.eulerAngles;
        when_making_particle.transform.localScale = transform.localScale;
        when_making_particle.name = when_making_particle_.name;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void ChangeFruits()
    {
        var fruits_obj = fruit_manager_.GetComponentsInChildren<Ike3SpecialEventSearch>();
        
        foreach(var fruit in fruits_obj)
        {
            if (fruit_obj_ == null) return;
            GameObject fruit_manager = GameObject.Find(manager_name_);
            GameObject game_object = Instantiate(fruit_obj_);
            game_object.transform.SetParent(fruit_manager.transform);
            game_object.transform.position = fruit.transform.position;
            game_object.transform.rotation = fruit.transform.rotation;
            game_object.transform.localScale = fruit.transform.localScale;
            game_object.name = fruit_obj_.name;

            // はじけてるエフェクト
            GameObject kudamon_particle = Instantiate(kudamon_particle_);
            kudamon_particle.transform.position = fruit.transform.position;
            kudamon_particle.transform.eulerAngles = KudamonParticleRot;
            kudamon_particle.transform.localScale = fruit.transform.localScale;
            kudamon_particle.name = kudamon_particle_.name;

            Destroy(fruit.gameObject);
        }

        // 紋章エフェクト
        GameObject coat_of_arms_particle = Instantiate(coat_of_arms_particle_);
        coat_of_arms_particle.transform.position = CoatOfArmsPos;
        coat_of_arms_particle.transform.eulerAngles = CoatOfArmsRot;
        coat_of_arms_particle.transform.localScale = CoatOfArmsScale;
        coat_of_arms_particle.name = coat_of_arms_particle_.name;
    }
}
