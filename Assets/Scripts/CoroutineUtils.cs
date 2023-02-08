using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineUtils
{
    public static IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
