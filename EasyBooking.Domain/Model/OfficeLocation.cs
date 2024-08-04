using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBooking.Domain.Model;

public class OfficeLocation
{
    public int Id { get; set; }
    public string LocationName { get; set; }
    public List<Desk> Desks { get; set; }

    public OfficeLocation(string locationName) 
    { 
        LocationName = locationName;
        Desks = new List<Desk>();
    }

    public void AddDesk(Desk desk) 
    {  
        Desks.Add(desk); 
    }

    public void RemoveDesk(Desk desk) 
    {
        Desks.Remove(desk);
    }
}