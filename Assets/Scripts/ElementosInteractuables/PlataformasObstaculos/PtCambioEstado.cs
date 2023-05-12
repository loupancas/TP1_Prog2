using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtCambioEstado : MonoBehaviour
{
    public float timeBetweenStates = 5f;
    public Collider platformCollider;
    public Material activeMaterial;
    public Material inactiveMaterial;
    public float shakeMagnitude = 0.1f;
    public float shakeDuration = 2f;

    private bool isActive = true;

    public ParticleSystem dustShake;

    private void Start()
    {
        // Iniciar la corrutina que cambia el estado de la plataforma
        StartCoroutine(ChangePlatformState());
    }

    private IEnumerator ChangePlatformState()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStates);

            // Cambiar el estado de la plataforma
            isActive = !isActive;

            // Actualizar el material y el box collider de la plataforma
            if (isActive)
            {
                platformCollider.enabled = true;
                GetComponent<Renderer>().material = activeMaterial;
            }
            else
            {
                // Esperar 2 segundos antes de desactivar la plataforma y cambiar su material
                yield return new WaitForSeconds(2f);

                // Hacer que la plataforma tiemble
                StartCoroutine(ShakePlatform());

                // Desactivar la plataforma y cambiar su material
                yield return new WaitForSeconds(shakeDuration);
                platformCollider.enabled = false;
                GetComponent<Renderer>().material = inactiveMaterial;
            }
        }
    }

    private IEnumerator ShakePlatform()
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;

        while (elapsedTime < shakeDuration)
        {
            dustShake.Play();
            // Calcular la posición de la plataforma en cada fotograma
            Vector3 newPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            // Actualizar la posición de la plataforma
            transform.position = newPosition;

            // Esperar al siguiente fotograma
            yield return null;

            elapsedTime += Time.deltaTime;
        }

        // Volver la plataforma a su posición original
        transform.position = originalPosition;
        dustShake.Stop();
    }
}
