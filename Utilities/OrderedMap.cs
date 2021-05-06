using System.Collections;
using System.Collections.Generic;

namespace Utilities
{
  public class OrderedMap<Key, Value> : ICollection<(Key key, Value value)>
  {
    IDictionary<Key, LinkedListNode<(Key key, Value value)>> mMap = new Dictionary<Key, LinkedListNode<(Key key, Value value)>>();
    LinkedList<(Key key, Value value)> mList = new LinkedList<(Key key, Value value)>();

    public OrderedMap() : this(EqualityComparer<Key>.Default)
    {
    }

    public OrderedMap(IEqualityComparer<Key> comparer)
    {
      mMap = new Dictionary<Key, LinkedListNode<(Key key, Value value)>>(comparer);
      mList = new LinkedList<(Key key, Value value)>();
    }

    public int Count
    {
      get { return mMap.Count; }
    }

    public bool Add(Key key, Value value)
    {
      if (ContainsKey(key))
        return false;

      var node = new LinkedListNode<(Key key, Value value)>((key, value));
      mList.AddLast(node);
      mMap.Add(key, node);
      return true;
    }

    public bool Remove(Key key)
    {
      LinkedListNode<(Key key, Value value)> node = null;
      bool removed = mMap.Remove(key, out node);
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

    public bool ContainsKey(Key key)
    {
      return mMap.ContainsKey(key);
    }

    public Value GetValueOrDefault(Key key, Value defaultValue = default)
    {
      LinkedListNode<(Key key, Value value)> outNode = null;
      if (!mMap.TryGetValue(key, out outNode))
        return defaultValue;
      return outNode.Value.value;
    }

    public Value this[Key key]
    {
      get
      {
        if(!ContainsKey(key))
        {
          Add(key, default);
        }
        return GetValueOrDefault(key);
      }
      set
      {
        LinkedListNode<(Key key, Value value)> outNode = null;
        if (!mMap.TryGetValue(key, out outNode))
          Add(key, value);
        else
          outNode.Value = (key, value);
      }
    }

    public IEnumerator<(Key key, Value value)> GetEnumerator()
    {
      return mList.GetEnumerator();
    }


    void ICollection<(Key key, Value value)>.Add((Key key, Value value) item)
    {
      Add(item.key, item.value);
    }
    bool ICollection<(Key key, Value value)>.Contains((Key key, Value value) item)
    {
      return ContainsKey(item.key);
    }
    bool ICollection<(Key key, Value value)>.Remove((Key key, Value value) item)
    {
      return Remove(item.key);
    }
    public virtual bool IsReadOnly
    {
      get { return mMap.IsReadOnly; }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    public void CopyTo((Key key, Value value)[] array, int arrayIndex)
    {
      mList.CopyTo(array, arrayIndex);
    }
  }
}
