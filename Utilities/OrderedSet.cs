using System.Collections;
using System.Collections.Generic;

namespace Utilities
{
  public class OrderedSet<T> : ICollection<T>
  {
    IDictionary<T, LinkedListNode<T>> mMap = new Dictionary<T, LinkedListNode<T>>();
    LinkedList<T> mList = new LinkedList<T>();

    public OrderedSet() : this(EqualityComparer<T>.Default)
    {
    }

    public OrderedSet(IEqualityComparer<T> comparer)
    {
      mMap = new Dictionary<T, LinkedListNode<T>>(comparer);
      mList = new LinkedList<T>();
    }

    public int Count
    {
      get { return mMap.Count; }
    }

    public bool Add(T item)
    {
      if (Contains(item))
        return false;

      var node = new LinkedListNode<T>(item);
      mList.AddLast(node);
      mMap.Add(item, node);
      return true;
    }

    public bool Remove(T item)
    {
      LinkedListNode<T> node = null;
      bool removed = mMap.Remove(item, out node);
      if (!removed)
        return false;

      mList.Remove(node);
      return true;
    }

    public void Clear()
    {
      mMap.Clear();
      mList.Clear();
    }

    public bool Contains(T item)
    {
      return mMap.ContainsKey(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
      return mList.GetEnumerator();
    }


    void ICollection<T>.Add(T item)
    {
      Add(item);
    }
    public virtual bool IsReadOnly
    {
      get { return mMap.IsReadOnly; }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      mList.CopyTo(array, arrayIndex);
    }
  }
}
