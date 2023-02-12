using UnityEngine;

public sealed class SpringMotion : MonoBehaviour {
    [SerializeField] private float radius;

    private float powFactor = 0;

    private void Update() {
        powFactor += Time.deltaTime;

        float distance = Mathf.Pow( 0.5f, powFactor );

        if ( Input.GetKeyDown(KeyCode.Space) ) {
            powFactor = 0;
        }

        float posX = radius * Mathf.Cos( Mathf.PI * 2f * Time.time );


        transform.position = new Vector3( distance * posX, 0f, 0f );
    }
}