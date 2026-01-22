using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInputHandler input;

    [Header("Menu Pages")]
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject SoloPlayer;
    [SerializeField] GameObject Settings;

    [Header("Bools")]
    private bool settingsOpen;
    private bool soloPlayerOpen;

    [Header("Else")]
    [SerializeField] private string DevScn1Name = "";
    [SerializeField] private string DevScn2Name = "";

    private void Start()
    {
        if (input == null) { input = GameObject.FindGameObjectWithTag("Input").GetComponent<PlayerInputHandler>(); }

        MainMenu.SetActive(true);
        settingsOpen = false;
        Settings.SetActive(false);
        soloPlayerOpen = false;
        SoloPlayer.SetActive(false);
    }

    private void Update()
    {
        if (input.PausePressed) 
            CloseMenus();
    }

    #region Global
    public void CloseMenus()
    {
        if (soloPlayerOpen)
        {
            soloPlayerOpen = false;
            SoloPlayer.SetActive(false);
            MainMenu.SetActive(true);
        }

        if (settingsOpen)
        {
            settingsOpen = false;
            Settings.SetActive(false);
            MainMenu.SetActive(true);
        }
    }

    public void OpenSinglePlayer()
    {
        soloPlayerOpen = true;
        SoloPlayer.SetActive(true);

        MainMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsOpen = true;
        Settings.SetActive(true);

        MainMenu.SetActive(false);
    }

    public void MainMenuQuit()
    {
        Application.Quit();
    }
    #endregion

    #region Singleplayer
    public void OpenDevScnPlayer()
    {
        SceneManager.LoadScene(DevScn1Name);
    }

    public void OpenDevScnVisual()
    {
        SceneManager.LoadScene(DevScn2Name);
    }
    #endregion
}
