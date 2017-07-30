using UnityEngine;

public class Player : MonoBehaviour {

    public AudioClip MoveClip;
    public AudioClip ActionClip;

    Map map;
    GameManager gm;
    AudioSource source;
    Vector2 input;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        map = Map.Instance;
        gm = GameManager.Instance;
        if (!map)
            Debug.LogError("Can't find the instance of the map script");

        map.SetPlayerPosition(transform.position);
    }

    private void Update()
    {
        if (!gm.Dead && !gm.Won)
        {
            if (Input.GetButtonDown("Up"))
            {
                map.Move(transform, Map.Direction.Up, true);
                PlayClip(MoveClip);
            }
            if (Input.GetButtonDown("Down"))
            {
                map.Move(transform, Map.Direction.Down, true);
                PlayClip(MoveClip);
            }
            if (Input.GetButtonDown("Left"))
            {
                map.Move(transform, Map.Direction.Left, true);
                PlayClip(MoveClip);
            }
            if (Input.GetButtonDown("Right"))
            {
                map.Move(transform, Map.Direction.Right, true);
                PlayClip(MoveClip);
            }

            if (Input.GetButtonDown("Hit"))
            {
                map.Hit(transform.position);
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
