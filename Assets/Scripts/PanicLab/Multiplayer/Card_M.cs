using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_M : MonoBehaviour {
    [SerializeField] private SceneController_M sceneController; 
    private int _id;
    public int id
    {
        get { return _id; }
    }

    public void setCard(int id, Sprite image, float[]coordinates)
    {
        GetComponent<SpriteRenderer>().sprite = image;
        transform.position = new Vector3(coordinates[0], coordinates[1], coordinates[2]);
        _id = id;
    }
    
}
