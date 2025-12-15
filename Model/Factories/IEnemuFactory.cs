using ISIP523_Korzh.Model;

namespace ISIP523_Korzh.Model.Factories
{
    public interface IEnemyFactory
    {
        Enemy CreateRandomEnemy();
        Enemy CreateBoss(int turnCount);
    }
}