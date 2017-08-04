using UnityEngine;

public class Player : MonoBehaviour {

    public AudioClip MoveClip;
    public AudioClip ActionClip;

    Map map;
    GameManager gm;
    AudioSource source;
    Swipe swipeInput;
    Vector2 input;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        swipeInput = GetComponent<Swipe>();
        map = Map.Instance;
        gm = GameManager.Instance;
        if (!map)
            Debug.LogError("Can't find the instance of the map script");

        map.SetPlayerPosition(transform.position);
    }

    private void Update()
    {
        if (!gm.Dead && !gm.Won && !gm.Pause)
        {
            if (swipeInput.SwipeUp)
            {
                Action action = map.Move(transform, Map.Direction.Up);
                if (action == Action.Walk)
                    PlayClip(MoveClip);
                else if (action == Action.Hit)
                    PlayClip(ActionClip);
            }
            if (swipeInput.SwipeDown)
            {
                Action action = map.Move(transform, Map.Direction.Down);
                if (action == Action.Walk)
                    PlayClip(MoveClip);
                else if (action == Action.Hit)
                    PlayClip(ActionClip);
            }
            if (swipeInput.SwipeLeft)
            {
                Action action = map.Move(transform, Map.Direction.Left);
                if (action == Action.Walk)
                    PlayClip(MoveClip);
                else if (action == Action.Hit)
                    PlayClip(ActionClip);
            }
            if (swipeInput.SwipeRight)
            {
                Action action = map.Move(transform, Map.Direction.Right);
                if (action == Action.Walk)
                    PlayClip(MoveClip);
                else if (action == Action.Hit)
                    PlayClip(ActionClip);
            }
        }
    }

    private void PlayClip(AudioClip clip)
    {
        source.clip = clip;
        source.volume = gm.SFXVolume();
        source.Play();
    }
}
