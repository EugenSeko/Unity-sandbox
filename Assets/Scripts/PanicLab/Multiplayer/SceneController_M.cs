using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController_M : MonoBehaviour {

    //Звук
    [SerializeField] private AudioSource[] soundSources;
    [SerializeField] private AudioClip[] audioClips;

    //Звук

    [SerializeField] private Sprite[] images;

    [SerializeField] private Sprite[] dice_images0;
    [SerializeField] private Sprite[] dice_images1;
    [SerializeField] private Sprite[] dice_images2;
    [SerializeField] private Sprite[] dice_images3;

    [SerializeField] private TextMesh gamesCountLabel;
    [SerializeField] private TextMesh message;
    [SerializeField] private TextMesh myScore;
    [SerializeField] private TextMesh score;

    [SerializeField] private TextMesh scoringMessage;
    [SerializeField] private TextMesh levelLabel;

    [SerializeField] private Card_M originalCard;
    private int[] _cardValuesSequence;
    private int[] _sequence;
    private GameHelper _gameHelper;



	private void Start () {
        _gameHelper = GameObject.FindObjectOfType<GameHelper>();//ссылка на на объект GameHelper и его скрипт.


        _sequence = Static_M.sequences(Static_M.numOfSequence);
        _cardValuesSequence = new int[26];
        for (int i = 0; i < 26; i++)
        {
            _cardValuesSequence[i] = Static_M.cardValuesSequence[_sequence[i]];
        }

        for (int i = 0; i < 26 ; i++)
        {
            int index = _sequence[i];

            Card_M card; // ссылка на контейнер для карты.
            card = Instantiate(originalCard) as Card_M; // создание карты согласно базовой.
            card.setCard(index, images[index], Static_M.getCoordinates(i));//расположение объекта согласно координатам.
            card.gameObject.name = _cardValuesSequence[i].ToString();//изменение имени объекта согласно кодировки.
            card.gameObject.tag = index.ToString();// добавление тэга для поиска.
        }
	}

    private void Update()
    {
        //score.text = Static_M.score.ToString();
        //myScore.text = Static_M.myScore.ToString();
        //gamesCountLabel.text = "Round " + Static_M.gamesCount;//выводим на экран число игр.
        //levelLabel.text = "Level "+Static_M.level.ToString();
    }

    public void  ThrowingDice()
    {
        if (!Static_M.freezeThrowingDice)
        {
            Static_M.gamesCount++;
            if (Static_M.gamesCount % 3 == 0)//периодически меняем раскладку карт.
            {
                if (Static_M.numOfSequence == 3)
                {
                    Static_M.numOfSequence = 0;
                }
                else
                {
                    Static_M.numOfSequence++;
                }
                DestroyCards();
                Start();
            }

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

            Card_M card; // ссылка на контейнер для карты.
            card = Instantiate(originalCard) as Card_M; // создание карты согласно базовой.
            if (Static_M.server)
            {
                Static_M.DiceIndexes[0] = Random.Range(0, dice_images0.Length);
            }
            card.setCard(0, dice_images0[index = Static_M.DiceIndexes[0]], Static_M.diceCoordinates(indexes[0]));
            card.tag = "dices";

            switch (Static_M.numOfSequence)  // определяем направление в соответствии с раскладкой карт.
            {
                case 0: if (index < 3) direction = -1;
                    break;
                case 1: if (index == 2 || index > 3) direction = -1;
                    break;
                case 2: if (index < 3) direction = -1;
                    break;
                case 3: if (index < 3) direction = -1;
                    break;
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

            card = Instantiate(originalCard) as Card_M; // создание карты согласно базовой.
            if (Static_M.server)
            {
                Static_M.DiceIndexes[1] = Random.Range(0, dice_images1.Length);
            }
            card.setCard(0, dice_images1[index = Static_M.DiceIndexes[1]], Static_M.diceCoordinates(indexes[1]));
            card.tag = "dices";
            if (index == 0) diceValue += 10;

            card = Instantiate(originalCard) as Card_M; // создание карты согласно базовой.
            if (Static_M.server)
            {
                Static_M.DiceIndexes[2] = Random.Range(0, dice_images2.Length);
            }
            card.setCard(0, dice_images2[index = Static_M.DiceIndexes[2]], Static_M.diceCoordinates(indexes[2]));
            card.tag = "dices";
            if (index == 1) diceValue += 1;

            card = Instantiate(originalCard) as Card_M; // создание карты согласно базовой.
            if (Static_M.server)
            {
                Static_M.DiceIndexes[3] = Random.Range(0, dice_images3.Length);
            }
            card.setCard(0, dice_images3[index = Static_M.DiceIndexes[3]], Static_M.diceCoordinates(indexes[3]));
            card.tag = "dices";
            if (index == 0) diceValue += 100;


            //???
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("dices").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("dices")[i],60); //удаление карточек костей через 60сек.
            }//???

            Static_M.diceValue = diceValue;
            Static_M.startPoint = labValue;
            Static_M.direction = direction;

            Static_M.throwDice = false; // отключаем автоматический запуск метода.
            Static_M.freezeThrowingDice = true;//замораживаем бросание костей.

            soundSources[0].PlayOneShot(audioClips[0]);//звуковой эффект.
            if (Static_M.server)
            {
                Searching();    //запускаем поиск.
            }
        }
    }

    private void Searching()
    {
        int[] cardValueSequence = _cardValuesSequence.Clone() as int[];
        int value = Static_M.diceValue;
        int startIndex = -1;
        bool ventel = false;
        int iterations = 0; //количество итераций поиска.



        for (int i = 0; i < cardValueSequence.Length; i++) // находим стартовый индекс с которого начнем движение.
        {
            if (cardValueSequence[i] == Static_M.startPoint) { startIndex = i; break; }
            iterations++;
        }

        if (Static_M.direction == 1)   // создание бесконечного цикла по часовой стрелке.
        {
            int i;
            for ( i = startIndex; i <= cardValueSequence.Length; i++)
            {
                iterations++;

                if (i == cardValueSequence.Length) i = 0;// создание бесконечного цикла по часовой стрелке.

                if (cardValueSequence[i] == value && !ventel) // проверка на соответствие значения.
                {
                    Static_M.iterations = iterations;
                    Static_M.id = _sequence[i];
                    _gameHelper.SendSearchDataOnClient();//рассылаем клиентам значение правильного выбора.
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

                    }
                    else  // меняем маркировку.
                    {
                        if (value % 2 != 0) { value -= 1; }
                        else { value += 1; }
                    }
                }
            }
        }
        else if (Static_M.direction == -1)                      // создание бесконечного цикла против часовой стрелки.
        {

            int i;
            for (i = startIndex; i > -2; i--)
            {
                iterations++;

                if (i == -1) i = cardValueSequence.Length-1;  // создание бесконечного цикла против часовой стрелки.

                if (cardValueSequence[i] == value && !ventel)  // проверка на соответствие значения.
                {
                    Static_M.iterations = iterations;
                    Static_M.id = _sequence[i];
                    _gameHelper.SendSearchDataOnClient();//рассылаем клиентам значение правильного выбора.
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
                    }
                    else  // меняем маркировку.
                    {
                        if (value % 2 != 0) { value -= 1; }
                        else { value += 1; }
                    }
                }
            }
        }
    }

    private IEnumerator Pulse()
    {
        // yield return new WaitForSeconds(Static_M.wait*Static_M.iterations);//ожидаем
        Static_M.isCardButtonsActive = false;

          GameObject  obj = GameObject.FindGameObjectWithTag(Static_M.id.ToString());

       // Static_M.id = -2;
       // Static_M.myId = -1;//сбрасываем значение ручного выбора.
        Static_M.goodAnswer = 0;

        message.text = "не успел!";
        for (int i = 0; i < 8; i++)
            {
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            soundSources[0].PlayOneShot(audioClips[1]);//звуковой эффект
            yield return new WaitForSeconds(0.05f);
            obj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            soundSources[0].PlayOneShot(audioClips[1]);//звуковой эффект
            yield return new WaitForSeconds(0.05f);
            }
        message.text = "";
        Static_M.isStartButtonActive = true;//делаем кнопку старт активной.
        Static_M.freezeThrowingDice = false;//размораживаем бросание костей.
        Static_M.diceValue = 0;//сбрасываем значения

        if (Static_M.score == 10 || Static_M.myScore == 10)//запускаем подсчет очков, при достижении одной из сторон 10 выигрышей.
        {
            Scoring();
        }

    }
    private IEnumerator Rotation()
    {
        Static_M.isCardButtonsActive = false;

        message.text = "правильно";
        Static_M.myScore++;

        GameObject obj = GameObject.FindGameObjectWithTag(Static_M.myId.ToString());
       // Static_M.myId = -1;//сбрасываем значение ручного выбора.
      //  Static_M.id = -2;//сбрасываем значение ручного выбора.
        Quaternion pos = obj.transform.rotation;//сохраняем исходное значение вращения
        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(0.05f);
            obj.transform.Rotate(0, 0, -26);
            soundSources[0].PlayOneShot(audioClips[0]);//звуковой эффект
        }
        message.text = "";
        obj.transform.rotation = pos;//возвращаем в исходное положение вращения.
        Static_M.freezeThrowingDice = false;//размораживаем бросание костей.
        Static_M.diceValue = 0;//сбрасываем значения 
        Static_M.isStartButtonActive = true;//делаем кнопку старт активной.

        if (Static_M.gamesCount>= Static_M.numOfGames)//запускаем подсчет очков.
        {
            _gameHelper.SendLoadScore();
        }
    }
    private IEnumerator QuickPulse()
    {
        GameObject obj = GameObject.FindGameObjectWithTag(Static_M.id.ToString());

       // Static_M.myId = -1;//сбрасываем значение ручного выбора.
        //Static_M.id = -2;
        Static_M.isCardButtonsActive = false;//делаем карточки неактивными.

        Static_M.score++;
        message.text = "не верно!";


        for (int i = 0; i < 8; i++)
        {
            soundSources[0].PlayOneShot(audioClips[1]);//звуковой эффект
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(0.05f);
            soundSources[0].PlayOneShot(audioClips[1]);//звуковой эффект
            obj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            yield return new WaitForSeconds(0.05f);
        }
        message.text = "";
        Static_M.freezeThrowingDice = false;//размораживаем бросание костей.
        Static_M.diceValue = 0;//сбрасываем значения 
        Static_M.isStartButtonActive = true;//делаем кнопку старт активной.
    }

    private void Scoring()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0, -11.8f, -100);

        if (Static_M.score < Static_M.myScore)
            {
                int diff = Static_M.myScore - Static_M.score;
                
                if (diff == 10)
                {
                    GameObject.Find("sc1").transform.position = new Vector3(0.06f, -11.33f, -2);
                scoringMessage.text = "Крутая победа без потерь, достойная настоящего чемпиона.";
                }
                else if (diff >6)
                {
                    GameObject.Find("sc2").transform.position = new Vector3(0.06f, -11.33f, -2);
                scoringMessage.text = "Ровная победа, переход на уровень " + Static_M.level+1;
                 }
                else if (diff >2)
                {
                    GameObject.Find("sc3").transform.position = new Vector3(0.06f, -11.33f, -2);
                scoringMessage.text = "Трудное противостояние, и Вы переходите на уровень " + Static_M.level+1;
                 }
               else 
                {
                    GameObject.Find("sc4").transform.position = new Vector3(0.06f, -11.33f, -2);
                scoringMessage.text = "Ух, чуть не проиграл, на следующем уровне будет реально трудно!";
                }
            Static_M.wait -= 0.15f;//переход на другой уровень.
            Static_M.level++;
        }
        else if (Static_M.score == Static_M.myScore)
            {
            GameObject.Find("draw").transform.position = new Vector3(0.06f, -11.33f, -2);
            scoringMessage.text = "Бег на месте общепримеряющий...";
        }
        else
            {
            GameObject.Find("loser").transform.position = new Vector3(0.06f, -11.33f, -2);
            scoringMessage.text = "Ничего, это был тяжелый уровень, в другой раз повезет.";
            if (Static_M.wait != 1f)
            {
                Static_M.wait += 0.15f;//переход на более низкий уровень.
                Static_M.level--;
            }
            

        }

        Static_M.gamesCount = 0;
            Static_M.score = 0;
            Static_M.myScore = 0;
    }
    public static void ScoringExit()
    {
        GameObject.Find("sc1").transform.position = new Vector3(0.06f, -11.33f, 1);
        GameObject.Find("sc2").transform.position = new Vector3(0.06f, -11.33f, 1);
        GameObject.Find("sc3").transform.position = new Vector3(0.06f, -11.33f, 1);
        GameObject.Find("sc4").transform.position = new Vector3(0.06f, -11.33f, 1);
        GameObject.Find("draw").transform.position = new Vector3(0.06f, -11.33f, 1);
        GameObject.Find("loser").transform.position = new Vector3(0.06f, -11.33f, 1);

    }



    private void DestroyCards()
    {
        for (int i = 0; i < 26; i++)
        {
            // Destroy(GameObject.Find(_cardValuesSequence[i].ToString()));
            Destroy(GameObject.FindGameObjectWithTag(i.ToString()));
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



}
