using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour {
	
	public GameObject[] pointList;

	private List<Vector3> m_pointPosList = new List<Vector3> ();
	private int m_pointNum;

	void Start () {
		Init ();
	}

	void Update() {
		DrawLine ();
	}

	public Vector3 CameraPositionFollowPath(Vector3 pos){
		int closestPointIndex = GetClosestPoint(pos);
		if(closestPointIndex == 0){
			return CameraOnSegment (m_pointPosList[0], m_pointPosList[1], pos);
		}else if(closestPointIndex == m_pointNum - 1){
			return CameraOnSegment (m_pointPosList[m_pointNum - 1], m_pointPosList[m_pointNum - 2], pos);
		}else{
			Vector3 leftSeg = CameraOnSegment (m_pointPosList[closestPointIndex - 1], m_pointPosList[closestPointIndex], pos);
			Vector3 rightSeg = CameraOnSegment (m_pointPosList[closestPointIndex + 1], m_pointPosList[closestPointIndex], pos);

			if ((pos - leftSeg).sqrMagnitude <= (pos - rightSeg).sqrMagnitude) {
				return leftSeg;
			} else {
				return rightSeg;
			}
		}
	}

	private void Init(){
		m_pointPosList.Clear ();
		m_pointNum = pointList.Length;
		for(int i = 0; i < m_pointNum; ++i){
			m_pointPosList.Add (pointList[i].transform.position);
		}
	}

	private void DrawLine () {
		if(m_pointNum > 1){
			for(int i = 0; i < m_pointPosList.Count - 1; ++i){
				Debug.DrawLine (m_pointPosList[i], m_pointPosList[i + 1], Color.blue);
			}
		}
	}

	private int GetClosestPoint(Vector3 pos){
		int closestPointIndex = 0;
		float shortDistance = 0f;

		for(int i = 0; i < m_pointNum; ++i){
			float sqrMagnitude = (m_pointPosList [i] - pos).sqrMagnitude;
			if(sqrMagnitude < shortDistance || closestPointIndex == 0) {
				shortDistance = sqrMagnitude;
				closestPointIndex = i;
			}
		}
		return closestPointIndex;
	}

	private Vector3 CameraOnSegment(Vector3 startPoint, Vector3 endPoint, Vector3 pos){
		Vector3 startToPos = pos - startPoint;
		Vector3 segDirection = (endPoint - startPoint).normalized;
		float distanceFormStart = Vector3.Dot (segDirection, startToPos);

		if (distanceFormStart < 0) {
			return startPoint;
		} else if (distanceFormStart * distanceFormStart > (endPoint - startPoint).sqrMagnitude) {
			return endPoint;
		} else {
			return startPoint + segDirection * distanceFormStart;
		}
	}

}
