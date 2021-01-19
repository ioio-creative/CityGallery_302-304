using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof (Collider2D))]
public class HandCursorSelectable : MonoBehaviour
{
    public UnityEvent OnStay;

    private bool isStaying;

    [SerializeField]
    private float sleepInterval;

    private Coroutine StayingRoutine = null;

    private void Start()
    {
        var pos = transform.position;
        pos.z = HandCursorBase.CursorDepth;
    }

    private void FixedUpdate()
    {
        if (isStaying && StayingRoutine == null)
        {
            StayingRoutine = StartCoroutine(InvokeOnStayEventAtInterval());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isStaying = false;
        var cursor = collision.GetComponent<HandCursorBase>();
        if (cursor != null)
        {
            isStaying = cursor.IsHandCursorRaised();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isStaying = false;
    }

    private IEnumerator InvokeOnStayEventAtInterval()
    {
        while (isStaying)
        {
            OnStay?.Invoke();
            Debug.Log(name +": Selectable Invoke");
            yield return new WaitForSeconds(sleepInterval);
        }

        StayingRoutine = null;
    }
}
