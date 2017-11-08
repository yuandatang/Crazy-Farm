using UnityEngine;
using System.Collections;

public class Potato : Tower
{
    Transform m_ShotPoint;

    protected override void Awake()
    {
        base.Awake();
        m_ShotPoint = transform.Find("ShotPoint");
    }

    public override void Shot(Monster monster)
    {
        base.Shot(monster);

        GameObject go = Game.Instance.ObjectPool.Spawn("SlowdownBullet");
        SlowdownBullet bullet = go.GetComponent<SlowdownBullet>();
        bullet.transform.position = transform.position;
        bullet.Load(this.UseBulletID, this.Level, this.MapRect, monster);
    }
}