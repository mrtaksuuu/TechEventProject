using System;

namespace TechEvent.Concrete
{
    internal class Biletci : BiletFirmalari
    {
        public override void EtkinlikleriJSONOlarakAlir()
        {
            string data = TechEventHelper.JSONDataGonder();
            this.DataCekildiMi = true;
        }

        public override void EtkinlikleriXMLOlarakAlir()
        {
            Console.WriteLine("Biletci firması dataları XML olarak almamaktadır");
        }
    }
}
