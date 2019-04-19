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
        private bool wasCalled;

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
            wasCalled = false;

        }

        //[Test]
        //public void DoorOpens_OnDoorOpenedWas()
        //{           
        //    door.Opened += (o, e) => wasCalled = true;
        //    door.Open();
        //    Assert.IsTrue(wasCalled);
        //}

        //[Test]
        //public void DoorCloses_OnDoorClosedWasCalled()
        //{
        //    door.Closed += (o, e) => wasCalled = true;
        //    door.Close();
        //    Assert.IsTrue(wasCalled);
        //}

        [Test]
        public void StartButtonIsPressed_LightTurnsOn()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void CancelButtonIsPressed_LightTurnsOff()
        {
            //This is the same test as if the timer is timed out, seen from the Light
            //class perspective. 
        
            powerButton.Press();
            timeButton.Press();
            //Power up
            startCancelButton.Press();
            //power down
            startCancelButton.Press();

            output.Received().OutputLine("Light is turned off");
        }

        [Test]
        public void DoorIsOpenedBeforeCooking_LightTurnsOn()
        {
            door.Open();
            output.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void DoorIsClosedBeforeCooking_LightTurnsOff()
        {
            door.Open();
            door.Close();

            output.Received().OutputLine("Light is turned off");
        }

        [Test]
        public void DoorIsOpenedUnderCooking_LightTurnsOn()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            door.Open();

            output.Received().OutputLine("Light is turned on");
        }
    }
}
