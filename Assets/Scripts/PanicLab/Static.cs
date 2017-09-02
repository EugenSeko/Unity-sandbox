using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static {

    public static int gamesCount = 0;//количество сыгранных игр.
    public static bool throwDice = false;//активность запуска бросания кубиков.
    public static bool freezeThrowingDice = false;//активность бросание кубиков.
    public static bool isCardButtonsActive = false;//активность карточек.
    public static int diceValue = 0;//значение выпавших кубиков.
    public static int direction = 0;//направление поиска (по часой или против.)
    public static int startPoint = 0;// код лаборатории из которой начинается поиск.
    public static int iterations = 0;// количество итераций поиска.
    public static float wait = 0.3f; // задержка в секундах.
    public static int goodAnswer = 0;

    public static bool isStartButtonActive = true;//значение активности стартовой кнопки.

    public static int id = -2;//ячейка для автоматически найденной карты.
    public static int myId = -1;//ячейка для найденной карты вручную.






    public static float[] getCoordinates(int id)
    {
        switch (id)
        {
            case 0:
                return new float[3] { -8.03f, -3.51f, -1f };
            case 1:
                return new float[3] { -8.05f, -1.68f, -1f };
            case 2:
                return new float[3] { -8.08f, 0.1f, -1f };
            case 3:
                return new float[3] { -8.08f, 1.85f, -1f };
            case 4:
                return new float[3] { -8.1f, 3.65f, -1f };
            case 5:
                return new float[3] { -6.3f, 3.65f, -1f };
            case 6:
                return new float[3] { -4.47f, 3.65f, -1f };
            case 7:
                return new float[3] { -2.66f, 3.65f, -1f };
            case 8:
                return new float[3] { -0.82f, 3.65f, -1f };
            case 9:
                return new float[3] { 1.02f, 3.65f, -1f };
            case 10:
                return new float[3] { 2.87f, 3.65f, -1f };
            case 11:
                return new float[3] { 4.77f, 3.65f, -1f };
            case 12:
                return new float[3] { 6.63f, 3.65f, -1f };
            case 13:
                return new float[3] { 8.46f, 3.61f, -1f };
            case 14:
                return new float[3] { 8.46f, 1.88f, -1f };
            case 15:
                return new float[3] { 8.46f, 0.12f, -1f };
            case 16:
                return new float[3] { 8.46f, -1.7f, -1f };
            case 17:
                return new float[3] { 8.46f, -3.51f, -1f };
            case 18:
                return new float[3] { 6.62f, -3.51f, -1f };
            case 19:
                return new float[3] { 4.78f, -3.51f, -1f };
            case 20:
                return new float[3] { 2.93f, -3.51f, -1f };
            case 21:
                return new float[3] { 1.07f, -3.51f, -1f };
            case 22:
                return new float[3] { -0.81f, -3.51f, -1f };
            case 23:
                return new float[3] { -2.63f, -3.51f, -1f };
            case 24:
                return new float[3] { -4.44f, -3.51f, -1f };
            case 25:
                return new float[3] { -6.24f, -3.51f, -1f };
            default:
                return null;
        }

    }
    public static float[]diceCoordinates(int id)
    {
        switch (id)
        {
            case 0:
                return new float[] { -1.46f, 1.54f, -1 };
            case 1:
                return new float[] { 1.44f, 1.51f, -1 };
            case 2:
                return new float[] { 1.44f, -1.27f, -1 };
            case 3:
                return new float[] { -1.37f, -1.3f, -1 };
            default:
                return null;
        }
            
    }
    // 1.живой1    2.двуглазый1   3.светлый1   4.крапчатый1 
    // 1.лаборатория1     2.красный1  желтый2 синий3 
    // 1.мутация0         2.форма1  цвет2   маркировка3
    public static int[] cardSequence = new int[26]
    {   0, 1111, 1000, 1010, 2,
        12, 1110, 1100, 1011, 0,
        1001, 1, 1001, 1011, 11,
        1101, 1111, 0, 1000, 1010,
        1110, 13, 3, 1100, 1101, 1};

}
