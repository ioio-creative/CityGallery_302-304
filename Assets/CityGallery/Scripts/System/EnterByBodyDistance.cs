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

    private bool eventRaised = false;
    public UnityEvent OnEnterThresholdReached;

    private void Update()
    {
        if (!eventRaised)
        {
            if (bodyDistances.FloatRefs.Length > 0)
            {
                minDist = Mathf.Min(bodyDistances.GetArray());
                if (minDist < enterDist)
                {
                    OnEnterThresholdReached?.Invoke();
                    eventRaised = true;
                }
            } 
        }

    }
}
