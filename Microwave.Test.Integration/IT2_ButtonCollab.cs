using System.Runtime.InteropServices;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
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

        [SetUp]
        public void Setup()
        {
            _powerButton = new Button();
            _startCancelButton = new Button();
            _timeButton = new Button();
            _door = new Door();
        
  

        }
         



    }
}