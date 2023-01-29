using UnityEngine;
using TMPro;

public sealed class SineWave : MonoBehaviour {
    private const float TAU = Mathf.PI * 2;

    [SerializeField] private Wave waveType;

    [SerializeField] private float radius;
    [SerializeField] private LineRenderer xAxis;
    [SerializeField] private LineRenderer yAxis;

    private void OnValidate() {
        yAxis.SetPosition( 0, Vector3.down * radius );
        yAxis.SetPosition( 1, Vector3.up * radius );

        xAxis.SetPosition( 0, Vector3.left * radius );
        xAxis.SetPosition( 1, Vector3.right * radius );

        if ( xAxis?.transform.childCount == 0 )
            return;

        float xOffset = radius / xAxis.transform.childCount;

        for ( int n = 0 ; n < xAxis.transform.childCount ; n++ ) {
            xAxis.transform.GetChild( n ).transform.position = xAxis.transform.GetChild( n ).transform.position.X( xOffset * ( n + 1 ) );
        }

        if ( yAxis?.transform.childCount == 0 )
            return;

        float yOffset = radius * 2 / yAxis.transform.childCount;

        for ( int n = 0 ; n < yAxis.transform.childCount / 2 ; n++ ) {
            yAxis.transform.GetChild( n ).transform.position = yAxis.transform.GetChild( n ).transform.position.Y( radius - yOffset * n );
            if ( yAxis.transform.GetChild( n ).TryGetComponent<TextMeshPro>( out TextMeshPro tmp ) ) {
                tmp.SetText( ( radius / ( n + 1 ) ).ToString() );
            }
        }
        for ( int n = yAxis.transform.childCount / 2 ; n < yAxis.transform.childCount ; n++ ) {
            yAxis.transform.GetChild( n ).transform.position = yAxis.transform.GetChild( n ).transform.position.Y( radius - yOffset * ( n + 1 ) );
            if ( yAxis.transform.GetChild( n ).TryGetComponent<TextMeshPro>( out TextMeshPro tmp ) ) {
                tmp.SetText( ( radius - ( n + 1 ) * yOffset ).ToString() );
            }
        }
    }

    private void Update() {
        float time = Time.time;
        float y = waveType switch
        {
            Wave.Sine => radius * Mathf.Sin( time * TAU ),
            Wave.Cos => radius * Mathf.Cos( time * TAU )
        };

        transform.position = new Vector3( radius * time, y, 0f );
    }
}

public static class ExtensionMethods {
    public static Vector3 X( this Vector3 v, float x ) {
        return new Vector3( x, v.y, v.z );
    }

    public static Vector3 Y( this Vector3 v, float y ) {
        return new Vector3( v.x, y, v.z );
    }
}

public enum Wave {
    Sine,
    Cos
}