namespace TechEvent.Concrete
{
    internal class BiletEvi : BiletFirmalari
    {
        public override void EtkinlikleriJSONOlarakAlir()
        {
            string data = TechEventHelper.JSONDataGonder();
            this.DataCekildiMi = true;
        }

        public override void EtkinlikleriXMLOlarakAlir()
        {
            string data = TechEventHelper.XMLDataOlustur();
            this.DataCekildiMi = true;
        }
    }
}
