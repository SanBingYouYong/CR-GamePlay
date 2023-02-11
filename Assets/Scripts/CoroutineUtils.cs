using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineUtils
{
    /// <summary>
    /// To simulate a temporary pause behavior. 
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static IEnumerator Sleep(float seconds)
    {
        Time.timeScale = 0.0001f;
        yield return new WaitForSecondsRealtime(seconds * Time.timeScale);
        Time.timeScale = 1;
    }
}
