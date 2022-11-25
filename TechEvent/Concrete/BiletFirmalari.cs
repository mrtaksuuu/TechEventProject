using TechEvent.Abstract;

namespace TechEvent.Concrete
{
    public abstract class BiletFirmalari : IBaseEntity
    {
        public string FirmaAdi { get; set; }
        public string WebSitesi { get; set; }
        public bool DataCekildiMi { get; set; }

        public abstract void EtkinlikleriXMLOlarakAlir();
        public abstract void EtkinlikleriJSONOlarakAlir();

        public void BiletSat()
        {

        }
    }
}
