
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "BasicStats/BasicEnemyValue", order = 1)]
public class EnemyBasicStatsSO : ScriptableObject
{
    public string name;
    public float speed,armour, health, damage,round_Type,Fire_Rate;
}
