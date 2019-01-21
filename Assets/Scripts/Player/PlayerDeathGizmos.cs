#pragma warning disable 0649

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerDeathGizmos : MonoBehaviour
{
    [SerializeField]
    private VideoClip videoClip;

    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private Button restartButton;

    private void Awake()
    {
        SetVideoClip(videoPlayer, videoClip);
        ToggleButton(restartButton, false);
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
        PlayClip(videoPlayer);
        ToggleButton(restartButton, true);
    }

    private void SetVideoClip(VideoPlayer videoPlayer, VideoClip videoClip)
    {
        if (videoPlayer != null)
        {
            videoPlayer.clip = videoClip;
        }
    }

    private void ToggleButton(Button restartButton, bool state)
    {
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(state);
        }
    }

    private void PauseGame(bool state) => Time.timeScale = state ? 0 : 1;

    private void PlayClip(VideoPlayer videoPlayer)
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }
}