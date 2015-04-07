using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using System.Collections.Generic;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		

        [TestMethod]
        public void TestThatCarGetCarLocation()
        {
            String carLocation ="";
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            List<Int32> Cars = new List<int>();

            Expect.Call(mockDB.getCarLocation(5)).Return(carLocation);

            mocks.ReplayAll();

            targetCar.Database = mockDB;

            String result;
            result = targetCar.getCarLocation(5);
            Assert.AreEqual(carLocation, result);
            mocks.VerifyAll();
        }
        [TestMethod]
        public void TestThatCarMileage()
        {
            Int32 carMiles= 55000;
            IDatabase mockDB = mocks.StrictMock<IDatabase>();

            Int32 Miles = 55000;
            Expect.Call(mockDB.Miles).PropertyBehavior();
            mocks.ReplayAll();
            targetCar = ObjectMother.BMW(); //showing use of ObjectMother
            mockDB.Miles = Miles;
            targetCar.Database = mockDB;
            Int32 result = targetCar.Mileage;
            Assert.AreEqual(carMiles, result);
            mocks.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestThatCarThrowsOnBadLength()
        {
            new Car(-5);
        }
	}
}
