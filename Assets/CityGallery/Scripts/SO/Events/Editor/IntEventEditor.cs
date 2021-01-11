using UnityEditor;
using UnityEngine;

namespace RoboRyanTron.Unite2017.Events
{
    [CustomEditor(typeof(GameIntEvent))]
    public class IntEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameIntEvent e = target as GameIntEvent;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }
}
