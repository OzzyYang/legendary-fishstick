using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	[SerializeField] private List<TKey> keys;
	[SerializeField] private List<TValue> values;
	public void OnBeforeSerialize()
	{
		keys = new List<TKey>();
		values = new List<TValue>();
		foreach (var pair in this)
		{
			keys.Add(pair.Key);
			values.Add(pair.Value);
		}
	}
	public void OnAfterDeserialize()
	{
		Clear();
		if (keys.Count != values.Count)
		{
			Debug.Log("Keys count is not equal to values count.");
		}
		for (int i = 0; i < keys.Count; i++)
		{
			Add(keys[i], values[i]);
		}
	}

}
