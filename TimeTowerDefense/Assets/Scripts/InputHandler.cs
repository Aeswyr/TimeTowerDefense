using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float dir {
        get;
        private set;
    }
    public ButtonState jump {
        get{return m_jump;}
    }
    public ButtonState mode {
        get {return m_mode;}
    }
    private ButtonState m_mode, m_jump;

    private void FixedUpdate() {
        this.m_jump.Reset();
        this.m_mode.Reset();
    }

    public void Move(InputAction.CallbackContext ctx) {
        this.dir = ctx.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext ctx) {
        this.m_jump.Set(ctx);
    }

    public void Mode(InputAction.CallbackContext ctx) {
        this.m_mode.Set(ctx);
    }

    public struct ButtonState {
        private bool firstFrame;
        public bool down {
            get;
            private set;
        }
        public bool pressed {
            get {
                if (down)
                    Debug.Log($"{down}, {firstFrame}");
                return down && firstFrame;
            }
        }
        public bool released {
            get {
                return !down && firstFrame;
            }
        }

        public void Set(InputAction.CallbackContext ctx) {
            down = !ctx.canceled;             
            firstFrame = true;
        }
        public void Reset() {
            firstFrame = false;
        }
    }
}
