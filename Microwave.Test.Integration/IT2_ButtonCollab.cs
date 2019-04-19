using System.Runtime.InteropServices;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
   [TestFixture]
    public class IT2_ButtonCollab
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IUserInterface _sut;
        private ICookController _cookController;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IDisplay _display;
        private ILight _light;
        private IOutput _output;

        [SetUp]
        public void Setup()
        {
            _powerButton = NSubstitute.Substitute.For<IButton>();
            _startCancelButton = NSubstitute.Substitute.For<IButton>();
            _timeButton = NSubstitute.Substitute.For<IButton>();
            _door = NSubstitute.Substitute.For<IDoor>();
            _timer = new Timer();
            _output = Substitute.For<Output>();
            _light = Substitute.For<Light>();
            _display = Substitute.For<Display>();
            _sut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController = new CookController(_timer,_display,_powerTube,_sut);

        }
     


    }
}