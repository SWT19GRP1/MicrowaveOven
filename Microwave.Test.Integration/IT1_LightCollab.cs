using System;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class IT1_LightCollab
    {
        private IDoor door;
        private ILight light;
        private IOutput output;
        private IButton startCancelButton;
        private IButton powerButton;
        private IButton timeButton;
        //private bool wasCalled;

        [SetUp]
        public void Setup()
        {
            door = new Door();

            output = Substitute.For<IOutput>();
            light = new Light(output);
            startCancelButton = new Button();
            powerButton = new Button();
            timeButton = new Button();

            var userInterface = new UserInterface(powerButton,
                timeButton, startCancelButton,
                door, Substitute.For<IDisplay>(), light, Substitute.For<ICookController>());

        }

        [Test]
        public void StartButtonIsPressed_LightTurnsOn()
        {
            //Arrange
            powerButton.Press();
            timeButton.Press();
            //Act
            startCancelButton.Press();
            //Assert
            output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void CancelButtonIsPressed_LightTurnsOff()
        {
            //This is the same test as if the timer is timed out, seen from the Light
            //class perspective. 
        
            //Arrange
            powerButton.Press();
            timeButton.Press();
            //Power up
            startCancelButton.Press();
            //power down - Act
            startCancelButton.Press();
            //Assert
            output.Received().OutputLine("Light is turned off");
        }

        [Test]
        public void DoorIsOpenedBeforeCooking_LightTurnsOn()
        {
            //Arrange
            //Act
            door.Open();
            //Assert
            output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void DoorIsClosedBeforeCooking_LightTurnsOff()
        {
            //Arrange
            door.Open();
            //Act
            door.Close();
            //Assert
            output.Received().OutputLine("Light is turned off");
        }

        [Test]
        public void DoorIsOpenedUnderCooking_LightTurnsOn()
        {
            //Arrange
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            //Act
            door.Open();
            //Assert
            output.Received().OutputLine("Light is turned on");
        }
    }
}
