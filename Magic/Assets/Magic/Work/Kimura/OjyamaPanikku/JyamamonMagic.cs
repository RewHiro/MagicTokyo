using UnityEngine;
using System.Collections;

public class JyamamonMagic : MonoBehaviour {

    FruitCreater fruitcreater;


	void Start () 
    {
        fruitcreater = GetComponentInChildren<FruitCreater>();
        //fruitcreater.EggPlantCreate(5);
	}
	
	void Update () 
    {
        if(Input.GetKeyDown(KeyCode.A))
        {

        JyamamonSend();

        }
	}

    void JyamamonSend()
    {
        fruitcreater.EggPlantCreate(5);

    }
}
