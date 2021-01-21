using RoboRyanTron.Unite2017.Events;
using SOVariables;
using UnityEngine;

public class EnterByBodyDistance302 : MonoBehaviour
{
    [SerializeField]
    private FloatReference closestDistance;
    [SerializeField]
    private float enterDist;

    [SerializeField]
    private Vector2 idleDistMappingRange;

    //internal event
    [SerializeField]
    private GameEvent enterRangeEvnt;

    private bool isIdle;


    private void Start()
    {
        isIdle = true;
    }

    private void Update()
    {
        if (Game302Manager.instance.CheckStatus(Status.Idle))
        {
            if (closestDistance < enterDist)
            {
                OnEnterThresholdFromIdle();                  
            }
            else
            {
                float dist01 = Mathf.InverseLerp(idleDistMappingRange.x, idleDistMappingRange.y, closestDistance);
                Game302Mediator.instance.SetColorCoverAlpha(Mathf.Max(0.1f, dist01));
            }         
        }

    }

    public void OnEnterThresholdFromIdle()
    {
        isIdle = false;
        enterRangeEvnt?.Raise();
    }
}
