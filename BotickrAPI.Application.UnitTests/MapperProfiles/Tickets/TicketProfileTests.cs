using AutoMapper;
using BotickrAPI.Application.Dtos.Tickets;
using BotickrAPI.Domain.Entities;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace BotickrAPI.Application.UnitTests.MapperProfiles.Tickets
{
    public class TicketProfileTests : IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public TicketProfileTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void Should_Map_NewTicketDto_To_TicketEntity()
        {
            // Arrange
            var newTicketDto = new NewTicketDto
            {
                Price = 100.00,
                Quantity = 50,
                TicketType = "VIP"
            };

            // Act
            var ticketEntity = _mapper.Map<TicketEntity>(newTicketDto);

            // Assert
            ticketEntity.Price.Should().Be(newTicketDto.Price);
            ticketEntity.Quantity.Should().Be(newTicketDto.Quantity);
            ticketEntity.TicketType.Should().Be(newTicketDto.TicketType);
            ticketEntity.Event.Should().BeNull();
            ticketEntity.StatusId.Should().Be(1);
            ticketEntity.Created.Should().Be(default);
            ticketEntity.BookingDetails.Should().BeNull();
            ticketEntity.Modified.Should().Be(null);
            ticketEntity.ModifiedBy.Should().BeNull();
            ticketEntity.Inactivated.Should().BeNull();
            ticketEntity.InactivatedBy.Should().BeNull();
            ticketEntity.EventId.Should().Be(0);
            ticketEntity.Id.Should().Be(default);
            ticketEntity.CreatedBy.Should().Be(string.Empty);
        }

        [Fact]
        public void Should_Map_TicketEntity_To_TicketDto()
        {
            // Arrange
            var bookingDetails = new List<BookingDetailEntity>
            {
                new BookingDetailEntity { Quantity = 20 },
                new BookingDetailEntity { Quantity = 10 }
            };

            var ticketEntity = new TicketEntity
            {
                Price = 150.00,
                Quantity = 50,
                TicketType = "Standard",
                BookingDetails = bookingDetails
            };

            // Act
            var ticketDto = _mapper.Map<TicketDto>(ticketEntity);

            // Assert
            ticketDto.Price.Should().Be(ticketEntity.Price);
            ticketDto.TicketType.Should().Be(ticketEntity.TicketType);
            ticketDto.TotalQuantity.Should().Be(ticketEntity.Quantity);
            ticketDto.AvailableQuantity.Should().Be(ticketEntity.Quantity - 30);
            ticketDto.IsSoldOut.Should().BeFalse();
        }

        [Fact]
        public void Should_Map_TicketEntity_To_TicketDto_When_SoldOut()
        {
            // Arrange
            var bookingDetails = new List<BookingDetailEntity>
            {
                new BookingDetailEntity { Quantity = 25 },
                new BookingDetailEntity { Quantity = 25 }
            };

            var ticketEntity = new TicketEntity
            {
                Price = 200.00,
                Quantity = 50,
                TicketType = "Standard",
                BookingDetails = bookingDetails
            };

            // Act
            var ticketDto = _mapper.Map<TicketDto>(ticketEntity);

            // Assert
            ticketDto.Price.Should().Be(ticketEntity.Price);
            ticketDto.TicketType.Should().Be(ticketEntity.TicketType);
            ticketDto.TotalQuantity.Should().Be(ticketEntity.Quantity);
            ticketDto.AvailableQuantity.Should().Be(0);
            ticketDto.IsSoldOut.Should().BeTrue();
        }

        [Fact]
        public void Should_Map_TicketEntity_To_TicketDto_When_BookingDetails_Empty()
        {
            var ticketEntity = new TicketEntity
            {
                Price = 100,
                Quantity = 10,
                TicketType = "VIP",
                BookingDetails = new List<BookingDetailEntity>()
            };

            var ticketDto = _mapper.Map<TicketDto>(ticketEntity);

            ticketDto.Price.Should().Be(ticketEntity.Price);
            ticketDto.TotalQuantity.Should().Be(ticketEntity.Quantity);
            ticketDto.TicketType.Should().Be(ticketEntity.TicketType);
            ticketDto.AvailableQuantity.Should().Be(ticketEntity.Quantity);
            ticketDto.IsSoldOut.Should().BeFalse();
        }
    }
}
