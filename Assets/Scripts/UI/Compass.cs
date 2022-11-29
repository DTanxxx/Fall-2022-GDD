using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Compass : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private RectTransform missionLayer;
    [SerializeField] private Transform missionPlace;
    [SerializeField] private Transform destination;

    private void Update()
    {
        ChangeMissionDirection();
    }

    private void ChangeMissionDirection()
    {
        if (player == null)
        {
            return;
        }
        if (missionPlace == null)
        {
            // Point to door
            missionPlace = destination;
        }

        Vector3 dir = missionPlace.position - player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        Quaternion missionDirection = Quaternion.AngleAxis(angle, Vector3.forward);
        missionLayer.localRotation = missionDirection;
    }
}
