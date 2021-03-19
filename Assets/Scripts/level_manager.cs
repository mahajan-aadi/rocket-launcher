using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_manager : MonoBehaviour
{
    int current_level()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public void next_level()
    {
        int current = current_level() + 1;
        StartCoroutine(waiting(current));
    }
    public void lost()
    {
        int current = current_level();
        StartCoroutine(waiting(current));
    }
    private void Awake()
    {
     /*   level_manager[] list = FindObjectsOfType<level_manager>();
        if (list.Length > 1) { Destroy(this.gameObject); }
        else { DontDestroyOnLoad(this.gameObject); }*/
    }
    private void Start()
    {
        if (current_level() == 0)
        {
            int current = current_level();
            StartCoroutine(waiting(1));
        }
    }
    IEnumerator waiting(int current_index)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(current_index);
    }
}
