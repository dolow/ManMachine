using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialScene : MonoBehaviour
{
    public GameObject title = null;
    public LinearIntensity linearLight = null;

    private float duration = 0.0f;
    private AudioClip clip = null;

    private int progress = 0;

    private void Start()
    {
        this.clip = Resources.Load<AudioClip>("NoRedistributionLicense/Audio/train-door2");
    }

    private void Update()
    {
        this.duration += Time.deltaTime;

        if (this.progress == 0 && this.duration >= 17.0f)
        {
            this.title.SetActive(true);
            this.progress = 1;
        }
        else if (this.progress == 1)
        {
            if (Input.GetMouseButtonDown(0) || Input.touches.Length > 0 || Input.anyKey)
            {
                AudioSource audio = this.GetComponent<AudioSource>();
                audio.PlayOneShot(this.clip);

                this.linearLight.duration = 3.0f;
                this.linearLight.from = 1.0f;
                this.linearLight.to = 0.0f;
                this.linearLight.Begin();

                this.duration = 0.0f;

                this.progress = 2;
            }
        }
        else if (this.progress == 2 && this.duration >= 3.0f)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene("TutorialScene");
        }
    }
}
