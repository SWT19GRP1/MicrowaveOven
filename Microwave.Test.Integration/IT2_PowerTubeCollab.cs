using System.Runtime.InteropServices;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
   [TestFixture]
    public class IT2_PowerTubeCollab
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IUserInterface _ui;
        private CookController _cookController;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IDisplay _display;
        private ILight _light;
        private IOutput _output;

        [SetUp]
        public void Setup()
        {
            _powerButton = new Button();
            _startCancelButton = new Button();
            _timeButton = new Button();
            _door = new Door();
            _timer = new Timer();
            _output = Substitute.For<IOutput>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _powerTube= new PowerTube(_output);

            _cookController = new CookController(_timer, _display, _powerTube);
            _ui = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _ui;
        }

        [Test]
        public void StartButtonIsPressedDuringSetupWith50WattagePowerTubeRunsWith50Watt()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine("PowerTube works with 50 W");

        }
        [Test]
        public void StartButtonIsPressedDuringSetupWith150WattagePowerTubeRunsWith150Watt()
        {
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine("PowerTube works with 150 W");
        }
        [Test]
        public void WattageIs700PowerButtonPressedOnceWattageIs50()
        {
            for (int i = 0; i < 14; i++)
            {
                _powerButton.Press();
            }

            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine("PowerTube works with 50 W");
        }
        [Test]
        public void CancelButtonIsPressedDuringSetupSettingsAreReset()
        {
            _powerButton.Press();
            _powerButton.Press();
            _powerButton.Press();
            _startCancelButton.Press();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine("PowerTube works with 50 W");
        }
        [Test]
        public void CancelButtonIsPressedDuringCookingPowerTubeTurnedOff()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine("PowerTube turned off");
        }
        [Test]
        public void CancelButtonIsPressedDuringCookingSettingsAreReset()
        {

        }
        [Test]
        public void DoorIsOpenedDuringCookingPowerTubeTurnedOff()
        {
        }
        [Test]
        public void DoorIsOpenedDuringCookingSettingsAreReset()
        {
        }
        [Test]
        public void TimerSetTo20SecondsAfter20SecondsPowerTubeTurnedOff()
        {
        }
    }
}