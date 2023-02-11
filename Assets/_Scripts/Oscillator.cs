using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class Oscillator : MonoBehaviour {
    private const float TAU = Mathf.PI * 2;

    [SerializeField] private float pathLength;
    [Range( 1f, 5f ), SerializeField] private float pathTravelTime;

    private float movementSpeed;

    private void Awake() {
        transform.position = Vector3.zero;
        movementSpeed = 1f / pathTravelTime;
    }

    private float halfPathLength => pathLength * 0.5f;

    private void Update() {
        float horizontalPos = halfPathLength * Mathf.Cos( movementSpeed * TAU * Time.time );
        transform.position = new Vector3( horizontalPos, 0.0f, 0.0f );
    }

    private void OnDrawGizmos() {
#if UNITY_EDITOR
        Vector3 v1 = halfPathLength * Vector3.right;
        Vector3 v2 = halfPathLength * Vector3.left;

        Handles.DrawAAPolyLine( v1, v2 ); // Draw patrol path

        if ( EditorApplication.isPlaying ) {
            Handles.DrawWireDisc( default, Vector3.forward, halfPathLength, thickness: 2f );

            var point = new Vector3( halfPathLength * Mathf.Cos( movementSpeed * TAU * Time.time ), halfPathLength * Mathf.Sin( movementSpeed * TAU * Time.time ), 0f );

            Handles.DrawSolidDisc( point, Vector3.forward, 1f );
            Handles.DrawDottedLine( point, Vector3.Dot( Vector3.right, point ) * Vector3.right, 2f );
        }
#endif
    }
}