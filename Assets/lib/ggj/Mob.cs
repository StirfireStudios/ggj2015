using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GGJ {

    /// The animation states for characters
    public enum MobState {
        Static,
        Move,
        Attack,
        Jump,
        Dead,
        None
    }

    /// A state manager for mobs
    public class MobStateBucket {

        /** The actual current playing state, or None */
        private MobState _state;

        /** The requested state, which is currently pending, or None */
        private MobState _requested;

        /** Was the last state an wait-for-animation request? */
        private bool _waiting;

        /** The current actual state */
        public MobState state {
            get { return _state; }
        }

        /**
        * Attempt to request a new state
        *
        * This will only work if there is no pending state, and if the
        * previously requested animation is completed
        */
        public bool request(MobState state, bool wait) {
            if (_waiting) {
                return false;
            }
            if (_requested != MobState.None) {
                return false;
            }
            if (state == _state) {
                return false;
            }
            _requested = state;
            _waiting = wait;
            return true;
        }

        /** Request shortcut for normal ops */
        public bool request(MobState state) {
            return this.request(state, false);
        }

        /** Reset the state */
        public void reset() {
            this._state = MobState.None;
            this._requested = MobState.None;
            this._waiting = false;
        }

        /** Notify the state object that a waiting request is complete */
        public void ready() {
            _waiting = false;
        }

        /** Return the next state to apply, or MobState.None */
        public MobState next() {
            if (_requested != MobState.None) {
                _state = _requested;
                _requested = MobState.None;
                return _state;
            }
            return MobState.None;
        }

        public MobStateBucket() {
            this.reset();
        }
    }

    /// Common behaviours and data for all mobile types
    public class Mob : MonoBehaviour {

        /** State manager */
        protected MobStateBucket _state = new MobStateBucket();

        /**
         * Public setter so we can just use SendMessage / BroadcastMessage
         */
        public void SetState(MobState newstate) {
            _state.request(newstate);
        }

        /**
        * Public setter so we can just use SendMessage / BroadcastMessage
        */
        public bool RequestState(MobState newstate, bool wait) {
            return _state.request(newstate, wait);
        }

        /** Finished with a state we're waiting for animation on */
        public void FinishedState() {
            this.FinishedState(MobState.Static);
        }

        /** Finished with a state we're waiting for animation on */
        public void FinishedState(MobState next) {
            _state.ready();
            _state.request(next);
        }

        void Start () {
        }

        void Update () {
            var state = this._state.next();
            if (state != MobState.None) {
                N.Meta._(gameObject).cmp<Animator>().Play(_stateId(state));
            }
        }

        /// Return the state id for the given state code
        protected string _stateId(MobState state) {
          switch (state) {
            case MobState.Static:
            case MobState.None:
                return "Idle";
            case MobState.Move:
                return "Walk";
            case MobState.Attack:
                return "Bite";
            case MobState.Dead:
                return "Death";
          }
          return "Idle";
        }
    }
}
