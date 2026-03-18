using System.ComponentModel.DataAnnotations;

namespace AplicacionesWeb.Models
{
    public class CuentaBancaria
    {
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string NumeroCuenta { get; set; } = string.Empty;

        [Range(0, 1000000000)]
        public decimal Saldo { get; set; }

        public void Depositar(decimal cantidad)
        {
            if (cantidad <= 0)
            {
                Console.WriteLine("La cantidad a depositar debe ser mayor que cero.");
                return;
            }

            Saldo += cantidad;
        }


        public decimal ObtenerSaldo()
        {
            return Saldo;
        }

        public decimal Retirar(decimal cantidad)
        {
            if (cantidad <= 0)
            {
                Console.WriteLine("La cantidad a retirar debe ser mayor que cero.");
                return 0;
            }
            if (cantidad > Saldo)
            {
                Console.WriteLine("Fondos insuficientes.");
                return 0;
            }
            Saldo -= cantidad;
            return cantidad;
        }
    }
}