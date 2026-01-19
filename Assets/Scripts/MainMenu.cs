using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // Varmistetaan, että hiiri näkyy valikossa
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void AloitaPeli()
    {
        // VAIHDA TÄHÄN PELISKENESI TARKKA NIMI!
        // Esimerkiksi: SceneManager.LoadScene("PeliSkenenNimi");
        // Jos peliskenesi nimi on SampleScene, se näyttää tältä:
        SceneManager.LoadScene("SampleScene"); 
    }

    public void LopetaPeli()
    {
        Debug.Log("Peli suljetaan...");
        Application.Quit();
    }
}