using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SOVariables;

public class PlayerTimeout : MonoBehaviour
{
    public UnityEvent AllPlayersLeft;

    [SerializeField]
    private FloatReference closestDistance;

    [SerializeField]
    private FloatReference outOfRangeDistance;

    /// <summary>
    /// user leave timeout in seconds
    /// </summary>
    [SerializeField]
    [Tooltip("user leave timeout in seconds")]
    private FloatReference leavingTimeout;

    [SerializeField]
    private bool toBeTimeout;
    public bool ToBeTimeout => toBeTimeout;

    [Header("Debug")]
    [SerializeField] private float timer;

    private Coroutine timeoutRoutine;

    private void Start()
    {
        toBeTimeout = false;
    }

    private void Update()
    {
        if (toBeTimeout)
        {
            if (closestDistance <= outOfRangeDistance)
            {
                ResetIdleTimer();
                toBeTimeout = false;
            }

            timer += Time.deltaTime;

            if (timer >= leavingTimeout)
            {
                TimeoutLeave();
            }
        }
        else
        {
            toBeTimeout = closestDistance > outOfRangeDistance;
        }

        if (Input.anyKey || Input.touchCount > 0)
        {
            ResetIdleTimer();
        }
    }

    private void TimeoutLeave()
    {
        AllPlayersLeft?.Invoke();
        Debug.Log("Player Left");
        ResetIdleTimer();
    }

    private IEnumerator CheckPlayerLeave()
    {
        yield return new WaitUntil(() => closestDistance > outOfRangeDistance);
        
        if (timeoutRoutine == null)
        {
            timeoutRoutine = StartCoroutine(LeaveRoomAfterTimeout(leavingTimeout)); 
        }
        
        yield return new WaitUntil(() => closestDistance <= outOfRangeDistance);
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
        AllPlayersLeft?.Invoke();
        Debug.Log("Player Left");
        timeoutRoutine = null;
        StopAllCoroutines();
    }

    //private bool AnybodyWithinDistance()
    //{
    //    var dists = closestDistance.GetArray();

    //    foreach (var d in dists)
    //    {
    //        if (d <= outOfRangeDistance)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}
    
    public void OnPlayerEnter()
    {
        toBeTimeout = true;
        //StartCoroutine(CheckPlayerLeave());
    }

    public void ResetIdleTimer() 
    {
        timer = 0;
    }

    public void OnServerLeave()
    {
        toBeTimeout = false;
        ResetIdleTimer();
    }
}
