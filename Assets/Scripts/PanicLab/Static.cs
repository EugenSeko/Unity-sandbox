using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static  {

    public static int setCount = 0;

    

    public static float[] getCoordinates(int id)
    {
        switch (id)
        {
            case 0:
                return new float[3] { -7.5f, -0.02384877f, -1f };
            case 1:
                return new float[3] { 0, 0, -1f };
            case 2:
                return new float[3] { -1.5f, -0.02384877f, -1f };
            default:
                return null;
        }

    }

}
