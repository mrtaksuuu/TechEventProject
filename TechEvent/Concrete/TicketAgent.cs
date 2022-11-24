using System;

namespace TechEvent.Concrete
{
    internal class TicketAgent : BiletFirmalari
    {
        public override void EtkinlikleriJSONOlarakAlir()
        {
            Console.WriteLine("TicketAgent firması dataları JSON olarak almamaktadır");
        }

        public override void EtkinlikleriXMLOlarakAlir()
        {
            string data = TechEventHelper.XMLDataOlustur();
            DataCekildiMi = true;
        }
    }
}
