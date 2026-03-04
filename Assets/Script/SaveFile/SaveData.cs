using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData 
{
    public string LevelName { get; set; }
    public int CountMoney { get; set; }
    public int CountLife { get; set; }

    public HashSet<int> СollectedBonus { get; set; }
}
