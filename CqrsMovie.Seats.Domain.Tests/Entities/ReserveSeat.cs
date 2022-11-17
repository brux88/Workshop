using CqrsMovie.Messages.Commands.Seat;
using CqrsMovie.Messages.Dtos;
using CqrsMovie.Messages.Events.Seat;
using CqrsMovie.Seats.Domain.CommandHandlers;
using CqrsMovie.SharedKernel.Domain.Ids;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsMovie.Seats.Domain.Tests.Entities
{
	public class ReserveSeatsTest : CommandSpecification<ReserveSeats>
  {
    private readonly DailyProgrammingId aggregateId = new DailyProgrammingId(Guid.NewGuid());
    private readonly MovieId movieId = new MovieId(Guid.NewGuid());
    private readonly ScreenId screenId = new ScreenId(Guid.NewGuid());
    private readonly DateTime dailyDate = DateTime.Today;
    private readonly string movieTitle = "rambo";
    private readonly string screenName = "screen 99";
    private readonly IEnumerable<Seat> initialSeats;
    private readonly IEnumerable<Seat> seatsToReserve;

    public ReserveSeatsTest()
    {
      initialSeats = new List<Seat> {
        new Seat { Number = 1, Row = "A" },
        new Seat { Number = 1, Row = "B" }
      };

      seatsToReserve =  new List<Seat> {
         new Seat { Number = 1, Row = "A" }
      };
    }

    protected override IEnumerable<DomainEvent> Given()
    {
      return new List<DomainEvent>()
      {
        new DailyProgrammingCreated(aggregateId, movieId, screenId, dailyDate, initialSeats, movieTitle, screenName)
      };
     
    }

    protected override ReserveSeats When()
    {
      return new ReserveSeats(aggregateId, seatsToReserve);
    }

    protected override ICommandHandler<ReserveSeats> OnHandler()
    {
      return new ReserveSeatsCommandHandler(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
      yield return new SeatsReserved(aggregateId, seatsToReserve);
    }
  }
}
