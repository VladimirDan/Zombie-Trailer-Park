using UnityEngine;
using UnityEditor;
using UI.Buttons; // Убедитесь, что это пространство имен правильно указано

[CustomEditor(typeof(ButtonWithCooldown))]
public class ButtonWithCooldownEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Получаем ссылку на целевой объект
        ButtonWithCooldown button = (ButtonWithCooldown)target;

        // Отображение стандартного инспектора
        DrawDefaultInspector();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(button);
        }
    }
}