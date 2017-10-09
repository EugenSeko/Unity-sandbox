using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static_M {

    public static int gamesCount = 0;//количество сыгранных игр.
    public static bool throwDice = false;//активность запуска бросания кубиков.
    public static bool freezeThrowingDice = false;//активность бросание кубиков.
    public static bool isCardButtonsActive = false;//активность карточек.
    public static int diceValue = 0;//значение выпавших кубиков.
    public static int direction = 0;//направление поиска (по часой или против.)
    public static int startPoint = 0;// код лаборатории из которой начинается поиск.
    public static int iterations = 0;// количество итераций поиска.
    public static float wait = 1f; // задержка в секундах.
    public static int goodAnswer = 0;
    public static int numOfSequence = 0;
    public static int level = 1;

    public static bool isStartButtonActive = true;//значение активности стартовой кнопки.

    public static int id = -2;//ячейка для автоматически найденной карты.
    public static int myId = -1;//ячейка для найденной карты вручную.

    public static int myScore = 0;
    public static int score = 0;

    //MULTIPLAYER
    public static bool server = false;
    public static int playerId = -1;

    public static int numOfGames = 5;
    public static int numOfPlayers=0;
    public static int numOfPlayersReady = 0;

    public static bool go = false;
    public static bool countdownEnd = false;

    public static int[] DiceIndexes = new int[4];

    public static int[] PlayersScore = new int[4];








    public static void init()
    {
     gamesCount = 0;//количество сыгранных игр.
     go = false;//активность запуска бросания кубиков.
    freezeThrowingDice = false;//активность бросание кубиков.
     isCardButtonsActive = false;//активность карточек.
     diceValue = 0;//значение выпавших кубиков.
     direction = 0;//направление поиска (по часой или против.)
    startPoint = 0;// код лаборатории из которой начинается поиск.
    iterations = 0;// количество итераций поиска.
     wait = 0.5f; // задержка в секундах.
     goodAnswer = 0;

     isStartButtonActive = true;//значение активности стартовой кнопки.

     id = -2;//ячейка для автоматически найденной карты.
     myId = -1;//ячейка для найденной карты вручную.

     myScore = 0;
     score = 0;
}






    public static float[] getCoordinates(int id)
    {
        switch (id)
        {
            case 0:
                return new float[3] { -5.21f, -3.75f, -1f };
            case 1:
                return new float[3] { -5.21f, -2.5f, -1f };
            case 2:
                return new float[3] { -5.21f, -1.23f, -1f };
            case 3:
                return new float[3] { -5.21f, 0.07f, -1f };
            case 4:
                return new float[3] { -5.21f, 1.34f, -1f };
            case 5:
                return new float[3] { -5.21f, 2.65f, -1f };
            case 6:
                return new float[3] { -3.84f, 2.65f, -1f };
            case 7:
                return new float[3] { -2.51f, 2.65f, -1f };
            case 8:
                return new float[3] { -1.2f, 2.65f, -1f };
            case 9:
                return new float[3] { 0.04f, 2.65f, -1f };
            case 10:
                return new float[3] { 1.33f, 2.65f, -1f };
            case 11:
                return new float[3] { 2.61f, 2.65f, -1f };
            case 12:
                return new float[3] { 3.91f, 2.65f, -1f };
            case 13:
                return new float[3] { 5.29f, 2.65f, -1f };
            case 14:
                return new float[3] { 5.29f, 1.37f, -1f };
            case 15:
                return new float[3] { 5.29f, 0.1f, -1f };
            case 16:
                return new float[3] { 5.29f, -1.16f, -1f };
            case 17:
                return new float[3] { 5.29f, -2.42f, -1f };
            case 18:
                return new float[3] { 5.29f, -3.75f, -1f };
            case 19:
                return new float[3] { 3.94f, -3.75f, -1f };
            case 20:
                return new float[3] { 2.61f, -3.75f, -1f };
            case 21:
                return new float[3] { 1.25f, -3.75f, -1f };
            case 22:
                return new float[3] { 0.01f, -3.75f, -1f };
            case 23:
                return new float[3] { -1.32f, -3.75f, -1f };
            case 24:
                return new float[3] { -2.67f, -3.75f, -1f };
            case 25:
                return new float[3] { -3.95f, -3.75f, -1f };
            default:
                return null;
        }

    }
    public static float[]diceCoordinates(int id)
    {
        switch (id)
        {
            case 0:
                return new float[] { 0.87f, 0.97f, -1 };
            case 1:
                return new float[] { 3.77f, 0.94f, -1 };
            case 2:
                return new float[] { 3.77f, -1.84f, -1 };
            case 3:
                return new float[] { 0.96f, -1.87f, -1 };
            default:
                return null;
        }
            
    }
    public static int[]sequences(int i)
    {
        switch (i)
        {
            case 0:
                return new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            case 1:
                return new int[] { 3, 4, 2, 9, 8, 22, 6, 14, 15, 13, 7, 11, 17, 21, 1, 10, 16, 12, 23, 19, 20, 0, 25, 18, 24, 5 };
            case 2:
                return new int[] { 2, 1, 0, 3, 25, 6, 5, 7, 8, 9, 24, 11, 12, 13, 15, 14, 16, 18, 17, 19, 10, 22, 21, 23, 20, 4 };
            case 3:
                return new int[] { 0, 1, 22, 13, 4, 8, 6, 7, 5, 10, 9, 11, 12, 3, 14, 15, 16, 17, 18, 21, 24, 19, 2, 23, 20, 25 };
            default:
                return null;
        }
    }
    // 1.живой1    2.двуглазый1   3.светлый1   4.крапчатый1 
    // 1.лаборатория1     2.красный1  желтый2 синий3 
    // 1.мутация0         2.форма1  цвет2   маркировка3
    public static int[] cardValuesSequence = new int[26]
    {   0, 1111, 1000, 1010, 2,
        12, 1110, 1100, 1011, 0,
        1001, 1, 1001, 1011, 11,
        1101, 1111, 0, 1000, 1010,
        1110, 13, 3, 1100, 1101, 1};

}
