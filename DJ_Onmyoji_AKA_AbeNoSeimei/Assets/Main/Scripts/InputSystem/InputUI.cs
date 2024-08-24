using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.InputSystem
{
    /// <summary>
    /// UI用のInputAction
    /// </summary>
    public class InputUI : MonoBehaviour, IInputSystemsOwner
    {
        /// <summary>ナビゲーション入力</summaryf>
        private Vector2 _navigated;
        /// <summary>ナビゲーション入力</summaryf>
        public Vector2 Navigated => _navigated;
        /// <summary>
        /// Navigationのアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnNavigated(InputAction.CallbackContext context)
        {
            _navigated = context.ReadValue<Vector2>();
        }

        /// <summary>決定入力</summary>
        private bool _submited;
        /// <summary>決定入力</summary>
        public bool Submited => _submited;
        /// <summary>
        /// Pauseのアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnSubmited(InputAction.CallbackContext context)
        {
            _submited = context.ReadValueAsButton();
        }

        /// <summary>キャンセル入力</summary>
        private bool _canceled;
        /// <summary>キャンセル入力</summary>
        public bool Canceled => _canceled;
        /// <summary>
        /// Cancelのアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnCanceled(InputAction.CallbackContext context)
        {
            _canceled = context.ReadValueAsButton();
        }

        /// <summary>ポーズ入力</summary>
        private bool _paused;
        /// <summary>ポーズ入力</summary>
        public bool Paused => _paused;
        /// <summary>
        /// Pauseのアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnPaused(InputAction.CallbackContext context)
        {
            _paused = context.ReadValueAsButton();
        }

        /// <summary>スペース入力</summary>
        private bool _spaced;
        /// <summary>スペース入力</summary>
        public bool Spaced => _spaced;
        /// <summary>
        /// Spaceのアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnSpaced(InputAction.CallbackContext context)
        {
            _spaced = context.ReadValueAsButton();
        }

        /// <summary>アンドゥ入力</summary>
        private bool _undoed;
        /// <summary>アンドゥ入力</summary>
        public bool Undoed => _undoed;
        /// <summary>
        /// Undoのアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnUndoed(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _undoed = true;
                    break;
                case InputActionPhase.Canceled:
                    _undoed = false;
                    break;
            }
        }

        /// <summary>セレクト入力</summary>
        private bool _selected;
        /// <summary>セレクト入力</summary>
        public bool Selected => _selected;
        /// <summary>
        /// Selectのアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnSelected(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _selected = true;
                    break;
                case InputActionPhase.Canceled:
                    _selected = false;
                    break;
            }
        }

        /// <summary>マニュアル入力</summary>
        private bool _manualed;
        /// <summary>マニュアル入力</summary>
        public bool Manualed => _manualed;
        /// <summary>
        /// Manualのアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnManualed(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _manualed = true;
                    break;
                case InputActionPhase.Canceled:
                    _manualed = false;
                    break;
            }
        }

        /// <summary>スクラッチ</summary>
        private Vector2 _scratch;
        /// <summary>スクラッチ</summary>
        public Vector2 Scratch => _scratch;
        /// <summary>
        /// Scratchのアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnScratch(InputAction.CallbackContext context)
        {
            _scratch = context.ReadValue<Vector2>();
        }

        /// <summary>昼チャージ入力</summary>
        private bool _chargeSun;
        /// <summary>昼チャージ入力</summary>
        public bool ChargeSun => _chargeSun;
        /// <summary>
        /// 昼チャージ入力のアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnChargeSun(InputAction.CallbackContext context)
        {
            _chargeSun = context.ReadValueAsButton();
        }

        /// <summary>夜チャージ入力</summary>
        private bool _chargeMoon;
        /// <summary>夜チャージ入力</summary>
        public bool ChargeMoon => _chargeMoon;
        /// <summary>
        /// 夜チャージ入力のアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnChargeMoon(InputAction.CallbackContext context)
        {
            _chargeMoon = context.ReadValueAsButton();
        }

        /// <summary>フェーダー（右）チャージ入力</summary>
        private bool _chargeRFader;
        /// <summary>フェーダー（右）チャージ入力</summary>
        public bool ChargeRFader => _chargeRFader;
        /// <summary>
        /// フェーダー（右）チャージ入力のアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnChargeRFader(InputAction.CallbackContext context)
        {
            _chargeRFader = context.ReadValueAsButton();
        }

        /// <summary>フェーダー（左）チャージ入力</summary>
        private bool _chargeLFader;
        /// <summary>フェーダー（左）チャージ入力</summary>
        public bool ChargeLFader => _chargeLFader;
        /// <summary>
        /// フェーダー（左）チャージ入力のアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnChargeLFader(InputAction.CallbackContext context)
        {
            _chargeLFader = context.ReadValueAsButton();
        }

        /// <summary>フェーダー（右）解放入力</summary>
        private bool _releaseRFader;
        /// <summary>フェーダー（右）解放入力</summary>
        public bool ReleaseRFader => _releaseRFader;
        /// <summary>
        /// フェーダー（右）解放入力のアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnReleaseRFader(InputAction.CallbackContext context)
        {
            _releaseRFader = context.ReadValueAsButton();
        }

        /// <summary>フェーダー（左）解放入力</summary>
        private bool _releaseLFader;
        /// <summary>フェーダー（左）解放入力</summary>
        public bool ReleaseLFader => _releaseLFader;
        /// <summary>
        /// フェーダー（左）解放入力のアクションに応じてフラグを更新
        /// </summary>
        /// <param name="context">コールバック</param>
        public void OnReleaseLFader(InputAction.CallbackContext context)
        {
            _releaseLFader = context.ReadValueAsButton();
        }

        public void DisableAll()
        {
            _navigated = new Vector2();
            _submited = false;
            _canceled = false;
            _paused = false;
            _spaced = false;
            _undoed = false;
            _selected = false;
            _manualed = false;
            _scratch = new Vector2();
            _chargeSun = false;
            _chargeRFader = false;
            _chargeLFader = false;
            _releaseRFader = false;
            _releaseLFader = false;
        }
    }
}
