using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SOVariables;

public class PlayerTimeout : MonoBehaviour
{
    public UnityEvent AllPlayerLeft;

    [SerializeField]
    private FloatReferenceArray bodyDistances;

    [SerializeField]
    private float outOfRangeDistance;

    /// <summary>
    /// user leave timeout in seconds
    /// </summary>
    [SerializeField]
    [Tooltip("user leave timeout in seconds")]
    private float leavingTimeout;

    [SerializeField]
    private bool isIdle = true;
    public bool IsIdle => isIdle;
    private float timer;

    private Coroutine timeoutRoutine;

    private void Start()
    {
        isIdle = true;
    }

    private void Update()
    {
        if (!isIdle)
        {
            timer += Time.deltaTime;

            if (AnybodyWithinDistance())
            {
                timer = 0;
            }

            if (timer > leavingTimeout)
            {
                isIdle = true;
                AllPlayerLeft?.Invoke();
                Debug.Log("Player Left");
                timer = 0;
            }
        }
    }


    private IEnumerator CheckPlayerLeave()
    {
        yield return new WaitUntil(() => !AnybodyWithinDistance());
        
        if (timeoutRoutine == null)
        {
            timeoutRoutine = StartCoroutine(LeaveRoomAfterTimeout(leavingTimeout)); 
        }
        
        yield return new WaitUntil(() => AnybodyWithinDistance());
        if (timeoutRoutine != null)
        {
            StopCoroutine(timeoutRoutine);
            timeoutRoutine = null;
        }
        StartCoroutine(CheckPlayerLeave());
    }

    private IEnumerator LeaveRoomAfterTimeout(float timeout)
    {
        yield return new WaitForSeconds(timeout);
        AllPlayerLeft?.Invoke();
        Debug.Log("Player Left");
        timeoutRoutine = null;
        StopAllCoroutines();
    }

    private bool AnybodyWithinDistance()
    {
        var dists = bodyDistances.GetArray();

        foreach (var d in dists)
        {
            if (d <= outOfRangeDistance )
            {
                return true;
            }
        }

        return false;
    }
    
    public void OnPlayerEnter()
    {
        isIdle = false;
        //StartCoroutine(CheckPlayerLeave());
    }
}
