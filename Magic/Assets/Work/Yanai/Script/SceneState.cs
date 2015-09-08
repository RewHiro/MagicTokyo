
using UnityEngine;
using Leap;

enum TitleState
{
    Set,
    Start,
}

public class SceneState : MonoBehaviour
{

    TitleState state_;
    Controller controller = new Controller();

    HandController hand_controller_ = null;

    [SerializeField]
    int VALID_MAX = 60;
    public int ValidMax
    {
        get { return VALID_MAX; }
    }

    [SerializeField]
    int valid_count_ = 0;
    public int ValidCount
    {
        get { return valid_count_; }
        private set { valid_count_ = value; }
    }

    bool is_tutroial_ = false;

    void Awake()
    {
        state_ = TitleState.Set;
    }

    void Start()
    {
        hand_controller_ = FindObjectOfType<HandController>();
    }

    void Update()
    {
        TitleUpdate();
        TutorialUpdate();
    }

    bool canShiftStart()
    {
        if (!isRecognizedHand())
        {
            ValidCount = 0;
            return false;
        }

        if (ValidCount < ValidMax)
        {
            ValidCount++;
            return false;
        }
        else
        {
            return true;
        }
    }

    bool isRecognizedHand()
    {
        var hand = FindObjectOfType<MyRigidHand>();
        
        if (hand == null) return false;

        return true;
    }

    public bool isStart()
    {
        return state_ == TitleState.Start;
    }

    void TitleUpdate()
    {
        if (is_tutroial_) return;
        // ゲーム本編に移行
        if (canShiftStart())
        {
            foreach (var player in FindObjectsOfType<LobbyPlayer>())
            {
                if (!player.isLocalPlayer) continue;
                player.ChangeReady();
            }
        }

        foreach (var hand in hand_controller_.GetFrame().Hands)
        {
            foreach (var gesture in hand.Frame.Gestures())
            {
                if (!hand.IsRight) continue;
                var swipe_gesture = new SwipeGesture(gesture);
                if (swipe_gesture.IsValid)
                {
                    is_tutroial_ = true;
                    FindObjectOfType<SlideDirector>().StartSlide();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            is_tutroial_ = true;
            FindObjectOfType<SlideDirector>().StartSlide();
        }
    }

    void TutorialUpdate()
    {
        if (!is_tutroial_) return;
        foreach (var hand in hand_controller_.GetFrame().Hands)
        {
            foreach (var gesture in hand.Frame.Gestures())
            {
                if (!hand.IsLeft) continue;
                var swipe_gesture = new SwipeGesture(gesture);
                if (swipe_gesture.IsValid)
                {
                    is_tutroial_ = false;

                    FindObjectOfType<SlideDirector>().FinishSlide();
                    GameObject.Destroy(GameObject.Find("TutorialRoot(Clone)"));
                    GameObject.Destroy(GameObject.Find("UI_Prefab(Clone)"));
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            is_tutroial_ = false;

            FindObjectOfType<SlideDirector>().FinishSlide();
            GameObject.Destroy(GameObject.Find("TutorialRoot(Clone)"));
            GameObject.Destroy(GameObject.Find("UI_Prefab(Clone)"));
        }
    }
}