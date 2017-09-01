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
    private int[] _cardSequence = Static.cardSequence;//инициализация последовательности по умолчанию.


	private void Start () {

        for (int i = 0; i < 26 ; i++)
        {
            Card card; // ссылка на контейнер для карты.
            card = Instantiate(originalCard) as Card; // создание карты согласно базовой.
            card.setCard(i, images[i], Static.getCoordinates(i));//расположение объекта согласно координатам.
            card.gameObject.name = _cardSequence[i].ToString();//изменение имени объекта согласно кодировки.
            card.gameObject.tag = i.ToString();// добавление тэга для поиска.
        }
	}
    private void Update()
    {
        if (Static.throwDice)
        {
            ThrowingDice(); //бросаем кости.
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

            if (GameObject.FindGameObjectsWithTag("dices").Length > 0)//удаление костей, если они не успели удалиться автоматически.
            {
               for (int i = 0; i < GameObject.FindGameObjectsWithTag("dices").Length; i++)
                {
                Destroy(GameObject.FindGameObjectsWithTag("dices")[i]);
                }
            }
            

            Card card; // ссылка на контейнер для карты.
            card = Instantiate(originalCard) as Card; // создание карты согласно базовой.
            card.setCard(0, dice_images0[index = Random.Range(0, dice_images0.Length)], Static.diceCoordinates(indexes[0]));
            card.tag = "dices";

            if (index < 3) direction = -1;   // определяем направление

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
                Destroy(GameObject.FindGameObjectsWithTag("dices")[i], 1200f); //удаление карточек костей через 3сек.
            }
            Static.diceValue = diceValue;
            Static.startPoint = labValue;
            Static.direction = direction;

            Static.throwDice = false; // отключаем автоматический запуск метода.
            Static.freeze = true;//замораживаем бросание костей.
            Static.isActive = true;//активируем поиск руками.
            Searching();    //запускаем поиск.
        }
    }

    private void Searching()
    {
        int[] sequence = _cardSequence.Clone() as int[];
        int value = Static.diceValue;
        int startIndex = -1;
        bool ventel = false;
        int iterations = 0; //количество итераций поиска.



        for (int i = 0; i < sequence.Length; i++) // находим стартовый индекс с которого начнем движение.
        {
            if (sequence[i] == Static.startPoint) startIndex = i;
            iterations++;
        }

        if (Static.direction == 1)   // создание бесконечного цикла по часовой стрелке.
        {
            int i;
            for ( i = startIndex; i <= sequence.Length; i++)
            {
                iterations++;

                if (i == sequence.Length) i = 0;// создание бесконечного цикла по часовой стрелке.

                if (sequence[i] == value && !ventel) // проверка на соответствие значения.
                {
                    Static.iterations = iterations;
                    StartCoroutine("Pulse", GameObject.FindGameObjectWithTag(i.ToString()));
                    return;
                }

                if (sequence[i] == 0 && !ventel)  // проверка на вход в вентиляцию.
                {
                    ventel = true;
                    Debug.Log("вход");
                }
                else if (sequence[i] == 0 && ventel)// проверка на выход из вентиляции.
                {
                    ventel = false;
                    Debug.Log("выход");
                }

                if (sequence[i] < 4 && sequence[i] > 0 && !ventel) // проверка на мутацию.
                {
                    if (sequence[i] == 1) // меняем форму.
                    {
                        if (value >= 1100) { value -= 100; }
                        else { value += 100; }
                    }
                    else if (sequence[i] == 2) // меняем цвет.
                    {
                        if (value - 1010 == 100 || value - 1010 == 101 || value - 1010 == 0 || value - 1010 == 1)
                        {
                            value -= 10;
                        }
                        else
                        {
                            value += 10;
                        }
                    }
                    else  // меняем маркировку.
                    {
                        if (sequence[i] % 2 != 0) { value -= 1; }
                        else { value += 1; }
                    }
                }
            }
        }
        else                         // создание бесконечного цикла против часовой стрелки.
        {
            int i;
            for (i = startIndex; i > -2; i--)
            {
                iterations++;

                if (i == -1) i = sequence.Length-1;  // создание бесконечного цикла против часовой стрелки.

                if (sequence[i] == value && !ventel)  // проверка на соответствие значения.
                {
                    Static.iterations = iterations;
                    StartCoroutine("Pulse", GameObject.FindGameObjectWithTag(i.ToString()));
                    return;
                }

                if (sequence[i] == 0 && !ventel)  // проверка на вход в вентиляцию.
                {
                    ventel = true;
                    Debug.Log("вход");
                }
                else if (sequence[i] == 0 && ventel) // проверка на выход из вентиляции.
                {
                    ventel = false;
                    Debug.Log("выход");
                }

                if (sequence[i] < 4 && sequence[i] > 0 && !ventel) // проверка на мутацию.
                {
                    if (sequence[i] == 1) // меняем форму.
                    {
                        if (value >= 1100) { value -= 100; }
                        else { value += 100; }
                    }
                    else if (sequence[i] == 2) // меняем цвет.
                    {
                        if(value-1010==100 || value - 1010 == 101 || value - 1010 == 0 || value - 1010 == 1)
                        {
                            value -= 10;
                        }
                        else
                        {
                            value += 10;
                        }
                    }
                    else  // меняем маркировку.
                    {
                        if (sequence[i] % 2 != 0) { value -= 1; }
                        else { value += 1; }
                    }
                }
            }
        }
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

    private IEnumerator Pulse(GameObject obj)
    {
        yield return new WaitForSeconds(Static.wait*Static.iterations);

            for (int i = 0; i < 8; i++)
            {
                obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                yield return new WaitForSeconds(0.1f);
                obj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                yield return new WaitForSeconds(0.1f);
            }
        Static.freeze = false;//размораживаем бросание костей.
        Static.isActive = false;//деактивируем поиск руками.

    }

}
