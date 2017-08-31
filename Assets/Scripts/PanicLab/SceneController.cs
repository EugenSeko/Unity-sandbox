using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField] private Sprite[] images;

    [SerializeField] private Sprite[] dice_images0;
    [SerializeField] private Sprite[] dice_images1;
    [SerializeField] private Sprite[] dice_images2;
    [SerializeField] private Sprite[] dice_images3;


    [SerializeField] private Card originalCard;
    private int[] sequence = Static.sequence;//инициализация последовательности по умолчанию.


	private void Start () {
        for (int i = 0; i < 26 ; i++)
        {
            Card card; // ссылка на контейнер для карты.
            card = Instantiate(originalCard) as Card; // создание карты согласно базовой.
            card.setCard(i, images[i], Static.getCoordinates(i));//расположение объекта согласно координатам.
            card.gameObject.name = sequence[i].ToString();//изменение имени объекта согласно кодировки.
        }
	}
    private void Update()
    {
        if (Static.throwDice)
        {
            ThrowingDice();//бросаем кости.
            Static.throwDice = false;
            Static.freeze = true;//замораживаем бросание костей.
            Static.isActive = true;//активируем поиск.
        }

    }

    private void  ThrowingDice()
    {
        if (!Static.freeze)
        {
            int diceValue = 1000;
            int labValue = 10;
            int direction = 1;

            int index;
            int[] indexes = ShuffleArray(new int[4] { 0, 1, 2, 3 });

            Card card; // ссылка на контейнер для карты.
            card = Instantiate(originalCard) as Card; // создание карты согласно базовой.
            card.setCard(0, dice_images0[index = Random.Range(0, dice_images0.Length)], Static.diceCoordinates(indexes[0]));
            card.tag = "dices";
            if (index < 3) direction = -1;
            if (index == 0 || index == 4)
            {
                labValue += 3;
            }
            else if (index == 1 || index == 5)
            {
                labValue += 2;
            }
            else
            {
                labValue += 1;
            }

            card = Instantiate(originalCard) as Card; // создание карты согласно базовой.
            card.setCard(0, dice_images1[index = Random.Range(0, dice_images1.Length)], Static.diceCoordinates(indexes[1]));
            card.tag = "dices";
            if (index == 0) diceValue += 10;

            card = Instantiate(originalCard) as Card; // создание карты согласно базовой.
            card.setCard(0, dice_images2[index = Random.Range(0, dice_images2.Length)], Static.diceCoordinates(indexes[2]));
            card.tag = "dices";
            if (index == 1) diceValue += 1;

            card = Instantiate(originalCard) as Card; // создание карты согласно базовой.
            card.setCard(0, dice_images3[index = Random.Range(0, dice_images3.Length)], Static.diceCoordinates(indexes[3]));
            card.tag = "dices";
            if (index == 0) diceValue += 100;

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("dices").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("dices")[i], 12f); //удаление карточек костей через 3сек.
            }
            Static.diceValue = diceValue;
            Static.startPoint = labValue;
            Static.direction = direction;
        }
    }

    private void Searching()
    {

    }

    private int[] ShuffleArray(int[] array)
    {
        int[] newArray = array.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int r = Random.Range(i, newArray.Length);
            int tmp = newArray[i];
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

}
