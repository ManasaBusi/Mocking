using CreditCardApplications;
using Moq;
namespace Processor.Tests
{
    public class ProcessorShould
    {
        [Fact]
        public void AcceptPersons()
        {
            //Arrange
            var mockGateway = new Mock<IGateway>();

            var person1 = new Person();
            var person2 = new Person();

            mockGateway.Setup(x => x.Execute(ref person1)).Returns(-1);

            var sut = new CreditCardApplications.Processor(mockGateway.Object);

            //Act
            var result1 = sut.Process(person1);
            var result2 = sut.Process(person2);

            //Assert
            Assert.Equal(false, result1);
            Assert.Equal(true, result2);
        }

        [Fact]
        public void AcceptAnyPerson()
        {
            //Arrange
            var mockGateway = new Mock<IGateway>();
            mockGateway.Setup(x=> x.Execute(ref It.Ref<Person>.IsAny)).Returns(-1);   

            var person1 = new Person();
            var person2 = new Person();

            var sut = new CreditCardApplications.Processor(mockGateway.Object);

            //Act
            var result1 = sut.Process(person1);
            var result2 = sut.Process(person2);

            //Assert
            Assert.Equal(false, result1);
            Assert.Equal(false, result2);
        }
    }
}