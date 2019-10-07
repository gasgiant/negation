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
        if (Input.GetKeyDown(KeyCode.R))
            LoadLevel(currentLevel);

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
                LoadLevel("Nothingness");
            if (Input.GetKeyDown(KeyCode.Alpha1))
                LoadLevel("Level1");
            if (Input.GetKeyDown(KeyCode.Alpha2))
                LoadLevel("Level2");
            if (Input.GetKeyDown(KeyCode.Alpha3))
                LoadLevel("Level3");
            if (Input.GetKeyDown(KeyCode.Alpha4))
                LoadLevel("Level4");
            if (Input.GetKeyDown(KeyCode.Alpha5))
                LoadLevel("Level5");
            if (Input.GetKeyDown(KeyCode.Alpha6))
                LoadLevel("Level6");

            if (Input.GetKeyDown(KeyCode.Alpha9))
                LoadLevel("LevelFinal");
        }
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
