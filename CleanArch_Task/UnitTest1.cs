using Xunit;
using Moq;
using CleanArch_Task.Application.Service;
using CleanArch_Task.Domain.Entities;
using CleanArch_Task.Domain.IRepo;
using CleanArch_Task.Application.DTOs;

namespace CleanArch_Task.Tests
{
    public class EventServiceTests
    {
        private readonly Mock<IRepo> _mockRepo;
        private readonly Service _service;

        public EventServiceTests()
        {
            _mockRepo = new Mock<IRepo>();
            _service = new Service(_mockRepo.Object);
        }

        [Fact]
        public void CreateEvent_ShouldReturnCreatedEventDTO()
        {
            // Arrange
            var eventDto = new EventDTO
            {
                Title = "Test Title",
                Description = "Test Description",
                VenueId = 1
            };

            var createdEntity = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                VenueId = eventDto.VenueId
            };

            _mockRepo.Setup(r => r.CreateEvent(It.IsAny<Event>()))
                     .Returns(createdEntity);

            // Act
            var result = _service.CreateEvent(eventDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventDto.Title, result.Title);
            Assert.Equal(eventDto.Description, result.Description);
            Assert.Equal(eventDto.VenueId, result.VenueId);
        }

        [Fact]
        public void GetAllEvent_ShouldReturnListOfEvents()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event { Title = "Event 1", Description = "Desc 1", VenueId = 1 },
                new Event { Title = "Event 2", Description = "Desc 2", VenueId = 2 }
            };

            _mockRepo.Setup(r => r.GetAllEvent()).Returns(events);

            // Act
            var result = _service.GetAllEvent();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void UpdateEvent_ShouldReturnUpdatedDTO_WhenSuccessful()
        {
            // Arrange
            var eventDto = new EventDTO
            {
                Title = "Updated Title",
                Description = "Updated Description",
                VenueId = 2
            };

            var updatedEntity = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                VenueId = eventDto.VenueId
            };

            _mockRepo.Setup(r => r.UpdateEvent(1, It.IsAny<Event>()))
                     .Returns(updatedEntity);

            // Act
            var result = _service.UpdateEvent(1, eventDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventDto.Title, result.Title);
        }

        [Fact]
        public void UpdateEvent_ShouldReturnNull_WhenEventNotFound()
        {
            // Arrange
            var eventDto = new EventDTO
            {
                Title = "Updated Title",
                Description = "Updated Description",
                VenueId = 2
            };

            _mockRepo.Setup(r => r.UpdateEvent(99, It.IsAny<Event>()))
                     .Returns((Event)null);

            // Act
            var result = _service.UpdateEvent(99, eventDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteEvent_ShouldReturnTrue_WhenDeleted()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteEvent(1)).Returns(true);

            // Act
            var result = _service.DeleteEvent(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteEvent_ShouldReturnFalse_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteEvent(99)).Returns(false);

            // Act
            var result = _service.DeleteEvent(99);

            // Assert
            Assert.False(result);
        }
    }
}
