using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace KismySTD
{
    public class RandomShuffle : MonoBehaviour
    {

        public static string[] Shuffle(string[] a) {

            for (int i = 0; i < a.Length-1; i++)
            {
                string temp = a[i];
                a[i] =a[ Random.Range(i+1, a.Length)];
                a[Random.Range(i+1, a.Length)] = temp;
            }
            return a;
        }


    }
}
