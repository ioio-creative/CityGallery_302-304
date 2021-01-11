using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandJointManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _CursorPrefab;
    //private HandCursor _Cursor;


    private void OnEnable()
    {
        if (_CursorPrefab != null)
        {
            GameObject newCursor = Instantiate(_CursorPrefab);
            newCursor.name = "HandCursor";
            //newCursor.GetComponent<HandCursor>().JointTransform = transform;
            //_Cursor = newCursor.GetComponent<HandCursor>();
            //_Cursor.transform.position = transform.position;
            //newCursor.GetComponent<HandCursor>().enabled = true;
        }
    }

    private void OnDisable()
    {
        //if (_Cursor != null)
        //{
        //    _Cursor.gameObject.SetActive(false);
        //    Destroy(_Cursor.gameObject); 
        //}
    }

}
