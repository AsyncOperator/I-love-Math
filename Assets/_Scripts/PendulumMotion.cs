using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class PendulumMotion : MonoBehaviour {
    [Range( 0f, 360f ), SerializeField] private float startingAngleInDegrees;
    [Range( 0f, 90f ), SerializeField] private float angleTravelInDegrees;
    [SerializeField] private float radius;

    private float offsetAngle;

    private void Update() {
        float startingAngleInRad = startingAngleInDegrees * Mathf.Deg2Rad;
        float angleTravelInRad = angleTravelInDegrees * Mathf.Deg2Rad;
        float angleInTime = ( Mathf.Sin( Mathf.PI * 2f * Time.time ) / 2f ) + 0.5f; // Remap value between 0-1
        offsetAngle = angleInTime * angleTravelInRad;

        float finalAngle = offsetAngle + startingAngleInRad;

        transform.position =
          new Vector3
          (
            radius * Mathf.Cos( finalAngle ),
            radius * Mathf.Sin( finalAngle ),
            0.0f
          );
    }

    private void OnDrawGizmos() {
#if UNITY_EDITOR
        Vector3 startingVector = new Vector3( Mathf.Cos( startingAngleInDegrees * Mathf.Deg2Rad ), Mathf.Sin( startingAngleInDegrees * Mathf.Deg2Rad ), 0f );
        Vector3 finalVector = new Vector3( Mathf.Cos( ( startingAngleInDegrees + angleTravelInDegrees ) * Mathf.Deg2Rad ), Mathf.Sin( ( startingAngleInDegrees + angleTravelInDegrees ) * Mathf.Deg2Rad ), 0f );

        Handles.DrawWireArc( default, Vector3.forward, startingVector, angleTravelInDegrees, radius );
        Handles.DrawAAPolyLine( Vector3.zero, startingVector * radius );
        Handles.DrawAAPolyLine( Vector3.zero, finalVector * radius );

        Handles.color = new Color( 1f, 1f, 1f, 0.3f );
        Handles.DrawSolidArc( default, Vector3.forward, startingVector, offsetAngle * Mathf.Rad2Deg, 1f );
        Handles.DrawAAPolyLine( Vector3.zero, transform.position );
#endif
    }
}