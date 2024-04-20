using System.Collections;
using System.Collections.Generic;
using Main.Common;
using UnityEngine;

namespace Main.Model
{
    /// <summary>
    /// 攻撃を与える判定のトリガー
    /// </summary>
    public class AttackColliderOfOnmyoBullet : DamageSufferedZoneModel, IAttackColliderOfOnmyoBullet
    {
        /// <summary>円形コライダー2D</summary>
        [SerializeField] private CircleCollider2D circleCollider2D;

        private void Reset()
        {
            tags = new string[1];
            tags[0] = ConstTagNames.TAG_NAME_ENEMY;
            circleCollider2D = GetComponent<CircleCollider2D>();
        }

        protected override void Start() { }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }

        protected override void OnDisable()
        {
            IsHit.Value = false;
        }

        public bool SetRadiosOfCircleCollier2D(float radios)
        {
            try
            {
                circleCollider2D.radius = radios;

                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }
    }

    /// <summary>
    /// 攻撃を与える判定のトリガー
    /// インターフェース
    /// </summary>
    public interface IAttackColliderOfOnmyoBullet
    {
        /// <summary>
        /// 円形コライダー2Dの半径をセット
        /// </summary>
        /// <param name="radios">半径</param>
        /// <returns>成功／失敗</returns>
        public bool SetRadiosOfCircleCollier2D(float radios);
    }
}