using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour {


    public void OnMouseDown()
    {
        

        if (gameObject.name == "start_button")
        {
            Static.throwDice = true;
            Static.setCount++;
        }
         if (Static.isActive)
        {
            Debug.Log("card id     " + gameObject.GetComponent<Card>().id);
            Debug.Log("obj name     " + gameObject.name);
        }
        if (gameObject.name == "menu")
        {
            GameObject.FindGameObjectWithTag("MainCamera").transform.position=new Vector3(19.23f, 0, -100);
        }
        if (gameObject.name == "exitMenu")
        {
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, 0, -100);

        }


    }
}
