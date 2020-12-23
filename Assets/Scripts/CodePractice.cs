using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePractice : MonoBehaviour
{
    int[] A = {3,2,9,5,1,4,6,7,0,8 };
    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        //number of time we traverse the list
        for(int i = 0; i < A.Length; i += 1)
        {
            //traversing the list j = list index
            for(int j = 0; j < A.Length - 1 - i; j += 1)
            {
                count += 1;
                if(A[j] > A[j + 1])
                {
                    int temp = A[j];
                    A[j] = A[j + 1];
                    A[j + 1] = temp;
                }
            }
            print(A);
        }
        Debug.Log(count);
    }

    void print(int[] A)
    {
        string str = "";
        foreach(int element in A)
        {
            str += element.ToString() + " ";
        }
        Debug.Log(str);
    }
}
