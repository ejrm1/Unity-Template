using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject _player;

    private void Update() {
        if (_player is null) return;

        Vector3 newPosition = _player.transform.position;
        newPosition.z = -10;
        transform.position = newPosition;
    }

    public void SetPlayer(GameObject player) {
        _player = player;
    }
}
