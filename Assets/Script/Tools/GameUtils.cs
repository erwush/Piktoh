using UnityEngine;

public static class GameUtils

{

    public static int FloatArrayChecker(float[] array, float value, float tolerance = 1f)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (Mathf.Abs(array[i] - value) < tolerance)
            {
                return i;
            }
        }
        return -1;
    }
    public static int ArrayChecker<T>(T[] array, T value)
    {

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Equals(value))
            {
                return i;
            }
        }

        return -1;
    }
}