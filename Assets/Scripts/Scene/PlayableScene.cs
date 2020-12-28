using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayableScene : MonoBehaviour
{
    public const int maxLevel = 1;

    private const string audioGoalCacheName = "goal";
    private const string audioUICacheName = "ui";

    public int level = 0;

    public Canvas uiCanvas = null;
    public Player player = null;
    public Camera TpsCamera = null;
    public Camera OverlookCamera = null;

    private bool gameFinished = false;

    void Awake()
    {
        AudioListener audio = this.GetComponent<AudioListener>();
        
        if (this.TpsCamera == null || this.OverlookCamera == null)
        {
            Debug.LogError("both tps camera and overlook camera are required to be set");
            return;
        }

        InteractionMediator mediator = this.gameObject.GetComponent<InteractionMediator>();
        if (mediator == null)
        {
            Debug.LogError("InteractionMediator is required");
            return;
        }

        mediator.RequestMove += this.MovePlayer;
        mediator.RequestStop += this.StopPlayer;
        mediator.RequestSwitchMainCamera += this.SwitchMainCamera;
        mediator.RequestSwitchShoulderCameraSide += this.SwitchShoulderCameraSide;
        mediator.RequestRestartLevel += this.RestartLevel;
        mediator.RequestAction += this.Action;

        this.TpsCamera.enabled = true;
        this.OverlookCamera.enabled = false;

        Achievee achievee = this.GetComponent<Achievee>();
        if (achievee != null)
        {
            achievee.Achieve += this.NextLevel;
        }
    }

    private void Start()
    {
        {
            AudioClip clip = Resources.Load<AudioClip>("NoRedistributionLicense/Audio/morsecode-sos1");
            AudioCache.GetInstance().AddCache(audioGoalCacheName, clip);
        }
        {
            AudioClip clip = Resources.Load<AudioClip>("NoRedistributionLicense/Audio/phone-receiver-hangup1");
            AudioCache.GetInstance().AddCache(audioUICacheName, clip);
        }
    }

    private float duration = 0.0f;
    public GameObject tmpAnnounce = null;
    private void Update()
    {
        if (this.gameFinished)
        {
            if (this.duration >= 5.0f)
            {
                if (this.level < maxLevel)
                {
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene("Level" + (this.level + 1));
                }
                else
                {
                    this.tmpAnnounce.SetActive(true);
                }
            }
            this.duration += Time.deltaTime;
        }
    }

    #region component delegates

    private void NextLevel(Achievable achievable, Achiever achiever)
    {
        if (this.gameFinished)
        {
            return;
        }

        Agent agent = achievable.GetComponent<Agent>();
        if (agent != null)
        {
            agent.Stop();

            OverlookCamera camera = this.OverlookCamera.GetComponent<OverlookCamera>();
            if (camera != null)
            {
                this.TpsCamera.enabled = false;
                this.OverlookCamera.enabled = true;

                camera.ShowAchievedAgent(agent);
            }
        }

        this.gameFinished = true;

        this.uiCanvas.worldCamera = null;

        // TODO: UserFeedbackable
        AudioCache.GetInstance().OneShot(audioGoalCacheName);
    }

    #endregion

    #region request from interaction

    private void SwitchMainCamera()
    {
        if (this.gameFinished)
        {
            return;
        }

        this.TpsCamera.enabled = !this.TpsCamera.enabled;
        this.OverlookCamera.enabled = !this.OverlookCamera.enabled;

        if (this.uiCanvas != null)
        {
            if (this.TpsCamera.enabled)
            {
                this.uiCanvas.worldCamera = this.TpsCamera;
            }
            else if (this.OverlookCamera.enabled)
            {
                this.uiCanvas.worldCamera = this.OverlookCamera;
            }
        }

        // TODO: UserFeedbackable
        AudioCache.GetInstance().OneShot(audioUICacheName);
    }

    private void SwitchShoulderCameraSide()
    {
        if (this.gameFinished)
        {
            return;
        }

        Follow follow = this.TpsCamera.GetComponent<Follow>();
        if (follow != null)
        {
            follow.SwitchOffsetX();
        }
        // TODO: UserFeedbackable
        AudioCache.GetInstance().OneShot(audioUICacheName);
    }

    private void RestartLevel()
    {
        if (this.gameFinished)
        {
            return;
        }

        // TODO: UserFeedbackable
        AudioCache.GetInstance().OneShot(audioUICacheName);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void Action()
    {
        this.player.TryAction();
    }

    protected void MovePlayer(float front, float right, float rotate)
    {
        if (this.gameFinished)
        {
            return;
        }

        if (this.player != null)
        {
            this.player.Walk(front, right, rotate);
        }
    }

    protected void StopPlayer()
    {
        if (this.player != null)
        {
            this.player.Stop();
        }
    }

    #endregion
}
