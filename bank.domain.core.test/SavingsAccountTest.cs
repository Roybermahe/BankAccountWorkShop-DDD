using System;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using NUnit.Framework;

namespace bank.domain.core.test
{
    public class SavingsAccountTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Escenario: Valor de consignación negativo o cero 
        //H1: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el dinero.
        //Criterio de Aceptación:
        //1.2 El valor a abono no puede ser menor o igual a 0
        //Ejemplo
        //Dado El cliente tiene una cuenta de ahorro                                       //A =>Arrange /Preparación
        //Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0 , ciudad Valledupar                               
        //Cuando Va a consignar un valor menor o igual a cero  (0)                            //A =>Act = Acción
        //Entonces El sistema presentará el mensaje. “El valor a consignar es incorrecto”  //A => Assert => Validación
        [Test]
        public void NoPuedeConsignacionValorNegativoCuentaAhorroTest()
        {
            //Preparar
            var savingsAccount = new SavingsAccount( number: "10001", name: "Cuenta Ejemplo", city: "Valledupar");
            //Acción
            var result=savingsAccount.Consign(0, "enero", "2020");
            //Verificación
            Assert.AreEqual("El valor a consignar es incorrecto", result);
        }
        
        //Escenario: Consignación Inicial Correcta
        //HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el dinero.
        //Criterio de Aceptación:
        //1.1 La consignación inicial debe ser mayor o igual a 50 mil pesos
        //1.3 El valor de la consignación se le adicionará al valor del saldo aumentará
        //Dado El cliente tiene una cuenta de ahorro
        //Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0
        //Cuando Va a consignar el valor inicial de 50 mil pesos
        //Entonces El sistema registrará la consignación
        //AND presentará el mensaje. “Su Nuevo Saldo es de $50.000,00 pesos m/c”.
        [Test]
        public void PuedeConsignacionInicialCorrectaCuentaAhorroTest()
        {
            //Preparar
            var savingsAccount = new SavingsAccount( number: "10001", name: "Cuenta Ejemplo", city: "Valledupar");
            //Acción
            var resultado = savingsAccount.Consign(50000, "enero", "2020");
            //Verificación
            Assert.AreEqual("Su Nuevo Saldo es de $50,000.00 pesos m/c", resultado);
        } 
        //Escenario: Consignación Inicial Incorrecta
        //HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el dinero.
        //Criterio de Aceptación:
        //1.1 La consignación inicial debe ser mayor o igual a 50 mil pesos
        //Dado El cliente tiene una cuenta de ahorro con
        //Número 10001, Nombre “Cuenta ejemplo”, Saldo de 0
        //Cuando Va a consignar el valor inicial de $49.999 pesos
        //Entonces El sistema no registrará la consignación
        //AND presentará el mensaje. “El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos”.
        [Test]
        public void NoPuedeConsignarMenosDeCincuentaMilPesosTest()
        {
            //Preparar
            var savingsAccount = new SavingsAccount( number: "10001", name: "Cuenta Ejemplo", city: "Valledupar");
            //Acción
            var result = savingsAccount.Consign(49999, "enero", "2020");
            //Verificación
            Assert.AreEqual("El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos", result);
        }
        
    /*HU 2.
    *
    Como Usuario quiero realizar retiros a una cuenta de ahorro para obtener el dinero en efectivo
    Criterios de Aceptación
    2.1 El valor a retirar se debe descontar del saldo de la cuenta.
    2.2 El saldo mínimo de la cuenta deberá ser de 20 mil pesos.
    2.3 Los primeros 3 retiros del mes no tendrán costo.
    2.4 Del cuarto retiro en adelante del mes tendrán un valor de 5 mil pesos.
    *
    * MSJ: No tiene suficiente saldo para realizar un retiro
    * MSJ: Usted acaba de retirar X cantidad de su cuenta de ahorros
    */
    [Test]
    public void DescontarValorDeCuentaDeAhorro()
    {
        var savingsAccount = new SavingsAccount( number: "10001", name: "Cuenta Ejemplo", city: "Valledupar");
        savingsAccount.Consign(50000, "enero", "2020");
        var take = savingsAccount.Takes(5000, "enero", "2020");
        Assert.AreEqual($"Usted acaba de retirar $ 5,000.00 de su cuenta de ahorros, Saldo restante {savingsAccount.Balance}", take);
    }
    
    [Test]
    public void SaldoMinimoEnCuentaDeAhorro()
    {
        var savingsAccount = new SavingsAccount( number: "10001", name: "Cuenta Ejemplo", city: "Valledupar");
        savingsAccount.Consign(0, "enero", "2020");
        var take = savingsAccount.Takes(5000, "enero", "2020");
        Assert.AreEqual("No tiene suficiente saldo para realizar un retiro", take);
    }

    [Test]
    public void LosTresRetirosDelMesSinCostoYConCosto()
    {
        var savingsAccount = new SavingsAccount(number: "10001", name: "Cuenta Ejemplo", city: "Valledupar");
        savingsAccount.Consign(50000 , "enero", "2020");
        savingsAccount.Takes(5000, "enero", "2020"); // first take
        savingsAccount.Takes(5000, "enero", "2020"); // second take
        var third = savingsAccount.Takes(5000, "enero", "2020"); // third take
        Assert.AreEqual($"Usted acaba de retirar $ 5,000.00 de su cuenta de ahorros, Saldo restante {savingsAccount.Balance}", third);
        Assert.AreEqual(savingsAccount.Balance, 35000);
        
        var fourth = savingsAccount.Takes(5000, "enero", "2020"); // fourth take
        Assert.AreEqual($"Usted acaba de retirar $ 5,000.00 de su cuenta de ahorros, Saldo restante {savingsAccount.Balance}", fourth);
        Assert.AreEqual(savingsAccount.Balance, 25000); //expected aplied value 5000 
    }

    [Test]
    public void RetirosEnMesesDiferentes()
    {
        var savingsAccount = new SavingsAccount(number: "10001", name: "Cuenta Ejemplo", city: "Valledupar");
        savingsAccount.Consign(50000 , "enero", "2020");

        savingsAccount.Takes(5000, "febrero", "2020");
        Assert.AreEqual(savingsAccount.Balance,45000);
        
        savingsAccount.Takes(5000, "marzo", "2021");
        Assert.AreEqual(savingsAccount.Balance,40000);
    }
    }
}