using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingnessScenario : MonoBehaviour
{
    public GameObject negationSymbol;
    public GameObject negationTip;

    private void Start()
    {
        StartCoroutine(Scenario());
    }

    IEnumerator Scenario()
    {
        yield return new WaitForSecondsRealtime(12f);
        negationSymbol.SetActive(true);
        yield return new WaitForSecondsRealtime(10f);
        negationTip.SetActive(true);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                break;
            }
            yield return null;
        }
        GameManager.Instance.LoadLevel("Level1");
    }
}
