using System;
using System.Collections;
using System.Collections.Generic;

internal class MyList<T> : IEnumerable<T>
{
    private int arraySize;
    private T[] array;
    public int Capacity
    {
        get
        {
            int count = 0;
            foreach (var item in array)
            {
                if (item != null)
                    count++;
                else
                    break;
            }
            return count;
        }
    }

    public int Count
    {
        get { return arraySize; }
    }
    public MyList()
    {
        { array = new T[0]; }
    }
    public MyList(int capacity)
    {
        array = new T[capacity];
    }
    public T this[int index]
    {
        get { return array[index]; }
        set { array[index] = value; }
    }

    /// <summary>
    /// Adds an element to the end of the list
    /// </summary>
    public void Add(T item)
    {
        if (arraySize == Capacity)
            Array.Resize(ref array, arraySize * 2 + 1);

        array[arraySize++] = item;
    }

    /// <summary>
    /// removes the item element from the list, and if the removal was successful. 
    /// then returns true. If there are several identical elements in the list, then only the first of them is removed.
    /// </summary>
    public bool Remove(T item)
    {
        int index = -1;
        for (int i = 0; i < arraySize; i++)
        {
            if (array[i].Equals(item))
            {
                index = i;
                break;
            }
        }

        if (index != -1)
        {
            for (int i = index; i < arraySize - 1; i++)
                array[i] = array[i + 1];
            array[--arraySize] = default;
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
    /// Base bubble sort
    /// </summary>
    public void Sort()
    {
        T temp;
        for (int i = 0; i < arraySize; i++)
        {
            for (int j = 1; j < arraySize; j++)
                if (Comparer<T>.Default.Compare(array[j - 1], array[j]) > 0)
                {
                    temp = array[j - 1];
                    array[j - 1] = array[j];
                    array[j] = temp;
                }
        }
    }

    /// <summary>
    /// Flips the list completely
    /// </summary>
    public void Reverse()
    {
        T temp;
        for (int i = 0; i < arraySize / 2; i++)
        {
            temp = array[i];
            array[i] = array[arraySize - i - 1];
            array[arraySize - i - 1] = temp;
        }
    }

    /// <summary>
    /// Copies the list to array
    /// </summary>
    public void CopyTo(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = this.array[i];
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
