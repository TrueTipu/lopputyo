using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IA_Bool
{
}

public interface IA_OnGround : IA_Bool
{
    Action<bool> SetOnGround { get; set; }
}
public interface IA_OnAir : IA_Bool
{
    Action<bool> SetOnAir { get; set; }
}
public interface IA_JumpVariables : IA_Bool
{
    Action<JumpVariables> SetJumpVariables { get; set; }
}
public interface IA_IsDashing : IA_Bool
{
    Action<bool> SetIsDashing { get; set; }
}

public interface IA_FacingRight : IA_Bool
{
    Action<bool> SetFacingRight { get; set; }
}

public interface IA_IsWallJumping : IA_Bool
{
    Action<bool> SetIsWallJumping { get; set; }
}

public interface IA_OnWall : IA_Bool
{
    Action<bool> SetOnWall { get; set; }
}
public interface IA_OnCeiling : IA_Bool
{
    Action<bool> SetOnCeiling { get; set; }
}

public interface IA_IsSupering : IA_Bool
{
    Action<bool> SetIsSupering { get; set; }
}

public interface IA_IsCharging : IA_Bool
{
    Action<bool> SetIsCharging { get; set; }
}

[System.Serializable]
public struct JumpVariables
{
    public JumpVariables(bool _isJumping, bool _fallAddApplied, bool _fallSlowApplied, bool _jumpCanceled) : this()
    {
        IsJumping = _isJumping;
        FallAddApplied = _fallAddApplied;
        FallSlowApplied = _fallSlowApplied;
        JumpCanceled = _jumpCanceled;
    }
    public JumpVariables SetIsJumping(bool _value)
    {
        return new JumpVariables(_value, FallAddApplied, FallSlowApplied, JumpCanceled);
    }
    public JumpVariables SetJumpCanceled(bool _value)
    {
        return new JumpVariables(IsJumping, FallAddApplied, FallSlowApplied, _value);
    }
    public JumpVariables SetFallAddApplied(bool _value)
    {
        return new JumpVariables(IsJumping, _value, FallSlowApplied, JumpCanceled);
    }
    public JumpVariables SetFallSlowApplied(bool _value)
    {
        return new JumpVariables(IsJumping, FallAddApplied, _value, JumpCanceled);
    }

    public bool IsJumping { get; private set; }
    public bool FallAddApplied { get; private set; }
    public bool FallSlowApplied { get; private set; }
    public bool JumpCanceled { get; private set; }


}



