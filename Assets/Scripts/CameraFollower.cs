using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    public Transform follow; //object to follow
    public Vector3 offset;
    public bool useBounds;

    public Vector3 minBounds;
    public Vector3 maxBounds;

    public float smoothing; //0 is no movement, 1 is instant movement;

	// Use this for initialization
	void Start ()
    {
		
	}

    void LateUpdate()
    {
        if (follow != null)
        {
            Vector3 followPos = follow.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, followPos, smoothing);

            if (!useBounds)
            {
                transform.position = smoothedPos;
            }
            else if (smoothedPos.y > maxBounds.y || smoothedPos.y < minBounds.y)
            {
                transform.position = new Vector3(smoothedPos.x, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = smoothedPos;
            }
        }
    }
}
