using UnityEngine;

namespace SOVariables
{
    [CreateAssetMenu(menuName = "SO Variables/Float Variable")]
    public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        [SerializeField]
        private float initValue;

        [SerializeField]
        private float runtimeValue;

        #region operators and casting
        public static implicit operator float(FloatVariable intV)
        {
            return intV.runtimeValue;
        }

        public static FloatVariable operator ++(FloatVariable intV)
        {
            intV.runtimeValue++;
            return intV;
        }

        public static FloatVariable operator --(FloatVariable intV)
        {
            intV.runtimeValue--;
            return intV;
        }

        public string ToString(string fmt = "")
        {
            return runtimeValue.ToString(fmt);
        }

        #endregion

        public void InitializeValue(float Value)
        {
            initValue = Value;
            Reset();
        }

        public void SetValue(float value)
        {
            runtimeValue = value;
        }

        public void SetValue(FloatVariable value)
        {
            SetValue((float)value);
        }

        public void ModifyValue(float amount)
        {
            runtimeValue += amount;
        }

        public void ModifyValue(FloatVariable amount)
        {
            ModifyValue((float)amount);
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