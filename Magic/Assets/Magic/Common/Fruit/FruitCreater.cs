using UnityEngine;
using System.Collections;

public class FruitCreater : MonoBehaviour {

    [SerializeField,TooltipAttribute("レーモンのprefabを入れてください")]
    GameObject lemon = null;

    [SerializeField, TooltipAttribute("アプモンのprefabを入れてください")]
    GameObject apple = null;

    [SerializeField, TooltipAttribute("モモンのprefabを入れてください")]
    GameObject peach = null;

    [SerializeField, TooltipAttribute("じゃまモンのprefabを入れてください")]
    GameObject egg_plant = null;

    [SerializeField, TooltipAttribute("ドリアンボムのprefabを入れてください")]
    GameObject dorian = null;

    [SerializeField,Range(0,200), TooltipAttribute("レーモンの出す数")]
    int LEMON_NUM = 100;

    [SerializeField,Range(0,200), TooltipAttribute("アプモンの出す数")]
    int APPLE_NUM = 100;

    [SerializeField, Range(0,200),TooltipAttribute("モモンの出す数")]
    int PEACH_NUM = 100;

    [SerializeField, Range(0, 200), TooltipAttribute("ジャマモンの出す数")]
    int JAMAMON_NUM = 100;

    HandController hand_controller_ = null;

    void Awake()
    {
        hand_controller_ = GameObject.Find("LeapHandController").GetComponent<HandController>();

        LemonCreate(LEMON_NUM);
        AppleCreate(APPLE_NUM);
        PeachCreate(PEACH_NUM);
        EggPlantCreate(JAMAMON_NUM);
    }

    public void LemonCreate(int lemon_num)
    {
        GameObject lemon_manager = GameObject.Find("LemonManager");
        for (int i = 0; i < lemon_num; ++i)
        {
            if (lemon == null) continue;
            GameObject game_object = Instantiate(lemon);
            game_object.transform.SetParent(lemon_manager.transform);
            game_object.name = lemon.name;
        }
    }

    public void AppleCreate(int apple_num)
    {
        GameObject apple_manager = GameObject.Find("AppleManager");
        for (int i = 0; i < apple_num; ++i)
        {
            if (apple == null) continue;
            GameObject game_object = Instantiate(apple);
            game_object.transform.SetParent(apple_manager.transform);
            game_object.name = apple.name;
        }
    }

    public void PeachCreate(int peach_num)
    {
        GameObject peach_manager = GameObject.Find("PeachManager");
        for (int i = 0; i < peach_num; ++i)
        {
            if (peach == null) continue;
            GameObject game_object = Instantiate(peach);
            game_object.transform.SetParent(peach_manager.transform);
            game_object.name = peach.name;
        }
    }

    public void EggPlantCreate(int egg_plant_num)
    {
        GameObject egg_plant_manager = GameObject.Find("EggPlantManager");
        for (int i = 0; i < egg_plant_num; ++i)
        {
            if (egg_plant == null) continue;
            GameObject game_object = Instantiate(egg_plant);
            game_object.transform.SetParent(egg_plant_manager.transform);
            game_object.name = egg_plant.name;
        }
    }

    public void DorianCreate(int dorian_num)
    {
        GameObject dorian_manager = GameObject.Find("DorianManager");
        for (int i = 0; i < dorian_num; ++i)
        {
            if (dorian == null) continue;
            GameObject game_object = Instantiate(dorian);
            game_object.transform.SetParent(dorian_manager.transform);
            game_object.name = dorian.name;
        }
        AudioManager.Instance.PlaySe(7);
    }

    public GameObject LemonCreate()
    {
        GameObject lemon_manager = GameObject.Find("LemonManager");
        if (lemon == null) return null;
        GameObject game_object = Instantiate(lemon);
        game_object.transform.SetParent(lemon_manager.transform);
        game_object.name = lemon.name;
        return game_object;
    }

    public GameObject AppleCreate()
    {
        GameObject apple_manager = GameObject.Find("AppleManager");
        if (apple == null) return null;
        GameObject game_object = Instantiate(apple);
        game_object.transform.SetParent(apple_manager.transform);
        game_object.name = apple.name;
        return game_object;
    }

    public GameObject PeachCreate()
    {
        GameObject peach_manager = GameObject.Find("PeachManager");
        if (peach == null) return null;
        GameObject game_object = Instantiate(peach);
        game_object.transform.SetParent(peach_manager.transform);
        game_object.name = peach.name;
        return game_object;
    }

    public GameObject EggPlantCreate()
    {
        GameObject egg_plant_manager = GameObject.Find("EggPlantManager");
        if (egg_plant == null) return null;
        GameObject game_object = Instantiate(egg_plant);
        game_object.transform.SetParent(egg_plant_manager.transform);
        game_object.name = egg_plant.name;
        return game_object;
    }

    public GameObject DorianCreate()
    {
        GameObject dorian_manager = GameObject.Find("DorianManager");
        if (dorian == null) return null;
        GameObject game_object = Instantiate(dorian);
        game_object.transform.SetParent(dorian_manager.transform);
        game_object.name = dorian.name;
        return game_object;
    }

}