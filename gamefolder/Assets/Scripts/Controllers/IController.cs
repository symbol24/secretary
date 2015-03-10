using UnityEngine;
using System.Collections;

public interface IController : IGameObject
{
    bool GetLeftWingDown();
    bool GetRightWingDown();
    bool Screech();
    bool GrabAction();
    bool DoGust();
}
