namespace TechEvent.Concrete
{
    public abstract class BiletFirmalari
    {
        public string FirmaAdi { get; set; }
        public string WebSitesi { get; set; }
        public bool DataCekildiMi { get; set; }

        public abstract void EtkinlikleriXMLOlarakAlir();
        public abstract void EtkinlikleriJSONOlarakAlir();
    }
}
