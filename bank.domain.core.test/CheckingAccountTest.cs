using NUnit.Framework;

namespace bank.domain.core.test
{
   public class CheckingAccountTest
   {
      [SetUp]
      public void Setup()
      {
      }
      
      /*HU 3.
      Como Usuario quiero realizar consignaciones a una cuenta corriente para salvaguardar el dinero.
      Criterios de Aceptación
      3.1 La consignación inicial debe ser de mínimo 100 mil pesos.
      3.2 El valor consignado debe ser adicionado al saldo de la cuenta
      MSJ: No se aceptan menos de 100 mil para cuenta corriente
      MSJ: Se consignarón {consignQuantity}, su cuenta tiene {Balance}
      */

      [Test]
      public void consignacionInicial()
      {
         var checkingAccount = new CheckingAccount(number: "10001", name: "Cuenta Ejemplo", city: "Valledupar", OverdraftBalance: 100000);
         var result = checkingAccount.Consign(5000,  new DateComplex("01/01/2020"));
         Assert.AreEqual("No se aceptan menos de 100 mil para cuenta corriente", result);
      }
      
      [Test]
      public void consignacionComun()
      {
         var checkingAccount = new CheckingAccount(number: "10001", name: "Cuenta Ejemplo", city: "Valledupar", OverdraftBalance: 100000);
         var result = checkingAccount.Consign(105000, new DateComplex("01/01/2020"));
         Assert.AreEqual(checkingAccount.OverdraftBalance, 21000); // up value with sobregiro
         Assert.AreEqual($"Se consignarón $ 105,000.00, su cuenta tiene {checkingAccount.Balance:n2}", result);
      }
      
      /*
       * HU 4.
         Como Usuario quiero realizar retiros a una cuenta corriente para salvaguardar el dinero.
         Criterios de Aceptación
         4.1 El valor a retirar se debe descontar del saldo de la cuenta.
         4.2 El saldo mínimo deberá ser mayor o igual al cupo de sobregiro.
         4.3 El retiro tendrá un costo del 4×Mil
       *
       * MSJ: Se desconto dinero de su saldo actual
       * MSJ: No puede retirar esta cantidad de dinero
       */
      [Test]
      public void descuentoDeDinero()
      {
         var checkingAccount = new CheckingAccount(number: "10001", name: "Cuenta Ejemplo", city: "Valledupar", OverdraftBalance: 100000);
         checkingAccount.Consign(225000, new DateComplex("01/01/2020"));
         var result = checkingAccount.Takes(100000, new DateComplex("01/01/2020"));
         Assert.AreEqual("Se desconto dinero de su saldo actual", result);
      }
      
      [Test]
      public void saldoMinimoSobregiro()
      {
         var checkingAccount = new CheckingAccount(number: "10001", name: "Cuenta Ejemplo", city: "Valledupar", OverdraftBalance: 100000);
         checkingAccount.Consign(225000, new DateComplex("01/01/2020")); // overdraftBalance = 45000
         var result = checkingAccount.Takes(189000, new DateComplex("01/01/2020"));
         Assert.AreEqual(checkingAccount.OverdraftBalance, 45000);
         Assert.AreEqual("No puede retirar esta cantidad de dinero", result);
      }
      
      [Test]
      public void aplicacionCuatroXMil()
      {
         var checkingAccount = new CheckingAccount(number: "10001", name: "Cuenta Ejemplo", city: "Valledupar", OverdraftBalance: 100000);
         checkingAccount.Consign(225000,  new DateComplex("01/01/2020")); // overdraftBalance = 45000
         var result = checkingAccount.Takes(100000, new DateComplex("01/01/2020"));
         Assert.AreEqual(checkingAccount.Balance, 124100);
         Assert.AreEqual("Se desconto dinero de su saldo actual", result);
      }
   }
}