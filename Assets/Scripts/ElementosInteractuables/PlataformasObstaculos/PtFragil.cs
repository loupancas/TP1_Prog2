using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtFragil : MonoBehaviour
{
    public float timeBetweenStates = 5f;
    public Collider platformCollider;
    public Material activeMaterial;
    public Material inactiveMaterial;
    public float shakeMagnitude = 0.1f;
    public float shakeDuration = 2f;
    public ParticleSystem dustShake;

    private bool isActive = true;

    //a línea yield return new WaitForSeconds(2f); indica cuánto tiempo la plataforma se mantendrá activa después de que el jugador colisione con ella.

    private void Start()
    {
        // Iniciar la corrutina que cambia el estado de la plataforma
        StartCoroutine(ChangePlatformState());
    }

    private IEnumerator ChangePlatformState()
    {
        while (true)
        {
            if (!isActive)
            {
                yield return new WaitForSeconds(timeBetweenStates);
            }

            // Cambiar el estado de la plataforma
            isActive = true;

            // Actualizar el material y el box collider de la plataforma
            platformCollider.enabled = true;
            GetComponent<Renderer>().material = activeMaterial;

            // Esperar a que el jugador salga de la plataforma
            while (isActive)
            {
                yield return null;
            }

            // Hacer que la plataforma tiemble
            StartCoroutine(ShakePlatform());

            // Desactivar la plataforma y cambiar su material
            yield return new WaitForSeconds(shakeDuration);
            platformCollider.enabled = false;
            GetComponent<Renderer>().material = inactiveMaterial;

            yield return new WaitForSeconds(timeBetweenStates - shakeDuration);

            // Cambiar el estado de la plataforma
            isActive = true;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isActive)
        {
            dustShake.Play();
            StartCoroutine(ShakePlatform());
            // La plataforma se mantendrá activa durante 2 segundos después de que el jugador colisione con ella
            StartCoroutine(DeactivatePlatform());
        }
    }

    private IEnumerator DeactivatePlatform()
    {
        yield return new WaitForSeconds(2f);
        isActive = false;
    }
}
