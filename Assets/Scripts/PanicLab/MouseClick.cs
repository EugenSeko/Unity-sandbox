using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour {



    public void OnMouseDown()
    {
        Debug.Log("card id     "+ gameObject.GetComponent<Card>().id);
        Debug.Log("sprite name     " + gameObject.GetComponent<SpriteRenderer>().sprite.name);

    }
}
