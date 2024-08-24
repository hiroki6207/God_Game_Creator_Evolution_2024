using System.Collections;
using System.Collections.Generic;
using Main.Common;
using Main.View;
using UnityEngine;

namespace Main.Presenter
{
    /// <summary>
    /// クリアカウントダウンタイマービューアダプターのインターフェース
    /// </summary>
    public interface IClearCountdownTimerViewAdapter
    {
        /// <summary>
        /// セットメソッド
        /// </summary>
        bool Set(float timeSec, float limitTimeSecMax);
        /// <summary>
        /// セットメソッド
        /// </summary>
        bool Set(float timeSec, float limitTimeSecMax, int isTimeOutState);
    }

    /// <summary>
    /// クリアカウントダウンタイマーサークルビューアダプター
    /// </summary>
    public class ClearCountdownTimerCircleViewAdapter : IClearCountdownTimerViewAdapter
    {
        private ClearCountdownTimerCircleView view;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClearCountdownTimerCircleViewAdapter(ClearCountdownTimerCircleView view)
        {
            this.view = view;
        }

        /// <summary>
        /// セットメソッド
        /// </summary>
        public bool Set(float timeSec, float limitTimeSecMax)
        {
            return view.SetAngle(timeSec, limitTimeSecMax);
        }

        public bool Set(float timeSec, float limitTimeSecMax, int isTimeOutState)
        {
            try
            {
                switch ((IsTimeOutState)isTimeOutState)
                {
                    case IsTimeOutState.TimeOut:
                        if (!view.SetAngle(timeSec, limitTimeSecMax))
                            throw new System.Exception("SetAngle");

                        break;
                    default:
                        // それ以外
                        break;
                }

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
    /// クリアカウントダウンタイマーゲージビューアダプター
    /// </summary>
    public class ClearCountdownTimerGaugeViewAdapter : IClearCountdownTimerViewAdapter
    {
        private ClearCountdownTimerGaugeView view;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClearCountdownTimerGaugeViewAdapter(ClearCountdownTimerGaugeView view)
        {
            this.view = view;
        }

        /// <summary>
        /// セットメソッド
        /// </summary>
        public bool Set(float timeSec, float limitTimeSecMax)
        {
            return view.SetHorizontal(timeSec, limitTimeSecMax);
        }

        public bool Set(float timeSec, float limitTimeSecMax, int isTimeOutState)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// クリアカウントダウンタイマーテキストビューアダプター
    /// </summary>
    public class ClearCountdownTimerTextViewAdapter : IClearCountdownTimerViewAdapter
    {
        private ClearCountdownTimerTextView view;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClearCountdownTimerTextViewAdapter(ClearCountdownTimerTextView view)
        {
            this.view = view;
        }

        /// <summary>
        /// セットメソッド
        /// </summary>
        public bool Set(float timeSec, float limitTimeSecMax)
        {
            return view.SetTextImport(timeSec, limitTimeSecMax);
        }

        public bool Set(float timeSec, float limitTimeSecMax, int isTimeOutState)
        {
            throw new System.NotImplementedException();
        }
    }
}
