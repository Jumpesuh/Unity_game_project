using UnityEngine;
using UnityEngine.InputSystem; // Käytetään uutta Input Systemiä

public class WeaponAttack : MonoBehaviour
{
    public float swingSpeed = 10f;
    public float swingAngle = 45f;
    private bool isSwinging = false;
    
    private Quaternion startRotation;
    private float timer = 0f;

    void Start()
    {
        // Tallennetaan aseen normaali asento
        startRotation = transform.localRotation;
    }

    void Update()
    {
        // Jos ase on poimittu (sillä on isä) ja painetaan hiirtä
        if (transform.parent != null && Mouse.current.leftButton.wasPressedThisFrame && !isSwinging)
        {
            isSwinging = true;
            timer = 0f;
        }

        if (isSwinging)
        {
            DoSwing();
        }
    }

    void DoSwing()
{
    timer += Time.deltaTime * swingSpeed;
    float angle = Mathf.Sin(timer) * swingAngle;

    // VAIHTOEHTO A: Heilautus eteen/taakse (X-akseli)
    transform.localRotation = startRotation * Quaternion.Euler(angle, 0, 0);

    // VAIHTOEHTO B: Jos A ei toimi, kokeile tätä (Y-akseli)
    // transform.localRotation = startRotation * Quaternion.Euler(0, angle, 0);

    // VAIHTOEHTO C: Jos kirves nousee pystyyn, kokeile tätä (Z-akseli, miinusmerkillä)
    // transform.localRotation = startRotation * Quaternion.Euler(0, 0, -angle);

    if (timer > Mathf.PI) 
    {
        isSwinging = false;
        transform.localRotation = startRotation;
    }
}
}