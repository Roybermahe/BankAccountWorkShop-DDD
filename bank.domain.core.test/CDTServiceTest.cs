using NUnit.Framework;

namespace bank.domain.core.test
{
    public class CDTServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        /**
            HU 7.
            Como Usuario quiero realizar consignar mi dinero a mi CDT para ahorrar el dinero sin tener
            acceso al de acuerdo al término definido.
            Criterios de Aceptación
            7.1 El valor de consignación inicial debe ser de mínimo 1 millón de pesos.
            7.2 Sólo se podrá realizar una sola consignación.

            MSJ: Solo se acepta 1M como mínimo
            MSJ: Solo puede realizar una consignación 
        */

        [Test]
        public void consignacionDeMenosDeUnMillon() {
            var CDT = new CDTService(number: "101010", city: "valledupar", Termin: CDTService.SEMESTER);
            var result = CDT.Consign(900000, new DateComplex("01/01/2020"));
            Assert.AreEqual("Solo se acepta 1M como mínimo", result);
        }

        [Test]
        public void consignacionDeUnMillon() {
            var CDT = new CDTService(number: "101010", city: "valledupar", Termin: CDTService.SEMESTER);
            var result = CDT.Consign(1000000, new DateComplex("01/01/2020"));
            Assert.AreEqual("Se realizo la consignacion", result);
        }

        [Test]
        public void NoMasDeUnaConsignacion() {
            var CDT = new CDTService(number: "101010", city: "valledupar", Termin: CDTService.SEMESTER);
            CDT.Consign(1000000, new DateComplex("01/01/2020"));
            var result = CDT.Consign(1000000, new DateComplex("01/01/2020"));
            Assert.AreEqual("Solo se puede realizar una consignacion",result);
        }
    }
}