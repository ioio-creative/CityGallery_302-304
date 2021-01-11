using UnityEngine;

namespace SOVariables
{
    [CreateAssetMenu(menuName = "SO Variables/Int Variable")]
    public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif

        [SerializeField]
        private int initValue;

        [SerializeField]
        private int runtimeValue;

        #region operators and casting
        public static implicit operator int(IntVariable intV)
        {
            return intV.runtimeValue;
        }

        public static IntVariable operator ++(IntVariable intV)
        {
            intV.runtimeValue++;
            return intV;
        }

        public static IntVariable operator --(IntVariable intV)
        {
            intV.runtimeValue--;
            return intV;
        }

        public string ToString(string fmt = "")
        {
            return runtimeValue.ToString(fmt);
        }

        #endregion

        public void InitializeValue(int Value)
        {
            initValue = Value;
            Reset();
        }

        public void SetValue(int value)
        {
            runtimeValue = value;
        }

        public void SetValue(IntVariable value)
        {
            SetValue((int)value);
        }

        public void ModifyValue(int amount)
        {
            runtimeValue += amount;
        }

        public void ModifyValue(IntVariable amount)
        {
            ModifyValue((int)amount);
        }

        public void Reset()
        {
            runtimeValue = initValue;
        }

        #region ISerializationCallbackReceiver
        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            Reset();
        }      
        #endregion


    }

}