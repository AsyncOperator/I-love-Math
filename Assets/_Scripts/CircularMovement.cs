using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class CircularMovement : MonoBehaviour {
    [SerializeField] private float radius;
    [SerializeField] private float angularSpeedInDegrees;

    [SerializeField] private TextMeshPro tmp;

    private float x, y;
    public float Angle {
        get
        {
            float signedAngle = Mathf.Atan2( y, x ) * Mathf.Rad2Deg;
            return Mathf.Sign( signedAngle ) > 0f ? signedAngle : 180f + ( signedAngle + 180f );
        }
    }

    private void Update() {
        x = radius * Mathf.Cos( Time.time * angularSpeedInDegrees * Mathf.Deg2Rad );
        y = radius * Mathf.Sin( Time.time * angularSpeedInDegrees * Mathf.Deg2Rad );

        transform.position = new Vector3( x, y, 0f );
        tmp.SetText( Mathf.CeilToInt( Angle ).ToString() );
    }

    private void OnDrawGizmos() {
#if UNITY_EDITOR
        Handles.DrawWireDisc( default, Vector3.forward, radius, thickness: 2f ); // Draw circle with radius
        Handles.DrawDottedLine( default, Vector3.right * radius, screenSpaceSize: 3f ); // Draw x axis with dotted line
        Handles.DrawAAPolyLine( width: 2f, Vector3.zero, new Vector3( x, y, 0f ) ); // Vector from center to sphere itself
        Handles.color = new Color( 1, 1, 1, 0.1f );
        Handles.DrawSolidArc( default, Vector3.forward, Vector3.right, Mathf.Abs( Angle ), radius * 0.2f );
#endif
    }
}