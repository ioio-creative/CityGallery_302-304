using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOVariables
{
    [CreateAssetMenu(menuName = "SO Variables/String Variable")]
    public class StringVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif

        [SerializeField]
        public string Value;

        #region operators and casting
        public static implicit operator string(StringVariable stringV)
        {
            return stringV.Value;
        }
        #endregion


    }
}
