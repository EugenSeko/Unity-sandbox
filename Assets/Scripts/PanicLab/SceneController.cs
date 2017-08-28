using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField] private Sprite[] images;
    [SerializeField] private Card originalCard;


	void Start () {
        for (int i = 2; i >= 0; i--)
        {
            Card card;
            card = Instantiate(originalCard) as Card;
            card.setCard(i, images[i], Static.getCoordinates(i));
        }
	}

}
