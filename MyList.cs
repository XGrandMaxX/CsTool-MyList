using System;
using System.Collections;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        MyList<int> list = new MyList<int>(3);
        Console.WriteLine($"list count = {list.Count}\n");
        list.Add(3);
        Console.WriteLine($"a new element is added to the list");
        for (int i = 0; i < list.Count; i++)
            Console.WriteLine(list[i]);
        Console.WriteLine($"list count = {list.Count}\n");
        list.Clear();
        Console.WriteLine($"list count = {list.Count}\n");

        list.Add(11);
        list.Add(23);
        list.Add(3);
        list.Add(6);
        list.Add(2);
        Console.WriteLine($"5 elements is added to the list");
        Console.WriteLine($"list count = {list.Count}\n");

        for (int i = 0; i < list.Count; i++)
            Console.WriteLine(list[i]);
        list.Sort();

        foreach (int i in list)
            Console.WriteLine(i);
        Console.WriteLine($"list count = {list.Count}\n");

        Console.WriteLine($"Removing from the array");
        list.RemoveAt(2);
        foreach (int i in list)
            Console.WriteLine(i);
        Console.WriteLine($"list count = {list.Count}\n");
        Console.ReadLine();
    }
}
class MyList<T> : IEnumerable<T>
{
    private int arraySize;
    private T[] array;
    public int Count { get => arraySize; }
    public MyList() => array = new T[0];
    public MyList(int capacity) => array = new T[capacity];
    public T this[int index]
    {
        get => array[index];
        set => array[index] = value;
    }

    /// <summary>
    /// Adds an element to the end of the list
    /// </summary>
    public void Add(T item)
    {
        if (arraySize == array.Length)
            Array.Resize(ref array, arraySize * 2);

        array[arraySize++] = item;
    }

    /// <summary>
    /// removes the item element from the list, and if the removal was successful. 
    /// then returns true. If there are several identical elements in the list, then only the first of them is removed.
    /// </summary>
    public bool Remove(T item)
    {
        int index = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Equals(item))
            {
                index = i;
                break;
            }
        }

        if (index != -1)
        {
            for (int i = index; i < array.Length - 1; i++)
                array[i] = array[i + 1];
            array[arraySize--] = default;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removing an element at the specified index.
    /// </summary>
    public void RemoveAt(uint index)
    {
        if (index >= arraySize)
            throw new ArgumentException();
        arraySize--;
        if (index < arraySize)
            Array.Copy(array, index + 1, array, index, arraySize - index);
        array[arraySize] = default;
    }

    /// <summary>
    /// Clears the entire list, leaving the list size at 1 so that elements can continue to be added to it
    /// </summary>
    public void Clear()
    {
        arraySize = 0;
        Array.Resize(ref array, 0);
    }

    /// <summary>
    /// Sorts the elements of the list depending on the selected sort type
    /// 1 - bubble sort 2 - not implemented
    /// </summary>
    public void Sort()
    {
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 1; j < arraySize; j++)
                if (Comparer<T>.Default.Compare(array[j - 1], array[j]) > 0)
                {
                    T temp = array[j - 1];
                    array[j - 1] = array[j];
                    array[j] = temp;
                }
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < arraySize; i++)
            yield return array[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
