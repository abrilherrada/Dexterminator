using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float damageTaken;
    public float damageDone;
    public int pointsForKill;
}
