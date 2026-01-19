using UnityEngine;
using UnityEngine.InputSystem;

public class BeanMovement : MonoBehaviour
{
    [Header("Liikeasetukset")]
    public float perusNopeus = 5f;
    public float juoksuKerroin = 2f;
    public float hyppyVoima = 10f;
    public float painovoima = 20f;

    [Header("Hiiri & Kamera")]
    public float hiirenHerkkyys = 0.2f;
    public Transform cameraPivot; // Raahaa CameraPivot-olio tähän Inspectorissa!
    private float kameranPystyKierto = 0f;

    private Vector2 liikeInput;
    private Vector2 hiiriInput;
    private Vector3 pystyNopeus; // Pitää kirjaa hypystä ja painovoimasta
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputValue value) => liikeInput = value.Get<Vector2>();
    public void OnLook(InputValue value) => hiiriInput = value.Get<Vector2>();

    // TÄMÄ PALAUTETTU: Hyppytoiminto
    public void OnJump(InputValue value)
    {
        if (controller.isGrounded)
        {
            pystyNopeus.y = hyppyVoima;
        }
    }

    void Update()
    {
        // 1. KÄÄNTYMINEN (Orbit)
        transform.Rotate(Vector3.up * hiiriInput.x * hiirenHerkkyys);

        kameranPystyKierto -= hiiriInput.y * hiirenHerkkyys;
        kameranPystyKierto = Mathf.Clamp(kameranPystyKierto, -40f, 80f);
        cameraPivot.localRotation = Quaternion.Euler(kameranPystyKierto, 0f, 0f);

        // 2. LIIKE & JUOKSU
        bool kontrolliPohjassa = Keyboard.current.leftCtrlKey.isPressed;
        float nopeusNyt = kontrolliPohjassa ? perusNopeus * juoksuKerroin : perusNopeus;

        Vector3 suunta = transform.forward * liikeInput.y + transform.right * liikeInput.x;
        controller.Move(suunta * nopeusNyt * Time.deltaTime);

        // 3. PAINOVOIMA JA HYPPY (Pystysuuntainen liike)
        if (controller.isGrounded && pystyNopeus.y < 0)
        {
            pystyNopeus.y = -2f;
        }

        pystyNopeus.y -= painovoima * Time.deltaTime; // Vetää hahmoa alaspäin
        controller.Move(pystyNopeus * Time.deltaTime); // Suorittaa hypyn tai putoamisen
    }
}