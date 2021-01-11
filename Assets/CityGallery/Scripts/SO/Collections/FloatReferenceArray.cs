using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SOVariables
{
	[CreateAssetMenu(menuName = "SO Variables/Float Reference Array")]
	public class FloatReferenceArray : ScriptableObject
	{
#if UNITY_EDITOR
		[Multiline]
		public string DeveloperDescription = "";
#endif
		[SerializeField]
		private FloatReference[] floatRefs;
		public FloatReference[] FloatRefs => floatRefs;

		public int Length => floatRefs.Length;

		public void SetArray(float[] array)
		{
			floatRefs = new FloatReference[array.Length];

			for (int i = 0; i < floatRefs.Length; i++)
			{
				floatRefs[i] = new FloatReference(array[i]);
			}
		}

		public float[] GetArray()
		{
			return FloatRefs.Select(x => (float)x).ToArray();
		}

		public void Clear()
		{
			floatRefs = new FloatReference[0];
		}
	}
}
