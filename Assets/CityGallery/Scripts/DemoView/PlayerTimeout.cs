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

            if (closestDistance <= outOfRangeDistance)
            {
                timer = 0;
            }

            if (timer > leavingTimeout)
            {
                isIdle = true;
                AllPlayersLeft?.Invoke();
                Debug.Log("Player Left");
                timer = 0;
            }
        }
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
        isIdle = false;
        //StartCoroutine(CheckPlayerLeave());
    }
}
