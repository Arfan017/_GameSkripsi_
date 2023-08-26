using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadDate(GameData data);
    void SaveData(ref GameData data);
}
