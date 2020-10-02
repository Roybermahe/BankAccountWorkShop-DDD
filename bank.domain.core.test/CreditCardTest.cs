using NUnit.Framework;

namespace bank.domain.core.test
{
    public class CreditCardTest
    {
        [SetUp]
        public void Setup()
        {
        }

        /*
        HU 5.
        Como Usuario quiero realizar consignaciones (abonos) a una Tarjeta Crédito para abonar al saldo
        del servicio.
        Criterios de Aceptación
        5.1 El valor a abono no puede ser menor o igual a 0.
        5.2 El abono podrá ser máximo el valor del saldo de la tarjeta de crédito.
        5.3 Al realizar un abono el cupo disponible aumentará con el mismo valor que el valor del abono
        y reducirá de manera equivalente el saldo.

        MSJ: El valor del abono no esta permitido
        MSJ:  Se realizo el abono a la cuenta
        */

        [Test]
        public void realizarAbono()
        {
            var creditCard = new CreditCard("javier", "3022745897", "V/dupar", 500);
            var result = creditCard.Consign(0,  new DateComplex("01/01/2020"));
            Assert.AreEqual("El valor del abono no esta permitido", result);
        }

        [Test]
        public void realizarAbonoMayorASaldo()
        {
            var creditCard = new CreditCard("javier", "3022745897", "V/dupar", 500);
            var result = creditCard.Consign(600,  new DateComplex("01/01/2020"));
            Assert.AreEqual(creditCard.Quota, 500);
            Assert.AreEqual(creditCard.Balance, 500);
            Assert.AreEqual("El valor del abono no esta permitido", result);
        }

        [Test]
        public void realizarAbonoExacto()
        {
            var creditCard = new CreditCard("javier", "3022745897", "V/dupar", 500);
            var result = creditCard.Consign(500,  new DateComplex("01/01/2020"));
            Assert.AreEqual(creditCard.Quota, creditCard.Balance);
            Assert.AreEqual("Se realizo el abono a la cuenta", result);
        }

        /*
        HU 6.
        Como Usuario quiero realizar retiros (avances) a una cuenta de crédito para retirar dinero en
        forma de avances del servicio de crédito.
        Criterios de Aceptación
        6.1 El valor del avance debe ser mayor a 0.
        6.2 Al realizar un avance se debe reducir el valor disponible del cupo con el valor del avance.
        6.3 Un avance no podrá ser mayor al valor disponible del cupo.
        
        MSJ: Valor del avance no esta permitido
        MSJ: Se realizo el avance
        */
        [Test]
        public void realizarAvancesConCero()
        {
            var creditCard = new CreditCard("javier", "3022745897", "V/dupar", 500);
            var result = creditCard.Takes(0,  new DateComplex("01/01/2020"));
            Assert.AreEqual("Valor del avance no esta permitido", result);
        }

        [Test]
        public void realizarAvancesConMasDeCero()
        {
            var creditCard = new CreditCard("javier", "3022745897", "V/dupar", 500);
            var result = creditCard.Takes(400, new DateComplex("01/01/2020"));
            Assert.AreEqual(creditCard.Balance, 100);
            Assert.AreEqual("Se realizo el avance", result);
        }

        [Test]
        public void realizarAvancesConMasDeLaQuota()
        {
            var creditCard = new CreditCard("javier", "3022745897", "V/dupar", 500);
            var result = creditCard.Takes(600, new DateComplex("01/01/2020"));
            Assert.AreEqual(creditCard.Balance, 500);
            Assert.AreEqual("Valor del avance no esta permitido", result);
        }
    }
}