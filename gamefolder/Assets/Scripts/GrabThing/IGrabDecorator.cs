using UnityEngine;
using System.Collections;

public interface IGrabDecorator : IGameObject
{
    bool HasObjectGrabbed { get; }
    void DetachObject();
    bool AttemptToGrab();
}
