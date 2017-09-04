using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField] private Sprite[] images;

    [SerializeField] private Sprite[] dice_images0;
    [SerializeField] private Sprite[] dice_images1;
    [SerializeField] private Sprite[] dice_images2;
    [SerializeField] private Sprite[] dice_images3;

    [SerializeField] private TextMesh gamesCountLabel;
    [SerializeField] private TextMesh message;
    [SerializeField] private TextMesh myScore;
    [SerializeField] private TextMesh score;

    GameObject obj;

    [SerializeField] private Card originalCard;
    private int[] _cardValuesSequence;
    private int[] _sequence;


	private void Start () {

        _sequence = Static.sequences(Static.numOfSequence);
        _cardValuesSequence = new int[26];
        for (int i = 0; i < 26; i++)
        {
            _cardValuesSequence[i] = Static.cardValuesSequence[_sequence[i]];
        }

        for (int i = 0; i < 26 ; i++)
        {
            int index = _sequence[i];

            Card card; // ссылка на контейнер для карты.
            card = Instantiate(originalCard) as Card; // создание карты согласно базовой.
            card.setCard(index, images[index], Static.getCoordinates(i));//расположение объекта согласно координатам.
            card.gameObject.name = _cardValuesSequence[i].ToString();//изменение имени объекта согласно кодировки.
            card.gameObject.tag = index.ToString();// добавление тэга для поиска.
        }
	}
    private void Update()
    {
        if (Static.throwDice)
        {
            ThrowingDice(); //бросаем кости.
        }

        if (Static.goodAnswer==1)
        {
            StartCoroutine("Rotation");
            Static.goodAnswer = 0;
            return;
        }
        else if(Static.goodAnswer == -1)
        {
            StartCoroutine("QuickPulse");
            Static.goodAnswer = 0;
            return;
        }

        score.text = Static.score.ToString();
        myScore.text = Static.myScore.ToString();
        gamesCountLabel.text = "Round " + Static.gamesCount;//выводим на экран число игр.
    }

    private void  ThrowingDice()
    {
        if (!Static.freezeThrowingDice)
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

            switch (Static.numOfSequence)  // определяем направление в соответствии с раскладкой карт.
            {
                case 0: if (index < 3) direction = -1;
                    break;
                case 1: if (index == 2 || index > 3) direction = -1;
                    break;
                case 2: break;
                case 3: break;
            }

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
            Static.freezeThrowingDice = true;//замораживаем бросание костей.

            Static.gamesCount++;

            Searching();    //запускаем поиск.
        }
    }

    private void Searching()
    {
        int[] cardValueSequence = _cardValuesSequence.Clone() as int[];
        int value = Static.diceValue;
        int startIndex = -1;
        bool ventel = false;
        int iterations = 0; //количество итераций поиска.



        for (int i = 0; i < cardValueSequence.Length; i++) // находим стартовый индекс с которого начнем движение.
        {
            if (cardValueSequence[i] == Static.startPoint) { startIndex = i; break; }
            iterations++;
        }

        if (Static.direction == 1)   // создание бесконечного цикла по часовой стрелке.
        {
            Debug.Log("+1");
            int i;
            for ( i = startIndex; i <= cardValueSequence.Length; i++)
            {
                iterations++;

                if (i == cardValueSequence.Length) i = 0;// создание бесконечного цикла по часовой стрелке.

                if (cardValueSequence[i] == value && !ventel) // проверка на соответствие значения.
                {
                    Static.iterations = iterations;
                   // obj = GameObject.FindGameObjectWithTag(i.ToString());
                    obj = GameObject.FindGameObjectWithTag(_sequence[i].ToString());
                    StartCoroutine("Pulse");
                    //Static.id = i;
                    Static.id = _sequence[i];
                    return;
                }

                if (cardValueSequence[i] == 0 && !ventel)  // проверка на вход в вентиляцию.
                {
                    ventel = true;
                }
                else if (cardValueSequence[i] == 0 && ventel)// проверка на выход из вентиляции.
                {
                    ventel = false;
                }

                if (cardValueSequence[i] < 4 && cardValueSequence[i] > 0 && !ventel) // проверка на мутацию.
                {
                    if (cardValueSequence[i] == 1) // меняем форму.
                    {
                        if (value >= 1100) { value -= 100; }
                        else { value += 100; }
                        Debug.Log(value);
                    }
                    else if (cardValueSequence[i] == 2) // меняем цвет.
                    {
                        if (value - 1010 == 100 || value - 1010 == 101 || value - 1010 == 0 || value - 1010 == 1)
                        {
                            value -= 10;
                        }
                        else
                        {
                            value += 10;
                        }
                        Debug.Log(value);

                    }
                    else  // меняем маркировку.
                    {
                        if (value % 2 != 0) { value -= 1; }
                        else { value += 1; }
                        Debug.Log(value);

                    }
                }
            }
        }
        else if (Static.direction == -1)                      // создание бесконечного цикла против часовой стрелки.
        {
            Debug.Log("-1");

            int i;
            for (i = startIndex; i > -2; i--)
            {
                iterations++;

                if (i == -1) i = cardValueSequence.Length-1;  // создание бесконечного цикла против часовой стрелки.

                if (cardValueSequence[i] == value && !ventel)  // проверка на соответствие значения.
                {
                    Static.iterations = iterations;
                   // obj = GameObject.FindGameObjectWithTag(i.ToString());
                    obj = GameObject.FindGameObjectWithTag(_sequence[i].ToString());
                    StartCoroutine("Pulse");
                   // Static.id = i;
                    Static.id = _sequence[i];
                    return;
                }

                if (cardValueSequence[i] == 0 && !ventel)  // проверка на вход в вентиляцию.
                {
                    ventel = true;
                }
                else if (cardValueSequence[i] == 0 && ventel) // проверка на выход из вентиляции.
                {
                    ventel = false;
                }

                if (cardValueSequence[i] < 4 && cardValueSequence[i] > 0 && !ventel) // проверка на мутацию.
                {
                    if (cardValueSequence[i] == 1) // меняем форму.
                    {
                        if (value >= 1100) { value -= 100; }
                        else { value += 100; }
                        Debug.Log(value);

                    }
                    else if (cardValueSequence[i] == 2) // меняем цвет.
                    {
                        if(value-1010==100 || value - 1010 == 101 || value - 1010 == 0 || value - 1010 == 1)
                        {
                            value -= 10;
                        }
                        else
                        {
                            value += 10;
                        }
                        Debug.Log(value);

                    }
                    else  // меняем маркировку.
                    {
                        if (value % 2 != 0) { value -= 1; }
                        else { value += 1; }
                        Debug.Log(value);

                    }
                }
            }
        }
    }

    private IEnumerator Pulse()
    {    
        yield return new WaitForSeconds(Static.wait*Static.iterations);//ожидаем
        Static.id = -2;//сбрасываем значение ручного выбора.
        Static.goodAnswer = 0;
        Static.isCardButtonsActive = false;//делаем карточки неактивными.

        Debug.Log("не успел!");
        Static.score++;
        
        message.text = "не успел!";

        for (int i = 0; i < 8; i++)
            {
                obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                yield return new WaitForSeconds(0.1f);
                obj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                yield return new WaitForSeconds(0.1f);
            }
        message.text = "";
        Static.isStartButtonActive = true;//делаем кнопку старт активной.
        Static.freezeThrowingDice = false;//размораживаем бросание костей.
        Static.diceValue = 0;//сбрасываем значения 


    }
    private IEnumerator Rotation()
    {
        Debug.Log("правильно");
        message.text = "правильно";
        Static.myScore++;

        obj = GameObject.FindGameObjectWithTag(Static.myId.ToString());
        Static.myId = -1;
        StopCoroutine("Pulse");
        Static.myId = -1;//сбрасываем значение ручного выбора.
        Static.isCardButtonsActive = false;//делаем карточки неактивными.
        Quaternion pos = obj.transform.rotation;//сохраняем исходное значение вращения
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.05f);
            obj.transform.Rotate(0, 0, -26);
        }
        message.text = "";
        obj.transform.rotation = pos;//возвращаем в исходное положение вращения.
        Static.freezeThrowingDice = false;//размораживаем бросание костей.
        Static.diceValue = 0;//сбрасываем значения 
        Static.isStartButtonActive = true;//делаем кнопку старт активной.
    }
    private IEnumerator QuickPulse()
    {
        Static.myId = -1;
        StopCoroutine("Pulse");
        Static.id = -2;//сбрасываем значение ручного выбора.
        Static.isCardButtonsActive = false;//делаем карточки неактивными.

        Debug.Log("не верно!");
        Static.score++;
        message.text = "не верно!";

        for (int i = 0; i < 8; i++)
        {
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            obj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            yield return new WaitForSeconds(0.1f);
        }
        message.text = "";
        Static.freezeThrowingDice = false;//размораживаем бросание костей.
        Static.diceValue = 0;//сбрасываем значения 
        Static.isStartButtonActive = true;//делаем кнопку старт активной.
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
