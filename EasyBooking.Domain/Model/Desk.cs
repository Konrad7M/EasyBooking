using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBooking.Domain.Model;

public class Desk
{
    public Desk(bool isAvailable = true)
    {
        IsAvailable = isAvailable;
    }

    public int Id { get; set; }
    public bool IsAvailable { get; set; }
}
