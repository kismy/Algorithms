using UnityEngine;
using System.Collections;

public class TEST2: MonoBehaviour {

    void Start()
    {
        //FixedCapacityStackTest();
        //Capacity();
        //DyncCapacityStackTest();
        //ResizingArrayStackTest();
        //MyStackTest();
        //QueueTest();
        //TestBag();
        //TestSortK();
        //TestReverseK();
        //DateTest();
        //QueueDeleteEnd();
        //PointerTest();
        //StackDeleteEnd();
        //QueueDeleteStart();
        //QueueFind();
        //QueueInsert();
        //QueueRemoveKey();
        //QueueMax();
        //QueueReverse();
        //StackReverse();
        //DoubleNodeQueue_InsertInHead();
        //DoubleNodeQueueTest();
        QueueCopy();
    }


    void FixedCapacityStackTest()
    {
        //string[] str = new string[13] { "To", "be", "or", "not", "to", "-", "be", "-", "-", "that", "-", "-", "-" };
        int[] array = new int[13] { 00, -1, 22, 33, 44, -1, 66, 77, 88, 99, -1, 110, 120 };
        //FixedCapacityStack<string> stringStack = new FixedCapacityStack<string>(100);
        FixedCapacityStack<int> intStack = new FixedCapacityStack<int>(100);

        //stringStack...........................................
        //for (int i = 0; i < str.Length; i++)
        //{
        //    string item = str[i];
        //    if (!item.Equals("-"))
        //    {
        //        stringStack.Push(item);
        //        print("+" + item);
        //    }
        //    else if (!stringStack.IsEmpty()) print("-" + stringStack.POP());
        //}

        //for (int i = 0; i < stringStack.N; i++)
        //{
        //    print("Remain stringStack item:" + stringStack.a[i]);
        //}

        //intStack...........................................
        for (int i = 0; i < array.Length; i++)
        {
            int item = array[i];
            if (item != -1)
            {
                intStack.Push(item);
                print("+" + item);
            }
            else if (!intStack.IsEmpty()) print("-" + intStack.POP());
        }

        for (int i = 0; i < intStack.N; i++)
        {
            print("Remain intStack item:" + intStack.a[i]);
        }
    }
    //怎么在不话费额外空间下，改变数组的大小
    int[] B = new int[3] { 11, 22, 33 };
    void Capacity()
    {
        int[] A = new int[10];  //A数组的生命空间是Capcity（）函数内，运行结束自动进行垃圾回收     
        for (int i=0;i<B.Length;i++)
        {
            A[i] = B[i];
        }
        B = A;
        print("B.Length:"+B.Length);

        string str="B[]:";
        for (int i = 0; i < B.Length; i++)
        {
            str += B[i].ToString()+"\t";
        }
        print(str);
    }
    void DyncCapacityStackTest()
    {
        string[] str = new string[14] { "To", "be", "or", "not", "to", "-", "be", "-", "-", "that", "-", "-", "-" ,"is"};
        DyncCapacityStack<string> stringStack = new DyncCapacityStack<string>(1);

        //stringStack...........................................
        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                print("+" + item);
                stringStack.Push(item);
                print("capacity.Length:" + stringStack.capacity.Length);
            }
            else if (!stringStack.IsEmpty())
            {
                print("-" + stringStack.POP());
                print("capacity.Length:" + stringStack.capacity.Length);

            }
        }

        for (int i = 0; i < stringStack.N; i++)
        {
            print("Remain str item:"+ stringStack.capacity[i]);
        }
    }
    void ResizingArrayStackTest()
    {
        //string[] str = new string[14] { "To", "be", "or", "not", "to", "-", "be", "-", "-", "that", "-", "-", "-", "is" };
        int[] array = new int[13] { 00, -1, 22, 33, 44, -1, 66, 77, 88, 99, -1, 110, 120 };
        //ResizingArrayStack<string> stringStackTerator = new ResizingArrayStack<string>();
        ResizingArrayStack<int> intStackTerator = new ResizingArrayStack<int>();

        //for (int i = 0; i < str.Length; i++)
        //{
        //    string item = str[i];
        //    if (!item.Equals("-"))
        //    {              
        //        stringStackTerator.Push(item);             
        //    }
        //    else if (!stringStackTerator.IsEmpty())
        //    {
        //        stringStackTerator.POP();
        //    }
        //}

        //for (int i = 0; i < array.Length; i++)
        //{
        //    int item = array[i];
        //    if (item != -1)
        //    {
        //        intStackTerator.push(item);
              
        //    }else if (!intStackTerator.IsEmpty()) intStackTerator.POP();
        //}

        //foreach (string item in stringStackTerator)
        //{
        //    print(item);
        //}

        //for (int i = 0; i < intStackTerator.size(); i++)
        //{
        //    print("For[i] item:" + intStackTerator.values[i]);
        //}
        foreach (int item in intStackTerator)
        {
            print("Foreach item:" + item);
        }
    }
    private void MyStackTest()
    {
        Stack<string> stack = new Stack<string>();

        string[] str = new string[14] { "To", "be", "or", "not", "to", "-", "be", "-", "-", "that", "-", "-", "-", "is" };

        
        for (int i = 0; i < str.Length; i++) {
            string item =str[i];
            if (!item.Equals("-"))
                stack.push(item);
            else if (!stack.isEmpty())
                stack.pop();
        }
            

        foreach (string T in stack)
        {
            print(Stack<string>.n+":"+T);
        }     
    }
    private void QueueTest()
    {
        Queue<string> queue = new Queue<string>();

        string[] str = new string[14] { "To", "be", "or", "not", "to", "-", "be", "-", "-", "that", "-", "-", "-", "is" };



        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                queue.Enqueue(item);
                print("+" + item);
            }
            else if (!queue.isEmpty()) print("-" + queue.Dequeue());
        }


        foreach (string T in queue)
        {
            print("Remain:"+Queue<string>.n + ":" + T);
        }


    }
    private void TestBag()
    {
        Bag<string> bag = new Bag<string>();

        string[] str = new string[14] { "To", "be", "or", "not", "to", "-", "be", "-", "-", "that", "-", "-", "-", "is" };


        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                bag.Add(item);
                print("+" + item);
            }
        }


        foreach (string T in bag)
        {
            print("bag:" + Bag<string>.n + ":" + T);
        }


    }
    void TestSortK()
    {

        Queue<string> queue = new Queue<string>();

        string[] str = new string[10] { "To", "be", "or", "not", "to", "be", "that", "is", "a", "question." };


        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                queue.Enqueue(item);
                print("+" + item);
            }
            else if (!queue.isEmpty()) print("-" + queue.Dequeue());
        }

        foreach (string T in queue)
        {
            print("Remain:" + Queue<string>.n + ":" + T);
        }

        print("第二个元素："+queue.SortPeek(1));
    }
    void TestReverseK()
    {

        Queue<string> queue = new Queue<string>();

        string[] str = new string[10] { "To", "be", "or", "not", "to", "be", "that", "is", "a", "question." };


        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                queue.Enqueue(item);
                print("+" + item);
            }
            else if (!queue.isEmpty()) print("-" + queue.Dequeue());
        }

        foreach (string T in queue)
        {
            print("Remain:" + Queue<string>.n + ":" + T);
        }

        print("第二个元素：" + queue.SortPeek(3));
        print("倒数第5个元素：" + queue.ReversePeek(6));
    }
    void DateTest()
    {
        Date.ReadDates("2017.08.18");
    }
    void QueueDeleteEnd()
    {
        Queue<string> queue = new Queue<string>();

        string[] str = new string[10] { "To", "be", "or", "not", "to", "be", "that", "is", "a", "question." };


        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                queue.Enqueue(item);
                print("+" + item);
            }
            else if (!queue.isEmpty()) print("-" + queue.Dequeue());
        }

        int index = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index + "]:" + T);
            index++;
        }

        print("被删掉的末尾元素：" + queue.RemoveNthFromEnd(2));  //删除元素操作

        int index2 = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index2 + "]:" + T);
            index2++;
        }

    }
    void QueueDeleteStart()
    {
        Queue<string> queue = new Queue<string>();

        string[] str = new string[10] { "To", "be", "or", "not", "to", "be", "that", "is", "a", "question." };


        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                queue.Enqueue(item);
                print("+" + item);
            }
            else if (!queue.isEmpty()) print("-" + queue.Dequeue());
        }

        int index = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index + "]:" + T);
            index++;
        }

        print("被删掉的第二个元素：" + queue.RemoveNthFromStart(2));  //删除元素操作

        int index2 = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index2 + "]:" + T);
            index2++;
        }

    }
    void QueueFind()
    {
        Queue<string> queue = new Queue<string>();

        string[] str = new string[10] { "To", "be", "or", "not", "to", "be", "that", "is", "a", "question." };


        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                queue.Enqueue(item);
                print("+" + item);
            }
            else if (!queue.isEmpty()) print("-" + queue.Dequeue());
        }

        int index = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index + "]:" + T);
            index++;
        }

        print("查找question：" + queue.Find("question."));  //删除元素操作

        int index2 = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index2 + "]:" + T);
            index2++;
        }

    }
    void QueueInsert()
    {
        Queue<string> queue = new Queue<string>();

        string[] str = new string[10] { "To", "be", "or", "not", "to", "be", "that", "is", "a", "question." };


        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                queue.Enqueue(item);
                print("+" + item);
            }
            else if (!queue.isEmpty()) print("-" + queue.Dequeue());
        }

        int index = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index + "]:" + T);
            index++;
        }

        print("在value=‘question.’后面插入‘End.’");
        queue.Insert("question.", "End.");

        int index2 = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index2 + "]:" + T);
            index2++;
        }

    }
    void QueueRemoveKey()
    {
        Queue<string> queue = new Queue<string>();

        string[] str = new string[10] { "question", "be", "or", "not", "to", "be", "that", "is", "a", "question" };


        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                queue.Enqueue(item);
                print("+" + item);
            }
            else if (!queue.isEmpty()) print("-" + queue.Dequeue());
        }

        int index = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index + "]:" + T);
            index++;
        }

        print("移除queue的所有'question'");
        queue.RemoveKey("question");

        int index2 = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index2 + "]:" + T);
            index2++;
        }
    }
    void QueueMax()
    {
        Queue<int> queue = new Queue<int>();

        int[] nums = new int[10] {9,7,8,6,4,5,2,3,1,0 };


        for (int i = 0; i < nums.Length; i++)
        {
            queue.Enqueue(nums[i]);
        }

        int index = 0;
        foreach (int T in queue)
        {
            print("queue[ " + index + "]:" + T);
            index++;
        }

        print("queue.Max="+queue.Max());
     

        int index2 = 0;
        foreach (int T in queue)
        {
            print("queue[ " + index2 + "]:" + T);
            index2++;
        }
    }
    void QueueReverse()
    {
        Queue<int> queue = new Queue<int>();

        int[] nums = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };


        for (int i = 0; i < nums.Length; i++)
        {
            queue.Enqueue(nums[i]);
        }

        int index = 0;
        foreach (int T in queue)
        {
            print("queue[ " + index + "]:" + T);
            index++;
        }

        print("反转queue队列元素后，queue的以一个元素：" + Queue<int>.Reverse(ref queue).item);
        


        int index2 = 0;
        foreach (int T in queue)
        {
            print("queue[ " + index2 + "]:" + T);
            index2++;
        }
    }
    void StackReverse()
    {
        Stack<int> stack = new Stack<int>();

        int[] nums = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };


        for (int i = 0; i < nums.Length; i++)
        {
            stack.push(nums[i]);
        }

        int index = 0;
        foreach (int T in stack)
        {
            print("stack[ " + index + "]:" + T);
            index++;
        }

        print("反转stack队列元素后，stack的第一个元素：" + Stack<int>.Reverse(ref stack).item);



        int index2 = 0;
        foreach (int T in stack)
        {
            print("stack[ " + index2 + "]:" + T);
            index2++;
        }
    }
    void PointerTest() {
        Queue<string> queue = new Queue<string>();

        string[] str = new string[10] { "To", "be", "or", "not", "to", "be", "that", "is", "a", "question." };


        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                queue.Enqueue(item);
                print("+" + item);
            }
            else if (!queue.isEmpty()) print("-" + queue.Dequeue());
        }

        int index = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index + "]:" + T);
            index++;
        }

        print("修改First副本，修改queue队列..." );  //删除元素操作
        queue.Pointer();

        int index2 = 0;
        foreach (string T in queue)
        {
            print("queue[ " + index2 + "]:" + T);
            index2++;
        }
    }
    void StackDeleteEnd()
    {
        Stack<string> stack = new Stack<string>();

        string[] str = new string[10] { "To", "be", "or", "not", "to", "be", "that", "is", "a", "question" };


        for (int i = 0; i < str.Length; i++)
        {
            string item = str[i];
            if (!item.Equals("-"))
            {
                stack.push(item);
                print("+" + item);
            }
            else if (!stack.isEmpty()) print("-" + stack.pop());
        }

        int index = 0;
        foreach (string T in stack)
        {
            print("stack[ " + index + "]:" + T);
            index++;
        }

        print("被删掉的末尾元素：" + stack.RemoveNthFromEnd(1));  //删除元素操作

        int index2 = 0;
        foreach (string T in stack)
        {
            print("stack[ " + index2 + "]:" + T);
            index2++;
        }

    }
    void DoubleNodeQueue_InsertInHead()
    {
        Queue_2Node<int> queue2node = new Queue_2Node<int>();

        int[] nums = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };


        for (int i = 0; i < nums.Length; i++)
        {
            queue2node.Enqueue(nums[i]);
        }

        int index = 0;
        foreach (int T in queue2node)
        {
            print("queue2node[ " + index + "]:" + T);
            index++;
        }

        print("在首结点插入元素：-1" );
        queue2node.InsertInHead(-1);



        int index2 = 0;
        foreach (int T in queue2node)
        {
            print("queue2node[ " + index2 + "]:" + T);
            index2++;
        }
    }
    void DoubleNodeQueueTest()
    {
        Queue_2Node<int> queue2node = new Queue_2Node<int>();

        int[] nums = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };


        for (int i = 0; i < nums.Length; i++)
        {
            queue2node.Enqueue(nums[i]);
        }

        int index = 0;
        string str=null;
        foreach (int T in queue2node)
        {
            str+="[ " + index + "]:" + T+"\t";
            index++;
        }
        print(str);

        print("在首结点插入元素-1");
        queue2node.InsertInHead(-1);
        Node<int> current = queue2node.first.next;
        print(current.prior.item);
        print(current.item);
        print(current.next.item);
    }
    void QueueCopy()
    {
        Queue_2Node<int> queue2node = new Queue_2Node<int>();

        int[] nums = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };


        for (int i = 0; i < nums.Length; i++)
        {
            queue2node.Enqueue(nums[i]);
        }

        int index = 0;
        string str = null;
        foreach (int T in queue2node)
        {
            str += "[ " + index + "]:" + T + "\t";
            index++;
        }
        print(str);

        print("复制队列副本...");
        Queue_2Node<int> queueCopy = new Queue_2Node<int>(queue2node);
        print(" queue2node.addFirst(-1);");
        queue2node.addFirst(-1);

        int index2 = 0;
        string str2 = null;
        foreach (int T in queueCopy)
        {
            str2 += "[ " + index2 + "]:" + T + "\t";
            index2++;
        }
        print("Show 副本："+str2);



    }

}
