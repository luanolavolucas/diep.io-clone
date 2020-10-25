using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerController : ShipController
{

    void Update()
    {
        if(photonView.IsMine && PhotonNetwork.IsConnected)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                photonView.RPC("Fire", RpcTarget.All);
            }
            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            ship.Aim(target);
        }
    }

    void FixedUpdate()
    {
        if (photonView.IsMine && PhotonNetwork.IsConnected)
        {
            ship.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

}
