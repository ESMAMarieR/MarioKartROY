using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // La voiture à suivre
    public Vector3 offset = new Vector3(0, 10, 0); // Position relative de la caméra
    public float smoothSpeed = 5f; // Vitesse de suivi

    void LateUpdate()
    {
        if (target != null)
        {
            // Position cible (au-dessus du kart)
            Vector3 desiredPosition = target.position + offset;
            // Lissage du mouvement pour une caméra fluide
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
