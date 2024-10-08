﻿namespace EasyBooking.Domain.Model;

public class Reservation
{
    public int Id { get; private set; }

    public int ReservedDeskId { get; set; }
    public int ReservingEmployeeId { get; private set; }
    public DateOnly FromDate { get;  set; }
    public DateOnly ToDate { get; set; }

    public Reservation(int reservedDeskId, int reservingEmployeeId, DateOnly fromDate, DateOnly toDate)
    {
        if (fromDate > toDate)
        {
            throw new ArgumentException("Reservation cannot end before it starts");
        }

        if (fromDate.AddDays(7) < toDate)
        {
            throw new ArgumentException("Reservation cannot be longer than a week");
        }
        ReservedDeskId = reservedDeskId;
        ReservingEmployeeId = reservingEmployeeId;
        FromDate = fromDate;
        ToDate = toDate;
    }
}
