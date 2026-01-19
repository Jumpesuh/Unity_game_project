using UnityEngine;
using UnityEngine.InputSystem; // Lisää tämä rivi ylös!

public class AxeScript : MonoBehaviour
{
    public Transform WeaponHand;
    private bool playerIsClose = false;

    void Update()
    {
        // Uusi tapa tarkistaa näppäinpainallus (E-näppäin)
        if (playerIsClose && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickUp();
        }
    }

    void PickUp()
    {
        transform.SetParent(WeaponHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (GetComponent<Collider>()) 
        {
            GetComponent<Collider>().enabled = false;
        }
        Debug.Log("Poimittu uudella Input Systemillä!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerIsClose = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerIsClose = false;
    }
}