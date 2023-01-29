using UnityEngine;

public sealed class LittleCircles : MonoBehaviour {
    private const float TAU = Mathf.PI * 2f;

    [SerializeField] private Transform[] targets;
    [SerializeField] private float radius;
    [SerializeField] private float angularSpeedInTurns;

    private float angleOffsetInRadians;

    private void Start() {
        angleOffsetInRadians = TAU / targets.Length;
    }

    private void Update() {
        for ( int i = 0 ; i < targets.Length ; i++ ) {
            float offset = angleOffsetInRadians * i;
            targets[ i ].position = new Vector3( radius * Mathf.Cos( offset + Time.time * angularSpeedInTurns * TAU ), radius * Mathf.Sin( offset + Time.time * angularSpeedInTurns * TAU ), 0f );
        }
    }

    private void OnDrawGizmos() {
#if UNITY_EDITOR
        UnityEditor.Handles.DrawWireDisc( default, Vector3.back, radius, thickness: 2f );
#endif
    }
}