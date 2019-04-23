using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT3_OutputCollab
    {
        private IOutput output;
        private IDisplay disp;
        private ITimer timer;
        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;
        private IDoor door;
        private CookController cc;
        private IUserInterface userInterface;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>(); 
            disp = new Display(output);
            timer = new Timer();
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();
            door = new Door();

            cc = new CookController(Substitute.For<ITimer>(), disp, Substitute.For<IPowerTube>());
            userInterface = 
                new UserInterface(powerButton, 
                    timeButton,
                    startCancelButton,
                    door,
                    disp,
                    Substitute.For<ILight>(),
                    cc);

            cc.UI = userInterface;
        }

        [Test]
        public void PressPwrBtn_UserInterfaceIsReady_DisplayShows50Power()
        {
            //Arrange
            //Act
            powerButton.Press();

            //Assert
            output.Received().OutputLine("Display shows: 50 W");
        }

        [Test]
        public void PressPwrBtn_PwrBtnPressedOnce_DisplayShows100Power()
        {
            //Arrange
            powerButton.Press();

            //Act
            powerButton.Press();

            //Assert
            output.Received().OutputLine("Display shows: 100 W");
        } 

        [Test]
        public void PressPwrBtn_WattageIsAlready700_DisplayShows50Power()
        {
            //Arrange
            for (int i = 0; i < 14; i++)
            {
                powerButton.Press();
            }
            
            //Act
            powerButton.Press();

            //Assert
            output.Received().OutputLine("Display shows: 50 W");
        }

        [Test]
        public void PressTimerBtn_PowerIsSelected_DisplayShows1Minute0Seconds()
        {
            //Arrange
            powerButton.Press();

            //Act
            timeButton.Press();

            //Assert
            output.Received().OutputLine("Display shows: 01:00");
        }

        [Test]
        public void PressTimerBtn_TimeAlready1Minute_DisplayShows2Minutes0Seconds()
        {
            //Arrange
            powerButton.Press();
            timeButton.Press();

            //Act
            timeButton.Press();

            //Assert
            output.Received().OutputLine("Display shows: 02:00");
        }

        [Test]
        public void PressStartBtn_DuringSetup_DisplayClears()
        {
            //Arrange
            powerButton.Press();

            //Act
            startCancelButton.Press();

            //Assert
            output.Received(1).OutputLine("Display cleared");
        }

        [Test]
        public void DoorOpens_DuringSetup_DisplayClears()
        {
            //Arrange
            powerButton.Press();

            //Act
            door.Open();

            //Assert
            output.Received(1).OutputLine("Display cleared");
        }

        [Test]
        public void PressStartBtn_DuringCooking_DisplayClears()
        {
            //Arrange
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            //Act
            startCancelButton.Press();

            //Assert
            output.Received(1).OutputLine("Display cleared");
        }

        [Test]
        public void DoorOpens_UserInterfaceIsCooking_DisplayClears()
        {
            //Arrange
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();

            //Act
            door.Open();

            //Assert
            output.Received(1).OutputLine("Display cleared");
        }
    }
}
