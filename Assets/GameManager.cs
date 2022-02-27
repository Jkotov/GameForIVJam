using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private List<Collider2D> doors = new List<Collider2D>();
    [HideInInspector] public string nextScene;
    [SerializeField] private Transform player;
    [SerializeField] private Transform columns;
    [SerializeField] private Transform liane;
    [SerializeField] private Transform bottomBg;
    [SerializeField] private Transform upBg;

    [SerializeField] private float playerFirstPos;
    [SerializeField] private float columnsFirstPos;
    [SerializeField] private float lianeFirstPos;
    [SerializeField] private float bottomBgFirstPos;
    [SerializeField] private float upBgFirstPos;
    [SerializeField] private float firstDuration;
    [SerializeField] private AudioClip firstAudio;
    
    [HideInInspector] public float playerYMove;
    [HideInInspector] public Transform openingDoor;
    [HideInInspector] public SpriteRenderer doorShadow;
    [SerializeField] float doorYMove;
    [SerializeField] private float yMoveDuration;

    [SerializeField] private float playerEndPos;
    [SerializeField] private float columnsEndPos;
    [SerializeField] private float lianeEndPos;
    [SerializeField] private float bottomBgEndPos;
    [SerializeField] private float upBgEndPos;
    [SerializeField] private float endDuration;
    [SerializeField] private AudioClip endAudio;
    private AudioSource _audioSource;
    
    [SerializeField] private SkeletonAnimation anim;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        foreach (var door in doors)
        {
            door.enabled = false;
        }

        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(MoveToDoor());
    }

    IEnumerator FirstVoice()
    {
        yield return new WaitForSeconds(4);
        _audioSource.PlayOneShot(firstAudio);
    }

    private IEnumerator MoveToDoor()
    {
        player.DOMoveX(playerFirstPos, firstDuration);
        columns.DOMoveX(columnsFirstPos, firstDuration);
        liane.DOMoveX(lianeFirstPos, firstDuration);
        bottomBg.DOMoveX(bottomBgFirstPos, firstDuration);
        upBg.DOMoveX(upBgFirstPos, firstDuration);
        anim.AnimationName = "animation";
        StartCoroutine(FirstVoice());
        yield return new WaitForSeconds(firstDuration);
        
        foreach (var door in doors)
        {
            door.enabled = true;
        }
        anim.AnimationName = "";
    }

    public void TurnOffDoorColliders()
    {
        foreach (var door in doors)
        {
            door.enabled = false;
        }
    }
    public void ResumeMove()
    {
        StartCoroutine(YMove());
    }
    public IEnumerator YMove()
    {
        player.DOMoveY(player.position.y + playerYMove, yMoveDuration);
        openingDoor.DOMoveY(openingDoor.position.y + doorYMove, yMoveDuration);
        anim.AnimationName = "animation";
        var shadowColor = doorShadow.color;
        shadowColor.a = 0;
        doorShadow.DOColor(shadowColor, yMoveDuration);
        yield return yMoveDuration;
        StartCoroutine(MoveOutDoor());
    }
    private IEnumerator MoveOutDoor()
    {
        player.DOMoveX(playerEndPos, endDuration);
        columns.DOMoveX(columnsEndPos, endDuration);
        liane.DOMoveX(lianeEndPos, endDuration);
        bottomBg.DOMoveX(bottomBgEndPos, endDuration);
        upBg.DOMoveX(upBgEndPos, endDuration);
        _audioSource.PlayOneShot(endAudio);
        yield return new WaitForSeconds(endDuration);
        SceneManager.LoadScene(nextScene);
    }
    
    
}
