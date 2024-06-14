using Moq;
namespace CreditCardApplications.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        [Fact]
        public void AcceptHighIncomeApplications()
        {
            //Arrange
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { GrossAnnualIncome = 100_000 };

            //Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            //Assert
            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        //Used DefaultValue.Mock for setting ServiceInformation.License.LicenseKey
        [Fact]
        public void ReferYoungApplications()
        {
            //Arrange
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            mockValidator.DefaultValue = DefaultValue.Mock;
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);


            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { Age = 19 };

            //Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            //Assert
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        //Used Property Hierarchy set up with x.ServiceInformation.License.LicenseKey
        //Used mock setup to return a specific value
        [Fact]
        public void DeclineLowIncomeApplications()
        {
            //Arrange
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            //mockValidator.Setup(x => x.IsValid("x")).Returns(true);
            //mockValidator.Setup(x=> x.IsValid(It.Is<string>(y=> y.StartsWith("y")))).Returns(true);
            //mockValidator.Setup(x=> x.IsValid(It.IsInRange("a","z", Moq.Range.Inclusive))).Returns(true);
            //mockValidator.Setup(x=> x.IsValid(It.IsIn("x","y","z"))).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsRegex("[a-z]"))).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { GrossAnnualIncome = 19_999, Age = 42, FrequentFlyerNumber = "y" };

            //Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            //Assert
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);

        }

        //Used Property Hierarchy set up with x.ServiceInformation.License.LicenseKey
        [Fact]
        public void ReferInvalidFrequentFlyerApplications()
        {
            //Arrange
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication();

            //mockValidator.DefaultValue = DefaultValue.Mock;
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);

            //Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            //Assert
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        //[Fact]
        //public void DeclineLowIncomeApplicationsOutDemo()
        //{
        //    //Arrange
        //    Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>(MockBehavior.Strict);

        //    bool isValid = true;

        //    mockValidator.Setup(x => x.IsValid(It.IsAny<string>(), out isValid));

        //    var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

        //    var application = new CreditCardApplication { GrossAnnualIncome = 19_999, Age = 42 };

        //    //Act
        //    CreditCardApplicationDecision decision = sut.EvaluateUsingOut(application);

        //    //Assert
        //    Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);

        //}

        //Used Property Hierarchy set up with x.ServiceInformation.License.LicenseKey
        //Used setting up of a Return value through a function "GetLicenseKeyExpiryString"
        [Fact]
        public void ReferWhenLicenseKeyExpired()
        {

            //var mockLicenseData = new Mock<ILicenseData>();
            //mockLicenseData.Setup(x => x.LicenseKey).Returns("EXPIRED");
            //var mockServiceInfo = new Mock<IServiceInformation>();
            //mockServiceInfo.Setup(x => x.License).Returns(mockLicenseData.Object);
            //var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation).Returns(mockServiceInfo.Object);

            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("EXPIRED");
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns(GetLicenseKeyExpiryString);

            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication { Age = 42 };

            //Act
            CreditCardApplicationDecision decision = sut.Evaluate(application);

            //Asert
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);


        }

        string GetLicenseKeyExpiryString()
        {
            // Ex: read from vendor-supplied constants file
            return "EXPIRED";
        }

        //Used SetupProperty in this test
        //Used SetupAllProperties in this test
        [Fact]
        public void UseDetailedLookupForOlderApplications()
        {
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();

            //If we want to enable change tracking or value to be remembered, we need to use SetupProperty
            //mockValidator.SetupProperty(x => x.ValidationMode);

            //If we have more than 1 property on the mockObject to enable change tracking for, we can use SetupAllProperties. Also it must be done before doing any explicit setups.

            //Arrange

            mockValidator.SetupAllProperties();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { Age = 30 };

            //Act
            sut.Evaluate(application);

            //Assert
            Assert.Equal(ValidationMode.Detailed, mockValidator.Object.ValidationMode);
        }

        [Fact]
        public void ValidateFrequentFlyerNumberForLowIncomeApplications()
        {
            //Arrange
            var mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            mockValidator.Setup(x => x.ServiceInformation.License.LicenseKey).Returns("OK");

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);

            var application = new CreditCardApplication();

            //Act
            sut.Evaluate(application);

            mockValidator.Verify(x => x.IsValid(null),Times.Never);
        }

    }
}