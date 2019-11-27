using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Kismy : MonoBehaviour {
    private int comeBack;
    public Sprite[] images;
    private Sprite[] imagesBackUp= new Sprite[9];
    public Sprite tempImage;
    private int temp;

    public Text text;



    public GameObject[]  showTexture;

    
    // Use this for initialization
    void Start () {
        //测试排序
        int[] testArray = new int[10] { 2, 1, 0, 5, 4, 3, 6, 9, 8, 7 };
        ArraySort(testArray);

        for (int i=0;i<images.Length;i++) imagesBackUp[i] = images[i];
        comeBack = 0;
        order();
    }

    public void UpsetTheOrder()
    {
        comeBack++;
        if (comeBack % 2 == 1)
        {
            text.text = "恢复正序";
            int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            for (int i = array.Length - 1; i > 0; i--)
            {
                int p = Random.Range(0, i);
                temp = array[p]; tempImage = images[p];

                array[p] = array[i]; images[p] = images[i];
                array[i] = temp; images[i] = tempImage;
            }
            foreach (int e in array) showTexture[e].GetComponent<Image>().sprite = images[e];

        }else {
            order();
        }       

       
    }


    public void order()
    {
        text.text = "开始洗牌";
        for (int i=0;i<= showTexture.Length-1;i++)showTexture[i].GetComponent<Image>().sprite = imagesBackUp[i];
    }

    public static void ArraySort(int[] myInts )
    {
        
        for (int i=myInts.Length-1;i>0;i-- )
        {
            for (int j = i-1; j >= 0; j--)
            {
                if (myInts[i] < myInts[j])
                {
                    int temp = myInts[i];
                    myInts[i] = myInts[j];
                    myInts[j] = temp;
                }

            }
        }

        foreach (int ele in myInts)
        {
            print(ele);
        }

    }

}
