using System;
using System.Collections.Generic;
using CqrsMovie.SharedKernel.Domain.Ids;
using Muflone.Messages.Commands;

namespace CqrsMovie.Messages.Commands.Seat
{
  public class ReserveSeats : Command
  {

    public IEnumerable<Dtos.Seat> Seats { get; }

    public ReserveSeats(DailyProgrammingId aggregateId,   IEnumerable<Dtos.Seat> seats) : base(aggregateId)
    {
      Seats = seats;
 
    }


  }
}
