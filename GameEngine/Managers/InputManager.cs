using MoteurJeuxProjetFinal.GameEngine.Managers.Inputs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MoteurJeuxProjetFinal.GameEngine.Managers
{
    internal class InputManager
    {
        private string inputFileName = "";

        private enum Controller { KEYBOARD = 0 };

        private GameEngine _gameEngine;

        private static readonly Controller _controller = Controller.KEYBOARD;

        private List<Input> _inputs;

        public string InputFileName { get => inputFileName }

        internal static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }

        private static InputManager instance = null;

        private InputManager()
        {
            this._gameEngine = null;
            this._inputs = new List<Input>();
        }

        public void Init(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            inputFileName = _gameEngine.inputsPath;
            LoadInputs();
        }

        private void LoadInputs()
        {
            if (File.Exists(InputFileName))
            {
                string jsonInput = File.ReadAllText(InputFileName);
                this._inputs = JsonConvert.DeserializeObject<List<Input>>(jsonInput);
            }
            else
            {
                File.WriteAllText(InputFileName, JsonConvert.SerializeObject(this._inputs, Formatting.Indented));
            }
        }

        public void ManageKeyPress(KeyEventArgs key)
        {
            if (_controller == Controller.KEYBOARD)
            {
                foreach (Input input in _inputs)
                {
                    if (input is Inputs.Button btn)
                    {
                        if (btn.KeyPlus == key.ToString())
                        {
                            btn.IsPressed = true;
                        }
                    }
                    else if (input is Axis axis)
                    {
                        if (axis.KeyMinus == key.ToString())
                        {
                            axis.Value = -1f;
                            axis.IsPressed = true;
                        }
                        else if (axis.KeyPlus == key.ToString())
                        {
                            axis.Value = 1f;
                            axis.IsPressed = true;
                        }
                    }
                }
            }
            key.Handled = true;
        }

        public void ManageKeyRelease(KeyEventArgs key)
        {
            if (_controller == Controller.KEYBOARD)
            {
                foreach (Input input in _inputs)
                {
                    if (input is Inputs.Button btn)
                    {
                        if (btn.IsPressed && btn.KeyPlus == key.ToString())
                        {
                            btn.IsPressed = false;
                        }
                    }
                    else if (input is Axis axis)
                    {
                        if (axis.IsPressed && (axis.Value < 0) && axis.KeyMinus == key.ToString())
                        {
                            axis.Value = 0f;
                            axis.IsPressed = false;
                        }
                        else if (axis.IsPressed && (axis.Value > 0) && axis.KeyPlus == key.ToString())
                        {
                            axis.Value = 0f;
                            axis.IsPressed = false;
                        }
                    }
                }
            }
            key.Handled = true;
        }

        public float GetAxisI(string axisName)
        {
            Axis axis = (Axis)this._inputs.Where(o => o is Axis input && input.Name.Equals(axisName)).FirstOrDefault();
            if (axis != null)
                return axis.Value;
            else
                return 0f;
        }

        public bool GetButtonI(string btnName)
        {
            Inputs.Button button = (Inputs.Button)this._inputs.Where(o => o is Inputs.Button btn && btn.Name.Equals(btnName)).FirstOrDefault();
            if (button != null)
                return button.IsPressed;
            else
                return false;
        }

        public static float GetAxis(string axisName) => instance.GetAxisI(axisName);

        public static bool GetButton(string btnName) => instance.GetButtonI(btnName);
    }
}