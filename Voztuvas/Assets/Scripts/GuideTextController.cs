using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideTextController : MonoBehaviour
{
    public float displayDistance = 10f;

    [SerializeField] private GameObject _player;

    // Makes the text appear and disappear only once. If set to true, make sure the guide text is not visible at the start of the scene.
    public bool _disableOnHide = false;
    private bool _hidden = false;
    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerInfo.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Mathf.Abs(gameObject.transform.position.x - _player.transform.position.x);
        if (distanceToPlayer <= displayDistance)
        {
            if (_hidden)
            {
                _hidden = false;
                toggleChildrenVisibility(true);
            }
        }
        else
        {
            if (!_hidden)
            {
                _hidden = true;
                gameObject.SetActive(!_disableOnHide);
                toggleChildrenVisibility(false);
            }
        }
        
    }

    void toggleChildrenVisibility(bool shouldAppear)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(shouldAppear);
        }
    }

}
