using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private string startScene;

    private string currentLevel;

    public bool isPlayerDead { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentLevel = startScene;
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        ///    LoadLevel("Level3");
    }

    public void OnDeath(string reason)
    {
        isPlayerDead = true;
        StartCoroutine(DeathProcess(reason));
    }

    IEnumerator DeathProcess(string reason)
    {
        DeathScreen deathScreen = FindObjectOfType<DeathScreen>();
        deathScreen.SetText(reason);
        yield return new WaitForSecondsRealtime(4);
        LoadLevel(currentLevel);
    }

    public void LoadLevel(string name)
    {
        currentLevel = name;
        SceneManager.LoadScene(name);
    }
}
