#pragma warning disable 0649

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerDeathGizmos : MonoBehaviour
{
    private const string sceneName = "MainScene";

    [SerializeField]
    private VideoClip videoClip;

    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private Button restartButton;

    private void Awake()
    {
        SetVideoClip();
        ToggleButton(false);
    }

    private void Start()
    {
        //For some reason i cannot add methods to the AddListener and i have to do it inline
        restartButton.onClick.AddListener(() =>
        {
            ScoreManager.Score = 0;
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        });
    }

    private void OnDestroy()
    {
        PauseGame(true);
        PlayClip();
        ToggleButton(true);
    }

    private void SetVideoClip() => videoPlayer.clip = videoClip;

    private void ToggleButton(bool state) => restartButton.gameObject.SetActive(state);

    private void PauseGame(bool state) => Time.timeScale = state ? 0 : 1;

    private void PlayClip() => videoPlayer.Play();
}