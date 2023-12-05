using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IPlayerStateChanger
{

}
public interface IAbility_Main : IPlayerStateChanger
{
    void Init(PlayerStateCheck _playerState, Rigidbody2D _rb2);
}

