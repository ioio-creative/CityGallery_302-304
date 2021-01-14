using SOVariables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterByBodyDistance : MonoBehaviour
{
    [SerializeField]
    private FloatReferenceArray bodyDistances;
    [SerializeField]
    private float enterDist;
    private float minDist;

    private bool isIdle = true;
    public UnityEvent OnEnterThresholdFromIdle;

    
    private void Update()
    {
        if (Game303Manager.instance.CheckStatus(Status.Idle))
        {
            if (bodyDistances.FloatRefs.Length > 0)
            {
                minDist = Mathf.Min(bodyDistances.GetArray());
                if (minDist < enterDist)
                {
                    OnEnterThresholdFromIdle?.Invoke();
                    Game303Manager.instance.ChangeStatus(Status.Tutorial);
                    isIdle = false;
                }
            } 
        }

    }
}
